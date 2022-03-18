using ExtensionMethods;
using System;

namespace Data.Models
{
    public class Modification : IArtefactable<Modification>
    {
        public RSAKeyPair ParticipantKey { get; set; }
        public string OrganisationPublicKey { get; set; }
        public Permutation Permutation { get; set; }
        public PermutationKey PermutationKey { get; set; }
        public Guid CampaignId { get; set; }
        public string CampaignHash { get; set; }
        public int Phase { get; set; }
        private bool isRecorded = false;
        private DateTime timeRecorded;
        private DateTime keyRecordTime;

        private Modification(RSAKeyPair participantKey, string organisationPublicKey, Guid campaignId, string campaignHash, Permutation permutation, int phase)
        {
            if (participantKey == null || organisationPublicKey == null || campaignId.Equals(Guid.Empty)) throw new ArgumentNullException();
            ParticipantKey = participantKey;
            OrganisationPublicKey = organisationPublicKey;
            Permutation = permutation;
            Phase = phase;
            CampaignId = campaignId;
            CampaignHash = campaignHash;
        }

        public Modification(RSAKeyPair participantKey, string organisationPublicKey, Guid campaignId, string campaignHash, int permutationLength, int phase)
            : this(participantKey, organisationPublicKey, campaignId, campaignHash, new Permutation(permutationLength), phase)
        { }

        public new static Modification FromArtefact(Artefact _modification)
        {
            if ((_modification == null)) throw new ArgumentNullException();
            var modification = _modification as ModificationArtefact;
            RSAKeyPair participantPublicKey = RSAKeyPair.FromPublicKey(modification.ParticipantPublicKey);
            if (modification.EncryptedPermutation == null) throw new ContractException(ContractError.InvalidMod);
            Permutation permutation = Permutation.FromEncrypted(modification.EncryptedPermutation);

            return new Modification(participantPublicKey, modification.OrganisationPublicKey, Guid.Parse(modification.CampaignId), modification.CampaignHash, permutation, modification.Phase);
        }

        public override Artefact GetArtefact()
        {
            return new ModificationArtefact()
            {
                OrganisationPublicKey = OrganisationPublicKey,
                CampaignId = CampaignId.ToString(),
                CampaignHash = CampaignHash,
                ParticipantPublicKey = ParticipantKey.PublicKey,
                Phase = Phase,
                EncryptedPermutation = Permutation.GetEncrypted(),
            };
        }

        public ModificationConfirmationArtefact GetConfirmationArtefact()
        {
            if (!isRecorded) throw new InvalidOperationException();

            var artefact = GetArtefact() as ModificationConfirmationArtefact;
            artefact.TimeRecorded = timeRecorded.ToUnixMilliseconds();
            return artefact;
        }

        public ModificationKeyArtefact GetKeyArtefact()
        {
            if (PermutationKey == null) throw new InvalidOperationException();

            var artefact = GetArtefact() as ModificationKeyArtefact;
            artefact.PermutationKey = PermutationKey;
            return artefact;
        }

        public ModificationKeyConfirmationArtefact GetKeyConfirmationArtefact()
        {
            var artefact = GetKeyArtefact() as ModificationKeyConfirmationArtefact;
            artefact.KeyRecordTime = keyRecordTime.ToUnixMilliseconds();
            return artefact;
        }

        public void ValidateArtefact(ModificationArtefact artefact)
        {
            if (artefact == null) throw new ArgumentNullException();
            if (artefact.OrganisationPublicKey != OrganisationPublicKey) throw new ContractException(ContractError.InvalidModConfirmation);
            if (artefact.CampaignId != CampaignId.ToString()) throw new ContractException(ContractError.InvalidModConfirmation);
            if (artefact.CampaignHash != CampaignHash) throw new ContractException(ContractError.InvalidModConfirmation);
            if (artefact.ParticipantPublicKey != ParticipantKey.PublicKey) throw new ContractException(ContractError.InvalidModConfirmation);
            if (artefact.Phase != Phase) throw new ContractException(ContractError.InvalidModConfirmation);
            if (!Permutation.IsEqualTo(artefact.EncryptedPermutation)) throw new ContractException(ContractError.InvalidModConfirmation);
        }

        public void ValidateConfirmationArtefact(ModificationConfirmationArtefact artefact, bool validateResponseTime = false)
        {
            ValidateArtefact(artefact);

            if (validateResponseTime)
            {
                long timeDelta = DateTime.UtcNow.ToUnixMilliseconds() - artefact.TimeRecorded;
                if ((timeDelta < -60) || (timeDelta > 60)) throw new ContractException(ContractError.ValidationTimeout);
            }

            if (isRecorded)
            {
                if (timeRecorded.ToUnixMilliseconds() != artefact.TimeRecorded) throw new ContractException(ContractError.InconsistentResponse);
            }
        }

        public void ValidateKeyConfirmationArtefact(ModificationKeyConfirmationArtefact artefact, bool validateResponseTime = false)
        {
            ValidateArtefact(artefact);

            if (validateResponseTime)
            {
                long timeDelta = DateTime.UtcNow.ToUnixMilliseconds() - artefact.KeyRecordTime;
                if ((timeDelta < -60) || (timeDelta > 60)) throw new ContractException(ContractError.ValidationTimeout);
            }

            if (PermutationKey != null)
            {
                if (keyRecordTime.ToUnixMilliseconds() != artefact.KeyRecordTime) throw new ContractException(ContractError.InconsistentResponse);
            }
        }

        public void Confirm(ModificationArtefact artefact)
        {
            ValidateArtefact(artefact);
            isRecorded = true;
            timeRecorded = DateTime.UtcNow;
        }

        public void AddKey(ModificationKeyArtefact artefact)
        {
            ValidateArtefact(artefact);
            if (artefact.PermutationKey == null) throw new ContractException(ContractError.InvalidModKey);

            PermutationKey = artefact.PermutationKey;
            Permutation.AddKey(PermutationKey);
            keyRecordTime = DateTime.UtcNow;
        }

        public ModificationOutcome GetOutcome()
        {
            ModificationOutcome info = new ModificationOutcome();
            info.ParticipantPublicKey = ParticipantKey.PublicKey;
            info.PermutationOutcome = Permutation.GetOutcome();
            return info;
        }
    }
}