using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PensionManagementPortalAPP.Models;
using PensionManagementPortalAPP.Repository;

namespace PensionManagementPortalAPP.Controllers
{
	public class PensionController : Controller
	{
		static string token;
		private IConfiguration _configuration;
		PensionDetail detailObj = new();
		PensionDetail resDetails = new();
		private readonly IPensionPortalRepo _repo;

		public PensionController(IConfiguration configuration, IPensionPortalRepo repo)
		{
			_configuration = configuration;
			_repo = repo;

		}

		//Login Form for users
		public ActionResult Login()
		{
			return View();
		}


		//Take login creds from app and pass to auth api
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(Login cred)
		{
			Login loginCred = new();
			string tokenVal = _configuration.GetValue<string>("MyLinkValue:tokenUri");

			using (var httpClient = new HttpClient())
			{
				StringContent content = new StringContent(JsonConvert.SerializeObject(cred), Encoding.UTF8, "application/json");

				//link of auth controller api
				using (var res = await httpClient.PostAsync("https://pensionauthorizationapi.azurewebsites.net/api/Auth/", content))
				{
					if (!res.IsSuccessStatusCode)
					{
						//Login Failed
						ViewBag.Message = "Please Enter Valid Creds";
						return View("Login");
					}

					//suuccsffull login and token generate
					string strToken = await res.Content.ReadAsStringAsync();

					loginCred = JsonConvert.DeserializeObject<Login>(strToken);

					string userName = cred.Username;
					token = strToken;
					HttpContext.Session.SetString("token", strToken);
					HttpContext.Session.SetString("user", JsonConvert.SerializeObject(cred));
					HttpContext.Session.SetString("owner", userName);

				}
			}
			return View("PensionPortal");
		}

		/// <summary>
		/// For logging out of the current session
		/// </summary>
		/// <returns></returns>
		public ActionResult Logout()
		{
			HttpContext.Session.Clear();

			return View("Login");
		}

		//Get Input Values Form the Pensioner

		public ActionResult PensionPortal()
		{

			if (HttpContext.Session.GetString("token") == null)
			{
				
				ViewBag.Message = "Please Login First";
				return View("Login");
			}
			
			return View();
		}

		//processing the INput--> return output view
		[HttpPost]
		
		public async Task<ActionResult> PensionPortal(PensionerInput input)
		{
			if(HttpContext.Session.GetString("token") == null)
			{
				//Pensioner isnot logged in
				ViewBag.Message("Please Login First");
				return View("Login");
			}
			//login-->YES Proces the pension

			string processValue = _configuration.GetValue<string>("MyLinkValue:processUri");

			if (ModelState.IsValid)
			{
				using(var client = new HttpClient())
				{
					StringContent content = new(JsonConvert.SerializeObject(input), Encoding.UTF32, "applicaiton/json");
					client.BaseAddress = new Uri(processValue);
					client.DefaultRequestHeaders.Clear();
					client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


					try
					{
						using (var res = await client.PostAsync("api/ProcessPension/ProcessPension/", content))
						{
							var apiRes = await res.Content.ReadAsStringAsync();
							ProcessResponse processRes = JsonConvert.DeserializeObject<ProcessResponse>(apiRes);
							detailObj.Status = processRes.Result.Status;

							detailObj.PensionAmount = processRes.Result.PensionAmount;

							resDetails.PensionAmount = detailObj.PensionAmount;

							resDetails.Status = detailObj.Status;
						}
					}
					catch(Exception e)
					{
						//API not working
						detailObj = null;
					}
				}

				if(detailObj == null)
				{
					ViewBag.erroroccured = "Error Occured!";
					return View();

				}
				if (detailObj.Status.Equals(21))
				{
					ViewBag.erroroccured = "Error Occured! Try Again!";
					return View();
				}

				if (detailObj.Status.Equals(10))
				{
					//store in db
					//details mathced with csv and save the data in db
					_repo.AddResponse(resDetails);
					_repo.Save();
					return RedirectToAction("PensionerValuesDisplayed", resDetails);
				}
				else
				{
					//details not mathced with csv
					ViewBag.notmatch = "Pensioner Values not match";
					return View();
				}
			}
			//ViewBag.invalid = "Pensioner Values are not Valid";
			return View();
		}

		public ActionResult PensionerValuesDisplayed(PensionDetail detail)
		{
			return View(detail);
		}
	}
}
