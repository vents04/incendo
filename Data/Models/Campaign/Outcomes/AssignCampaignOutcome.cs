using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class AssignCampaignOutcome : CampaignOutcome
    {
        public IEnumerable<CampaignItemAssignee> Assignees { get; set; }
    }
}