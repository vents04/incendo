using System;
using System.IO;
using System.Security.Cryptography;

namespace Data.Models
{
    public class Permutation
    {//TODO: implement with sequence
        public Guid Id { get; set; }
        public int Length { get; set; }
        public string EncryptedSequence { get; set; }
        public string SequenceHash { get; set; }
        public bool HasKey { get; set; }

        public Sequence Sequence { get; set; }
        private string aesKey;
        private string aesIV;

        //client
        public Permutation(int length)
        {
            if (length < 1) throw new ArgumentOutOfRangeException();

            Length = length;
            RandomNumberGenerator generator = RandomNumberGenerator.Create();
            int[] permutation = new int[length];
            for (int i = 0; i < length; i++)
            {
                permutation[i] = i;
            }
            byte[] random = new byte[length * 4 * 2 * 10];
            generator.GetBytes(random);
            for (int i = 0; i < random.Length - 7; i++)
            {
                int from = ((int)random[i] | (((int)random[i + 1]) << 8) | (((int)random[i + 2]) << 16) | (((int)random[i + 3]) << 24));
                int to = ((int)random[i + 4] | (((int)random[i + 5]) << 8) | (((int)random[i + 6]) << 16) | (((int)random[i + 7]) << 24));
                if (from < 0) from = -from;
                if (to < 0) to = -to;
                from %= length;
                to %= length;
                int v = permutation[from];
                permutation[from] = permutation[to];
                permutation[to] = v;
            }
            Sequence = new Sequence(permutation);
            Aes aes = Aes.Create();
            HasKey = true;
            aes.GenerateIV();
            aes.GenerateKey();
            aesKey = Convert.ToBase64String(aes.Key);
            aesIV = Convert.ToBase64String(aes.IV);
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] sequenceBytes = new byte[length * 8];
            for (int i = 0; i < length; i++)
            {//TODO: fill up to 128 bytes
                byte[] bytes = BitConverter.GetBytes(permutation[i]);
                Array.Copy(bytes, 0, sequenceBytes, i * 8, 4);
                generator.GetBytes(sequenceBytes, i * 8 + 4, 4);
            }
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(sequenceBytes, 0, sequenceBytes.Length);
                }
                EncryptedSequence = Convert.ToBase64String(memoryStream.ToArray());
            }
            SHA512CryptoServiceProvider sha512Provier = new SHA512CryptoServiceProvider();
            SequenceHash = Convert.ToBase64String(sha512Provier.ComputeHash(sequenceBytes, 0, sequenceBytes.Length));
        }

        //host server
        private Permutation(int length, string encryptedSequence, string sequnceHash)
        {
            Length = length;
            EncryptedSequence = encryptedSequence;
            SequenceHash = sequnceHash;
        }

        public static Permutation FromEncrypted(EncryptedPermutation permutation)
        {
            if (permutation == null) throw new ArgumentNullException();
            if ((permutation.EncryptedSequence == null) || (permutation.SequenceHash == null)) throw new ContractException(ContractError.InvalidEcryptedPermutation);
            return new Permutation(permutation.Length, permutation.EncryptedSequence, permutation.SequenceHash);
        }

        public EncryptedPermutation GetEncrypted()
        {
            EncryptedPermutation info = new EncryptedPermutation
            {
                Length = Length,
                EncryptedSequence = EncryptedSequence,
                SequenceHash = SequenceHash
            };
            return info;
        }

        public bool IsEqualTo(EncryptedPermutation info)
        {
            if (info.Length != Length) return false;
            if (info.EncryptedSequence != EncryptedSequence) return false;
            if (info.SequenceHash != SequenceHash) return false;
            return true;
        }

        public bool IsKeyEqualTo(PermutationKey key)
        {
            if (key.SequenceHash != SequenceHash) return false;
            if (key.AesKey != aesKey) return false;
            if (key.AesIV != aesIV) return false;
            return true;
        }

        public PermutationKey GetKeyInfo()
        {
            if (aesKey == null) throw new InvalidOperationException();

            PermutationKey info = new PermutationKey
            {
                SequenceHash = SequenceHash,
                AesKey = aesKey,
                AesIV = aesIV
            };
            return info;
        }

        //host server
        public void AddKey(PermutationKey key)
        {
            if (key == null) throw new ArgumentNullException();
            if (key.SequenceHash != SequenceHash) throw new ContractException(ContractError.PermutationKeyMismatch);
            if ((key.AesKey == null) || (key.AesIV == null)) throw new ContractException(ContractError.MissingPermutationKey);
            Aes aes = Aes.Create();
            aes.Key = Serialization.DecodeBase64(key.AesKey);
            aes.IV = Serialization.DecodeBase64(key.AesIV);
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using MemoryStream memoryStream = new MemoryStream();
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
            {
                byte[] bytes = Serialization.DecodeBase64(EncryptedSequence);
                cryptoStream.Write(bytes, 0, bytes.Length);
            }
            byte[] decryptedSequenceBytes = memoryStream.ToArray();
            if (decryptedSequenceBytes.Length < Length * 8) throw new ContractException(ContractError.InvalidPermutation);

            SHA512CryptoServiceProvider sha512Provier = new SHA512CryptoServiceProvider();
            if (SequenceHash != Convert.ToBase64String(sha512Provier.ComputeHash(decryptedSequenceBytes, 0, decryptedSequenceBytes.Length))) throw new ContractException(ContractError.PermutationKeyMismatch);

            if (!Sequence.Valid()) throw new ContractException(ContractError.InvalidPermutation);

            int[] decryptedSequence = new int[Length];
            for (int i = 0; i < Length; i++)
            {
                int index = BitConverter.ToInt32(decryptedSequenceBytes, i * 8);
                decryptedSequence[i] = index;
            }

            HasKey = true;
            aesKey = key.AesKey;
            aesIV = key.AesIV;
            Sequence = new Sequence(decryptedSequence);
        }

        public void ApplyTo(object[] target)
        {
            if (target == null) throw new ArgumentNullException();
            if (target.Length != Length) throw new ArgumentOutOfRangeException();
            if (Sequence == null) throw new InvalidOperationException();

            Sequence.ApplyTo(target);
        }

        public PermutationOutcome GetOutcome()
        {
            PermutationOutcome info = new PermutationOutcome
            {
                Sequence = Sequence
            };
            return info;
        }
    }
}