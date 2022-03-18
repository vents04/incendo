using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Data.Models
{
    public class RSAKeyPair
    {
        public Guid Id { get; set; }

        [NotMapped]
        public const int RSABitCount = 2048;

        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }

        [NotMapped]
        public readonly RSACryptoServiceProvider Provider;

        private RSAKeyPair()
        {
        }

        private RSAKeyPair(string publicKey, string privateKey, RSACryptoServiceProvider provider)
        {
            if ((publicKey == null) && (privateKey == null)) throw new ArgumentNullException();

            PublicKey = publicKey;
            PrivateKey = privateKey;
            Provider = provider;
        }

        public static RSAKeyPair Create()
        {
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider(RSABitCount);

            string publicKey = EncodePublicKey(provider);
            string privateKey = EncodePrivateKey(provider);

            return new RSAKeyPair(publicKey, privateKey, provider);
        }

        public static RSAKeyPair FromPublicKey(string publicKey)
        {
            return new RSAKeyPair(publicKey, null, DecodePublicKey(publicKey));
        }

        public string SignWithPrivate(string data)
        {
            if (data == null) throw new ArgumentNullException();
            if (PrivateKey == null) throw new InvalidOperationException();

            return Convert.ToBase64String(Provider.SignData(Encoding.UTF8.GetBytes(data), new SHA512CryptoServiceProvider()));
        }

        public string SignWithPrivate(byte[] data)
        {
            if (data == null) throw new ArgumentNullException();
            if (PrivateKey == null) throw new InvalidOperationException();

            return Convert.ToBase64String(Provider.SignData(data, new SHA512CryptoServiceProvider()));
        }

        public void ValidateWithPublic(string data, string signature)
        {
            if (data == null) throw new ArgumentNullException();

            if (!Provider.VerifyData(Encoding.UTF8.GetBytes(data), new SHA512CryptoServiceProvider(), Serialization.DecodeBase64(signature)))
            {
                throw new ContractException(ContractError.InvalidSignature);
            }
        }

        public string SignArtefact(Artefact artefact, string challenge)
        {
            if (artefact == null) throw new ArgumentNullException();

            artefact.ArtefactType = artefact.GetType().Name;
            artefact.IssueTime = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
            artefact.Challenge = challenge;
            artefact.SignaturePublicKey = PublicKey;
            artefact.Hash = null;
            artefact.Signature = null;
            string json = JsonSerializer.Serialize(artefact, artefact.GetType());

            SHA512CryptoServiceProvider sha512Provier = new SHA512CryptoServiceProvider();
            artefact.Hash = Convert.ToBase64String(sha512Provier.ComputeHash(Encoding.UTF8.GetBytes(json)));
            artefact.Signature = SignWithPrivate(json);

            return JsonSerializer.Serialize(artefact, artefact.GetType());
        }

        public static Artefact ValidateArtefact(RSAKeyPair key, string artefactJson, string challenge, bool validateResponseTime)
        {
            Artefact artefact = Serialization.DeserializeInheritor<Artefact>(typeof(Artefact).Name, artefactJson);
            artefact = Serialization.DeserializeInheritor<Artefact>(artefact.ArtefactType, artefactJson);
            if (key != null)
            {
                key.ValidateArtefact(artefact, challenge, validateResponseTime);
            }
            else
            {
                ValidateArtefactWithTheIncludedKey(artefact, challenge, validateResponseTime);
            }
            return artefact;
        }

        public void ValidateArtefact(Artefact artefact, string challenge, bool validateResponseTime)
        {
            if (artefact == null) throw new ArgumentNullException();

            if (artefact.SignaturePublicKey != PublicKey) throw new ContractException(ContractError.KeyMismatch);
            if ((challenge != null) && (artefact.Challenge != challenge)) throw new ContractException(ContractError.ChallengeMismatch);

            if (validateResponseTime)
            {
                double timeDelta = (new DateTime(artefact.IssueTime * TimeSpan.TicksPerMillisecond) - DateTime.UtcNow).TotalSeconds;
                if ((timeDelta < -60) || (timeDelta > 60)) throw new ContractException(ContractError.ValidationTimeout);
            }

            string hash = artefact.Hash;
            string signature = artefact.Signature;

            artefact.Hash = null;
            artefact.Signature = null;
            string json = JsonSerializer.Serialize(artefact, artefact.GetType());
            artefact.Hash = hash;
            artefact.Signature = hash;

            SHA512CryptoServiceProvider sha512Provier = new SHA512CryptoServiceProvider();
            if (hash != Convert.ToBase64String(sha512Provier.ComputeHash(Encoding.ASCII.GetBytes(json)))) throw new ContractException(ContractError.HashMismatch);

            ValidateWithPublic(json, signature);
        }

        public static void ValidateArtefactWithTheIncludedKey(Artefact artefact, string challenge, bool validateResponseTime)
        {
            if (artefact == null) throw new ArgumentNullException();

            if (artefact.SignaturePublicKey == null) throw new ContractException(ContractError.MissingKey);
            RSAKeyPair key = FromPublicKey(artefact.SignaturePublicKey);

            key.ValidateArtefact(artefact, challenge, validateResponseTime);
        }

        public static string EncodePrivateKey(RSACryptoServiceProvider rsa)
        {
            if (rsa == null) throw new ArgumentNullException();

            RSAParameters parameters = rsa.ExportParameters(true);
            StringBuilder b = new StringBuilder();
            b.Append(Convert.ToBase64String(parameters.P));
            b.Append('_');
            b.Append(Convert.ToBase64String(parameters.Q));
            b.Append('_');
            b.Append(Convert.ToBase64String(parameters.Modulus));
            b.Append('_');
            b.Append(Convert.ToBase64String(parameters.Exponent));
            b.Append('_');
            b.Append(Convert.ToBase64String(parameters.D));
            b.Append('_');
            b.Append(Convert.ToBase64String(parameters.DP));
            b.Append('_');
            b.Append(Convert.ToBase64String(parameters.DQ));
            b.Append('_');
            b.Append(Convert.ToBase64String(parameters.InverseQ));
            return b.ToString();
        }

        public static RSACryptoServiceProvider DecodePrivateKey(string code)
        {
            if (code == null) throw new ArgumentNullException();

            RSAParameters parameters = new RSAParameters();
            string[] parts = code.Split('_');
            if (parts.Length != 8) throw new ContractException(ContractError.InvalidPrivateKey);
            parameters.P = Serialization.DecodeBase64(parts[0]);
            parameters.Q = Serialization.DecodeBase64(parts[1]);
            parameters.Modulus = Serialization.DecodeBase64(parts[2]);
            parameters.Exponent = Serialization.DecodeBase64(parts[3]);
            parameters.D = Serialization.DecodeBase64(parts[4]);
            parameters.DP = Serialization.DecodeBase64(parts[5]);
            parameters.DQ = Serialization.DecodeBase64(parts[6]);
            parameters.InverseQ = Serialization.DecodeBase64(parts[7]);
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(RSABitCount);
            rsa.ImportParameters(parameters);
            return rsa;
        }

        public static string EncodePublicKey(RSACryptoServiceProvider rsa)
        {
            if (rsa == null) throw new ArgumentNullException();

            RSAParameters parameters = rsa.ExportParameters(false);
            StringBuilder b = new StringBuilder();
            b.Append(Convert.ToBase64String(parameters.Modulus));
            b.Append('_');
            b.Append(Convert.ToBase64String(parameters.Exponent));
            return b.ToString();
        }

        public static RSACryptoServiceProvider DecodePublicKey(string code)
        {
            if (code == null) throw new ArgumentNullException();

            RSAParameters parameters = new RSAParameters();
            string[] parts = code.Split('_');
            if (parts.Length != 2) throw new ContractException(ContractError.InvalidPublicKey);
            parameters.Modulus = Serialization.DecodeBase64(parts[0]);
            parameters.Exponent = Serialization.DecodeBase64(parts[1]);
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(RSABitCount);
            rsa.ImportParameters(parameters);
            return rsa;
        }
    }
}