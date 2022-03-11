using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models.InputModels
{
    public class CampaignInputModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public CampaignType Type { get; set; }

        [Required]
        public List<CampaignItem> CampaignItems { get; set; }

        [Required]
        public CampaignConfiguration Settings { get; set; }
    }
}