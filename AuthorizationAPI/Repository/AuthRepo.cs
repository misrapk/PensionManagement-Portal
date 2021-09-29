using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizationAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthorizationAPI.Repository
{
	public class AuthRepo
	{
		private readonly IConfiguration _config;
		private readonly IPensionRepo _repo;

		public AuthRepo(IConfiguration config, IPensionRepo repo)
		{
			_config = config;
			_repo = repo;
		}

		public string GenerateJSONWebToken(PensionCreds userInfo)
		{
			//generate token
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
			var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _config["Jwt:Issuer"],
				audience: _config["Jwt:Issuer"],
				null,
				expires: DateTime.Now.AddMinutes(30),
				signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}



		//find user with matched creds
		public PensionCreds AuthenticateUser(PensionCreds login)
		{
			//validate user and get data
			PensionCreds user = _repo.GetPensionerCredentials(login);
			return user;

		}
	}
}
