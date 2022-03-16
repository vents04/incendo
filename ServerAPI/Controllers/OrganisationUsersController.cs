using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models.InputModels;
using ServerAPI.Common;
using System.Security.Cryptography;
using System.Text;
using Services.Authentication;
using Data.Models;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly SHA512CryptoServiceProvider cryptoProvider;
        private readonly ApplicationDbContext _dbContext;

        public UsersController(ApplicationDbContext context, IAuthService authService)
        {
            _dbContext = context;
            cryptoProvider = new SHA512CryptoServiceProvider();
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("ok");
        }

        [HttpPost("/login")]
        public IActionResult Login(OrganisationUserLoginInputModel input)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid request structure");
            var item = _dbContext.OrganisationUsers.Find(input.Name);
            if (item == null) return NotFound();
            var hash = Encoding.ASCII.GetString(cryptoProvider.ComputeHash(Encoding.ASCII.GetBytes(input.Password)));
            if (item.PasswordHash != hash) return BadRequest("Invalid password or name");
            return Ok(_authService.GenerateToken(new JWTContainerModel()
            {
                Claims = new System.Security.Claims.Claim[] {
                    new System.Security.Claims.Claim("Name", item.Name),
                    new System.Security.Claims.Claim("Id", item.Id.ToString()),
                }
            }));
        }

        [HttpPost("/register")]
        public IActionResult Register(OrganisationUserRegisterInputModel input)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid request structure");
            var item = _dbContext.OrganisationUsers.Find(input.Name);
            if (item != null) return BadRequest("User already exists");
            _dbContext.OrganisationUsers.Add(new OrganisationUser()
            {
                Name = input.Name,
                PasswordHash = Encoding.ASCII.GetString(cryptoProvider.ComputeHash(Encoding.ASCII.GetBytes(input.Password)))
                //TODO:bind other parameters
            });
            return Ok();
        }
    }
}