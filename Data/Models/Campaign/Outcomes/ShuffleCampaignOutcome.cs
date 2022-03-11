using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ShuffleCampaignOutcome : CampaignOutcome
    {
        public IEnumerable<CampaignItem> ShuffledSequence { get; set; }
    }
}