namespace Data.Models
{
    public class PermutationKey
    {
        public string SequenceHash { get; set; }
        public string AesKey { get; set; }
        public string AesIV { get; set; }
    }
}