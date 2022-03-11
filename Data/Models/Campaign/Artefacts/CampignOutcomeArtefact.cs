namespace Data.Models
{
    public class CampignOutcomeArtefact : Artefact
    {
        public PermutationOutcome InitialPermutationOutcome { get; set; }
        public CampaignPhaseOutcome[] PhaseOutcomes { get; set; }
        public string OutcomeType { get; set; }
        public string OutcomeJson { get; set; }
        public long FinishTime { get; set; }
    }
}