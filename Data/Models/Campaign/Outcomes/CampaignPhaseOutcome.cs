using System;

namespace Data.Models
{
    public class CampaignPhaseOutcome
    {
        public Guid Id { get; set; }
        public ModificationOutcome[] ModificationOutcomes { get; set; }
        public int NumberOfSubmissions { get; set; }
    }
}