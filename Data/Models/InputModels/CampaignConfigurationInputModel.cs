using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.InputModels
{
    public class CampaignConfigurationInputModel
    {
        [Required]
        public long ModificationsPhaseDuration { get; set; }

        [Required]
        public long DecryptionPhaseDuration { get; set; }

        [Required]
        public int PermutationLength { get; set; }
    }
}