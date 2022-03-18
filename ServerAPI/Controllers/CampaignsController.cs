using Data.Models;
using Data.Models.InputModels;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Common;
using Services.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IAuthService _jwtService;

        public CampaignsController(ApplicationDbContext context, IAuthService jwtService)
        {
            _dbContext = context;
            _jwtService = jwtService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var artefacts = new List<CampaignArtefact>();
            foreach (var item in _dbContext.Campaigns)
            {
                artefacts.Add(item.GetArtefact() as CampaignArtefact);
            }
            return Ok(artefacts);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var item = _dbContext.Campaigns.Find(Guid.Parse(id));
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Post(CampaignInputModel input, [FromHeader] string authorization)
        {
            if (!authorization.Equals(string.Empty))
            {
                var token = authorization;
                if (!_jwtService.IsTokenValid(token)) Unauthorized("invalid token");
                var claims = _jwtService.GetTokenClaims(token);
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var item = _dbContext.Campaigns.FirstOrDefault(item => item.Name == input.Name);
                if (item != null) BadRequest("cmapaing already exists");
                var config = new CampaignConfiguration()
                {
                    DecryptionPhaseDuration = TimeSpan.FromMilliseconds(input.Settings.DecryptionPhaseDuration),
                    ModificationsPhaseDuration = TimeSpan.FromMilliseconds(input.Settings.ModificationsPhaseDuration),
                    PermutationLength = input.Settings.PermutationLength
                };
                var organisationId = claims.FirstOrDefault(claim => claim.Type == "Id").Value;
                if (string.IsNullOrEmpty(organisationId)) BadRequest("invalid token claims");
                _dbContext.Campaigns.Add(new Campaign(config)
                {
                    Description = input.Description,
                    Name = input.Name,
                    Type = input.Type,
                    OrganisationId = Guid.Parse(organisationId)
                });
                _dbContext.SaveChanges();
            }
            else return Unauthorized("missing token");
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] CampaignInputModel input)
        {
            //TODO: check validity for updates
            //if (!ModelState.IsValid) BadRequest(ModelState);
            //TODO: authenticate using header
            //var item = _campaignService.UpdateCampaign(id, input);
            //if (item == null) return NotFound();
            //return Ok(item);
            return null;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            //TODO: authenticate using header
            var item = _dbContext.Campaigns.Find(Guid.Parse(id));
            if (item == null) return NotFound();
            _dbContext.Remove(item);
            _dbContext.SaveChanges();
            return Ok(item);
        }
    }
}