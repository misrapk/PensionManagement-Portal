using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PenionDisbursementAPI.Models;

namespace PenionDisbursementAPI.Providers
{
	public class PensionProvider : IPensionProvider
	{
		public PensionerDetail pensionerDetail = null;

		public PensionerDetail GetData(string AadharNumber)
		{
			string pensionerDetailURL = "https://pensionerdetailapiget.azurewebsites.net/";
			//string pensionerDetailURL = "https://localhost:5001/";
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(pensionerDetailURL);
				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage response = new();

				try
				{
					response = client.GetAsync("api/PensionerDetail/" + AadharNumber).Result;
				}
				catch (Exception e)
				{
					//_log4net.Debug("Exception Occured " + e);

					response = null;

				}
				if
					(response != null)
				{
					// _log4net.Debug("Connecting Sucessfull");

					var ObjResponse = response.Content.ReadAsStringAsync().Result;
					pensionerDetail = JsonConvert.DeserializeObject<PensionerDetail>(ObjResponse);
					return pensionerDetail;
				}
				return pensionerDetail;
			}
		}
	}
}
