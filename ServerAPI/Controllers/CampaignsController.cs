using Data.Models;
using Data.Models.InputModels;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Common;
using System;
using System.Collections.Generic;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public CampaignsController(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public IEnumerable<Campaign> Get()
        {
            return _dbContext.Campaigns;
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var item = _dbContext.Campaigns.Find(Guid.Parse(id));
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Post(CampaignInputModel input)
        {//TODO: implement input models
            //TODO: authenticate using header
            //if (!ModelState.IsValid) return BadRequest(ModelState);
            //var result = _campaignService.AddCampaign(input);
            //return Ok(result);
            return null;
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