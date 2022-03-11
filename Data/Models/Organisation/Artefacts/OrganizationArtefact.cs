namespace Data.Models
{
    public class OrganizationArtefact : Artefact
    {
        public string OrganizationId { get; set; }
        public string Name { get; set; }
        public string PublicKey { get; set; }
    }
}