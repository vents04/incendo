using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class CampaignConfiguration
    {
        public Guid id { get; set; }

        [Column(TypeName = "bigint")]
        public TimeSpan ModificationsPhaseDuration { get; set; }

        [Column(TypeName = "bigint")]
        public TimeSpan DecryptionPhaseDuration { get; set; }

        public int PermutationLength { get; set; }
    }
}