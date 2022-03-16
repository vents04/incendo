using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class CampaignPhase
    {
        public Guid Id { get; set; }
        public Permutation InitialPermutation { get; set; }

        public List<Modification> Modifications;

        [NotMapped]
        private Dictionary<string, Modification> participantPublicKeyToModification = null;

        [NotMapped]
        public Dictionary<string, Modification> ParticipantPublicKeyToModification
        {
            get
            {
                if (participantPublicKeyToModification == null)
                {
                    foreach (var modification in Modifications)
                    {
                        participantPublicKeyToModification = new Dictionary<string, Modification>();
                        participantPublicKeyToModification.Add(modification.ParticipantKey.PublicKey, modification);
                    }
                }
                return participantPublicKeyToModification;
            }
        }

        private CampaignPhase()
        {
        }

        public CampaignPhase(Permutation initial)
        {
            InitialPermutation = initial;
        }

        public void AddModification(string participantPublicKey, Modification modification)
        {
            Modifications.Add(modification);
            participantPublicKeyToModification.Add(participantPublicKey, modification);
        }

        public CampaignPhaseOutcome GetOutcome()
        {
            int i = 0;
            ModificationOutcome[] outcomes = new ModificationOutcome[ParticipantPublicKeyToModification.Count];
            ParticipantPublicKeyToModification.Values.ToList().ForEach(modification => outcomes[i++] = modification.GetOutcome());
            return new CampaignPhaseOutcome()
            {
                NumberOfSubmissions = ParticipantPublicKeyToModification.Count,
                ModificationOutcomes = outcomes
            };
        }
    }
}