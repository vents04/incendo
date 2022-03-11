using System;

namespace Data.Models
{
    public class Organization : IArtefactable<Organization>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public RSAKeyPair Key { get; set; }

        private Organization(string name, RSAKeyPair key, Guid id)
        {
            if ((name == null) || (key == null) || id.Equals(Guid.Empty)) throw new ArgumentNullException();

            Id = id;
            Name = name;
            Key = key;
        }

        public Organization(string name, RSAKeyPair key)
            : this(name, key, Guid.NewGuid())
        { }

        public static Organization Create(string name)
        {
            if (name == null) throw new ArgumentNullException();

            return new Organization(name, RSAKeyPair.Create());
        }

        public new static Organization FromArtefact(Artefact _artefact)
        {
            if (_artefact == null) throw new ArgumentNullException();

            var artefact = _artefact as OrganizationArtefact;
            if ((artefact.Name == null) || (artefact.PublicKey == null)) throw new ContractException(ContractError.InvalidOrganization);

            RSAKeyPair key = RSAKeyPair.FromPublicKey(artefact.PublicKey);
            return new Organization(artefact.Name, key, Guid.Parse(artefact.OrganizationId));
        }

        public override Artefact GetArtefact()
        {
            return new OrganizationArtefact()
            {
                OrganizationId = Id.ToString(),
                Name = Name,
                PublicKey = Key.PublicKey
            };
        }
    }
}