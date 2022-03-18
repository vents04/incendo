using Data.Models;
using Data.Models.InputModels;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Common;
using Services.Authentication;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ServerAPI.Controllers
{
    internal class TokenResponse
    {
        public string token { get; set; }
    }

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

        [HttpPost("login")]
        public IActionResult Login(OrganisationUserLoginInputModel input)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid request structure");
            var item = _dbContext.OrganisationUsers.FirstOrDefault(user => user.Name == input.Name);
            if (item == null) return NotFound();
            var hash = Encoding.ASCII.GetString(cryptoProvider.ComputeHash(Encoding.ASCII.GetBytes(input.Password)));
            if (item.PasswordHash != hash) return BadRequest("Invalid password or name");
            return Ok(new TokenResponse
            {
                token = _authService.GenerateToken(new JWTContainerModel()
                {
                    Claims = new System.Security.Claims.Claim[] {
                    new System.Security.Claims.Claim("Name", item.Name),
                    new System.Security.Claims.Claim("Id", item.Id.ToString()),
                }
                })
            });
        }

        [HttpPost("register")]
        public IActionResult Register(OrganisationUserRegisterInputModel input)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid request structure");
            var item = _dbContext.OrganisationUsers.FirstOrDefault(user => user.Name == input.Name);
            if (item != null) return BadRequest("User already exists");
            _dbContext.OrganisationUsers.Add(new OrganisationUser()
            {
                Name = input.Name,
                PasswordHash = Encoding.ASCII.GetString(cryptoProvider.ComputeHash(Encoding.ASCII.GetBytes(input.Password)))
                //TODO:bind other parameters
            });
            _dbContext.SaveChanges();
            return Ok(new TokenResponse
            {
                token = _authService.GenerateToken(new JWTContainerModel()
                {
                    Claims = new System.Security.Claims.Claim[] {
                    new System.Security.Claims.Claim("Name", item.Name),
                    new System.Security.Claims.Claim("Id", item.Id.ToString()),
                }
                })
            });
        }
    }
}