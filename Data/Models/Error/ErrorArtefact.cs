namespace Data.Models
{
    public class ErrorArtefact : Artefact
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}