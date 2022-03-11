using System.Collections.Generic;

namespace Data.Models
{
    public class CampaignItemAssignee : CampaignItem
    {
        public IEnumerable<CampaignItem> AssignedItems { get; set; }
        public int MaxAssigned { get; set; }
        public int MinAssigned { get; set; }
    }
}