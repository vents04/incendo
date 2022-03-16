using System;

namespace Data.Models
{
    public class ModificationOutcome
    {
        public Guid Id { get; set; }
        public string ParticipantPublicKey { get; set; }
        public PermutationOutcome PermutationOutcome { get; set; }
    }
}