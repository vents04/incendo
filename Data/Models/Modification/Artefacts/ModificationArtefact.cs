using System;

namespace Data.Models
{
    public class ModificationArtefact : Artefact
    {
        public string OrganisationPublicKey { get; set; }
        public string CampaignId { get; set; }
        public string CampaignHash { get; set; }
        public string ParticipantPublicKey { get; set; }
        public int Phase { get; set; }
        public EncryptedPermutation EncryptedPermutation { get; set; }
    }
}