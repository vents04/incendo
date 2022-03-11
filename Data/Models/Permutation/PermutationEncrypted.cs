namespace Data.Models
{
    public class EncryptedPermutation
    {
        public int Length { get; set; }
        public string EncryptedSequence { get; set; }
        public string SequenceHash { get; set; }
    }
}