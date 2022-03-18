using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Data.Models
{
    public class Campaign : IArtefactable<Campaign>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public CampaignType Type { get; set; }
        public List<CampaignPhase> Phases { get; set; }
        public Guid Id { get; set; }
        public Guid OrganisationId { get; set; }
        public string OrganisationPublicKey { get; set; }
        public RSAKeyPair Key { get; set; }
        public CampaignConfiguration Configuration { get; set; }
        public List<CampaignEvent> Events { get; set; } = new List<CampaignEvent>();
        public CampaignState State { get; set; }
        public DateTime LastStateChange { get; set; }
        public CampaignOutcome Outcome { get; set; }

        public int Phase
        {
            get { return Phases.Count(); }
        }

        public DateTime FinishTime { get; set; }
        public DateTime ActivationTime { get; set; }
        public string Hash { get; set; }

        public Campaign()
        {
            Id = Guid.NewGuid();
        }

        public Campaign(CampaignConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException();

            Id = Guid.NewGuid();
            Key = RSAKeyPair.Create();
            Configuration = configuration;
            Hash = ComputeHash();

            State = CampaignState.Inactive;
            Events.Add(new CampaignEvent(CampaignEventType.Created, ""));
        }

        private Campaign(Guid id, string organisationPublicKey, RSAKeyPair campaignKey, CampaignConfiguration configuration, DateTime activationTime)
        {
            Id = id;
            OrganisationPublicKey = organisationPublicKey;
            Key = campaignKey;
            Configuration = configuration;
            Hash = ComputeHash();
            State = CampaignState.Active;
            ActivationTime = activationTime;
        }

        public static Campaign FromArtefact(CampaignArtefact artefact, string organisationPublicKey)
        {
            if ((artefact == null) || (organisationPublicKey == null)) throw new ArgumentNullException();

            if (organisationPublicKey != artefact.OrganisationPublicKey) throw new ContractException(ContractError.InvalidCampaign);

            RSAKeyPair campaignKey = RSAKeyPair.FromPublicKey(artefact.CampaignPublicKey);

            CampaignConfiguration CampaignConfiguration = Serialization.DeserializeInheritor<CampaignConfiguration>(artefact.CampaignConfigurationType, artefact.CampaignConfigurationJson);

            Campaign campaign = new Campaign(artefact.CampaignId, organisationPublicKey, campaignKey, CampaignConfiguration, artefact.ActivationTime);
            if (campaign.Hash != artefact.CampaignHash) throw new ContractException(ContractError.InvalidCampaign);

            return campaign;
        }

        private string ComputeHash()
        {
            CampaignArtefact artefact = new CampaignArtefact
            {
                OrganisationPublicKey = OrganisationPublicKey,
                CampaignId = Id,
                CampaignPublicKey = Key.PublicKey,
                ActivationTime = ActivationTime,
                CampaignConfigurationType = Configuration.GetType().Name,
                CampaignConfigurationJson = JsonSerializer.Serialize(Configuration, Configuration.GetType())
            };

            string json = JsonSerializer.Serialize(artefact, artefact.GetType());
            SHA512CryptoServiceProvider sha512Provier = new SHA512CryptoServiceProvider();
            return Convert.ToBase64String(sha512Provier.ComputeHash(Encoding.UTF8.GetBytes(json)));
        }

        public override Artefact GetArtefact()
        {
            CampaignArtefact artefact = new CampaignArtefact
            {
                OrganisationPublicKey = OrganisationPublicKey,
                CampaignId = Id,
                CampaignPublicKey = Key.PublicKey,
                ActivationTime = ActivationTime,
                CampaignConfigurationType = Configuration.GetType().Name,
                CampaignConfigurationJson = JsonSerializer.Serialize(Configuration, Configuration.GetType()),
                CampaignHash = Hash
            };

            return artefact;
        }

        public void Activate()
        {
            if (State != CampaignState.Inactive) throw new InvalidOperationException();

            State = CampaignState.Active;
            ActivationTime = DateTime.Now;
            Hash = ComputeHash();
            Events.Add(new CampaignEvent(CampaignEventType.Activated, ""));
        }

        public ModificationConfirmationArtefact AddModification(ModificationArtefact artefact)
        {
            if (State != CampaignState.Active) throw new InvalidOperationException();

            if (Phase != 0)
            {
                var lastPhase = Phases[Phases.Count - 2];
                if (!lastPhase.ParticipantPublicKeyToModification.ContainsKey(artefact.ParticipantPublicKey)) throw new ContractException(ContractError.NewParticipantInAdditionalPhase);
                if (!lastPhase.ParticipantPublicKeyToModification.GetValueOrDefault(artefact.ParticipantPublicKey).Permutation.HasKey) throw new ContractException(ContractError.MissingPermutationKey);
            }

            var currentPhase = Phases[Phases.Count - 1];
            Modification modification = Modification.FromArtefact(artefact);
            if (currentPhase.ParticipantPublicKeyToModification.ContainsKey(artefact.ParticipantPublicKey)) throw new ContractException(ContractError.DuplicateParticipant);
            currentPhase.AddModification(artefact.ParticipantPublicKey, modification);

            Events.Add(new CampaignEvent(CampaignEventType.ModificationReceived, modification.GetArtefact().ToString()));
            return modification.GetConfirmationArtefact();
        }

        public void Seal()
        {
            if (State != CampaignState.Active) throw new InvalidOperationException();

            State = CampaignState.Sealed;

            Events.Add(new CampaignEvent(CampaignEventType.ModificationPhaseFinished, Phases[Phases.Count - 1].ToString()));
        }

        public CampaignStateArtefact GetCampaignStateArtefact()
        {
            CampaignStateArtefact artefact = new CampaignStateArtefact
            {
                CampaignId = Id.ToString(),
                CampaignHash = Hash,
                State = State.ToString(),
                Phase = Phase,
                LastStateChangeTime = LastStateChange.ToUnixMilliseconds(),
                EventCount = Events.Count,
                InitialPermutationEncrypted = Phases[Phases.Count - 1].InitialPermutation.GetEncrypted()
            };
            return artefact;
        }

        public ModificationKeyConfirmationArtefact AddModificationKey(ModificationKeyArtefact artefact)
        {
            if (artefact == null) throw new ArgumentNullException();

            if (State != CampaignState.Sealed) throw new InvalidOperationException();

            CampaignPhase currentPhase = Phases[Phases.Count - 1];
            Modification modification = Modification.FromArtefact(artefact);
            PermutationKey key = artefact.PermutationKey;

            if (!currentPhase.ParticipantPublicKeyToModification.ContainsKey(modification.ParticipantKey.PublicKey)) throw new ContractException(ContractError.MissingModification);
            if (currentPhase.ParticipantPublicKeyToModification.GetValueOrDefault(modification.ParticipantKey.PublicKey).Permutation.HasKey) throw new ContractException(ContractError.DuplicateParticipant);

            currentPhase.ParticipantPublicKeyToModification.GetValueOrDefault(modification.ParticipantKey.PublicKey).PermutationKey = key;

            Events.Add(new CampaignEvent(CampaignEventType.PermutationKeyReceived, modification.GetKeyArtefact().ToString()));//TODO: serialize all event data additions

            var resultArtefact = artefact as ModificationKeyConfirmationArtefact;
            resultArtefact.KeyRecordTime = DateTime.Now.ToUnixMilliseconds();
            return resultArtefact;
        }

        public CampaignPhaseOutcome FinishPhase()
        {
            if (State != CampaignState.Sealed) throw new InvalidOperationException();

            CampaignPhase currentPhase = Phases[Phases.Count - 1];

            bool missingKey = currentPhase.ParticipantPublicKeyToModification.Values.Any(modification => !modification.Permutation.HasKey);

            LastStateChange = DateTime.Now;
            Events.Add(new CampaignEvent(CampaignEventType.Activated, GetCampaignStateArtefact().ToString()));
            State = missingKey ? CampaignState.Active : CampaignState.Finished;
            if (missingKey)
            {
                Phases.Add(new CampaignPhase(new Permutation(currentPhase.InitialPermutation.Length)));
                return null;
            }
            return currentPhase.GetOutcome();
        }

        public void Fail()
        {
            if ((State == CampaignState.Finished) || (State != CampaignState.Failed)) throw new InvalidOperationException();
            State = CampaignState.Failed;
            LastStateChange = DateTime.Now;
        }

        public virtual CampaignOutcome GetOutcome()
        {
            return Outcome;//TODO: change behaviour in decsendands (different campaign types)
        }

        public CampignOutcomeArtefact GetOutcomeArtefact()
        {
            if (State != CampaignState.Finished) throw new InvalidOperationException();

            CampaignPhase currentPhase = Phases[Phases.Count - 1];
            CampaignPhaseOutcome[] outcomes = new CampaignPhaseOutcome[Phase];
            for (int i = 0; i < Phase; i++) outcomes[i] = Phases[i].GetOutcome();

            return new CampignOutcomeArtefact
            {
                InitialPermutationOutcome = currentPhase.InitialPermutation.GetOutcome(),
                PhaseOutcomes = outcomes,
                OutcomeType = GetOutcome().GetType().Name,
                OutcomeJson = JsonSerializer.Serialize(GetOutcome(), GetOutcome().GetType()),
                FinishTime = FinishTime.ToUnixMilliseconds(),
            };
        }

        public CampaignEventArtefact GetEventArtefact(int index)
        {
            if ((index < 0) || (index > Events.Count)) throw new ContractException(ContractError.InvalidEventIndex);

            return new CampaignEventArtefact()
            {
                EventId = Events[index].Id.ToString(),
                EventType = Events[index].Type.ToString(),
                EventData = Events[index].data
            };
        }
    }
}