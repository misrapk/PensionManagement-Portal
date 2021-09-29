using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PenionDisbursementAPI.Models;
using PenionDisbursementAPI.Repository;

namespace PenionDisbursementAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DisbursementController : ControllerBase
	{
		private readonly IPensionerRepo _repo;
		ProcessPensionResponse processPensionResponse = new();
		PensionerDetail pensionerDetail = null;

		public DisbursementController(IPensionerRepo repo)
		{
			_repo = repo;
		}

		[HttpPost]
		public IActionResult DisbursePension(ProcessPensionInput input)
		{
			pensionerDetail = _repo.GetDetail(input.AadharNumber);
			processPensionResponse.ProcessPensionStatusCode = _repo.Status(pensionerDetail, input);
			return Ok(processPensionResponse);
		}
	}
}
