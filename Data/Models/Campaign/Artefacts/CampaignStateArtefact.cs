using System;

namespace Data.Models
{
    public class CampaignStateArtefact : Artefact
    {
        public string CampaignId { get; set; }
        public string CampaignHash { get; set; }
        public string State { get; set; }
        public int Phase { get; set; }
        public long LastStateChangeTime { get; set; }
        public int EventCount { get; set; }
        public EncryptedPermutation InitialPermutationEncrypted { get; set; }
    }
}