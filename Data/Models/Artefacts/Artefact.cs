using ExtensionMethods;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Data.Models
{
    public class Artefact
    {
        public string ArtefactType { get; set; }
        public long IssueTime { get; set; }
        public string Challenge { get; set; }
        public string SignaturePublicKey { get; set; }
        public string Hash { get; set; }
        public string Signature { get; set; }

        public void Sign(string challenge, RSAKeyPair key)
        {
            if (challenge == null || challenge.Length < 1 || key == null || key.PublicKey == null || key.PublicKey.Length < 1) throw new ArgumentNullException();
            ArtefactType = GetType().Name;
            IssueTime = DateTime.UtcNow.ToUnixMilliseconds();
            Challenge = challenge;
            SignaturePublicKey = key.PublicKey;
            Hash = null;
            Signature = null;
            string json = JsonSerializer.Serialize(this, GetType());

            SHA512CryptoServiceProvider sha512Provider = new SHA512CryptoServiceProvider();
            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();
            Hash = Convert.ToBase64String(sha512Provider.ComputeHash(Encoding.UTF8.GetBytes(json)));
            Signature = Convert.ToBase64String(rsaProvider.SignData(Encoding.UTF8.GetBytes(json), sha512Provider));

            //return JsonSerializer.Serialize(this, GetType());
        }

        public void ValidateAuthenticity(string challenge, RSAKeyPair key, bool validateResponseTime = false)
        {
            if (challenge == null || challenge.Length < 1 || key == null || key.PublicKey == null || key.PublicKey.Length < 1) throw new ArgumentNullException();
            if (SignaturePublicKey != key.PublicKey) throw new ContractException(ContractError.KeyMismatch);
            if ((challenge != null) && (Challenge != challenge)) throw new ContractException(ContractError.ChallengeMismatch);

            if (validateResponseTime)
            {
                double timeDelta = (DateTime.UtcNow.FromUnixMilliseconds(IssueTime) - DateTime.UtcNow).TotalSeconds;
                if ((timeDelta < -60) || (timeDelta > 60)) throw new ContractException(ContractError.ValidationTimeout);
            }

            string hash = Hash;
            string signature = Signature;

            Hash = null;
            Signature = null;
            string json = JsonSerializer.Serialize(this, GetType());
            Hash = hash;
            Signature = signature;

            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();
            SHA512CryptoServiceProvider sha512Provier = new SHA512CryptoServiceProvider();
            if (hash != Convert.ToBase64String(sha512Provier.ComputeHash(Encoding.ASCII.GetBytes(json)))) throw new ContractException(ContractError.HashMismatch);
            if (!rsaProvider.VerifyData(Encoding.UTF8.GetBytes(json), sha512Provier, Serialization.DecodeBase64(signature))) throw new ContractException(ContractError.InvalidSignature);
        }
    }
}