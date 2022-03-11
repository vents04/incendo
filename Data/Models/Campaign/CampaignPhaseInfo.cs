using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class CampaignPhase
    {
        public CampaignPhase(Permutation initial)
        {
            InitialPermutation = initial;
            ParticipantPublicKeyToModification = new Dictionary<string, Modification>();
        }

        public Permutation InitialPermutation { get; set; }
        public Dictionary<string, Modification> ParticipantPublicKeyToModification { get; set; }

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