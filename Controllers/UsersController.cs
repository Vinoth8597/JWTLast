using JWT.Models;
using JWT.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IJWTManagerRepository jWTManagerRepository;
            public UsersController(IJWTManagerRepository jWTManagerRepository)
            {
            this.jWTManagerRepository = jWTManagerRepository;
            }
		[HttpGet]
		[Route("userlist")]
		public List<string> Get()
		{
			var users = new List<string>
		{
			"Satinder Singh",
			"Amit Sarna",
			"Davin Jon"
		};

			return users;
		}

		[AllowAnonymous]
		[HttpPost]
		[Route("authenticate")]
		public IActionResult Authenticate(Users usersdata)
		{
			var token = jWTManagerRepository.Authenticate(usersdata);

			if (token == null)
			{
				return Unauthorized();
			}

			return Ok(token);
		}
	}
}
