using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationAPI.Models;
using AuthorizationAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AuthorizationAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private IConfiguration _config;
		private readonly IPensionRepo _repo;

		public AuthController(IConfiguration config, IPensionRepo repo)
		{
			_config = config;
			_repo = repo;
		}

		//Post for login
		[HttpPost]

		public IActionResult Login([FromBody] PensionCreds login)
		{
			AuthRepo authRepo = new AuthRepo(_config, _repo);
			IActionResult res = Unauthorized();
			var user = authRepo.AuthenticateUser(login);
			if(user == null)
			{
				return NotFound();
			}
			else
			{
				var tokenString = authRepo.GenerateJSONWebToken(user);
				res = Ok(new { token = tokenString });
			}

			return res;

		}
	}
}
