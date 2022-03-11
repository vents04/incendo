using System;

namespace Data.Models
{
    public class ContractError
    {
        public static readonly ContractError InternalError = new ContractError(1, "Internal error.");
        public static readonly ContractError InvalidOrganizationId = new ContractError(2, "Invalid organization id.");
        public static readonly ContractError InvalidCampaignId = new ContractError(3, "Invalid campaign id.");
        public static readonly ContractError InvalidParameterType = new ContractError(4, "Invalid parameter type.");
        public static readonly ContractError InvalidParameter = new ContractError(5, "Invalid parameter.");
        public static readonly ContractError InvalidSignature = new ContractError(6, "Invalid signature.");
        public static readonly ContractError KeyMismatch = new ContractError(7, "Key mismatch.");
        public static readonly ContractError ChallengeMismatch = new ContractError(8, "Challenge mismatch.");
        public static readonly ContractError ValidationTimeout = new ContractError(9, "Validation timeout.");
        public static readonly ContractError HashMismatch = new ContractError(10, "Hash mismatch.");
        public static readonly ContractError MissingKey = new ContractError(11, "Missing key.");
        public static readonly ContractError InvalidPublicKey = new ContractError(12, "Invalid public key.");
        public static readonly ContractError InvalidPrivateKey = new ContractError(13, "Invalid private key.");
        public static readonly ContractError InvalidEcryptedPermutation = new ContractError(14, "Invalid encrypted permutation.");
        public static readonly ContractError PermutationKeyMismatch = new ContractError(15, "Permutation key mismatch.");
        public static readonly ContractError MissingPermutationKey = new ContractError(16, "Missing permutation key.");
        public static readonly ContractError InvalidBase64Encoding = new ContractError(17, "Invalid base 64 encoding.");
        public static readonly ContractError InvalidPermutation = new ContractError(18, "Invalid permutation.");
        public static readonly ContractError InvalidOrganization = new ContractError(19, "Invalid organization.");
        public static readonly ContractError InvalidMod = new ContractError(20, "Invalid mod.");
        public static readonly ContractError InvalidModConfirmation = new ContractError(21, "Invalid mod confirmation.");
        public static readonly ContractError InvalidModKey = new ContractError(22, "Invalid mod key.");
        public static readonly ContractError InconsistentResponse = new ContractError(23, "Inconsistent response.");
        public static readonly ContractError InvalidCampaign = new ContractError(24, "Invalid campaign.");
        public static readonly ContractError DuplicateParticipant = new ContractError(25, "Duplicate participant.");
        public static readonly ContractError NewParticipantInAdditionalPhase = new ContractError(26, "New participant in additional phase.");
        public static readonly ContractError TimmedOutParticipant = new ContractError(27, "Timmed out participant.");
        public static readonly ContractError ModificationKeyMismatch = new ContractError(28, "Mod key mismatch.");
        public static readonly ContractError InvalidEventIndex = new ContractError(29, "Invalid event index.");
        public static readonly ContractError MissingModification = new ContractError(30, "This participant has not submitted a modification.");

        public readonly int Code;
        public readonly string Message;

        public ContractError(int code, string message)
        {
            if (message == null) throw new ArgumentNullException();
            Code = code;
            Message = message;
        }
    }

    public class ContractException : Exception
    {
        public readonly ContractError Error;

        public ContractException(ContractError contractError) : base(contractError.Message)
        {
            Error = contractError;
        }
    }
}