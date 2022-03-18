using System;

namespace Data.Models
{
    public class CampaignArtefact : Artefact
    {
        public string OrganisationPublicKey { get; set; }
        public string CampaignId { get; set; }
        public string CampaignPublicKey { get; set; }
        public long DecryptionPhaseDuration { get; set; }
        public long ModificationsPhaseDuration { get; set; }
        public int PermutationLength { get; set; }
        public long ActivationTime { get; set; }
        public long LastStateChangedTime { get; set; }
        public string CampaignHash { get; set; }
    }
}