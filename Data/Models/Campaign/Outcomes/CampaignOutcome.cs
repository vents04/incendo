using System;

namespace Data.Models
{
    public class CampaignOutcome
    {
        public Guid Id { get; set; }
        public CampaignPhaseOutcome[] PhaseOutcomes { get; set; }
    }
}