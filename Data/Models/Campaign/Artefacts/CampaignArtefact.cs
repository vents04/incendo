using System;

namespace Data.Models
{
    public class CampaignArtefact : Artefact
    {
        public string OrganisationPublicKey { get; set; }
        public Guid CampaignId { get; set; }
        public string CampaignPublicKey { get; set; }
        public string CampaignConfigurationType { get; set; }
        public string CampaignConfigurationJson { get; set; }
        public DateTime ActivationTime { get; set; }
        public string CampaignHash { get; set; }
    }
}