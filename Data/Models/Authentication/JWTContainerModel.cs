using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Data.Models
{
    public class JWTContainerModel : IAuthContainerModel
    {
        #region Public Methods

        public int ExpireMinutes { get; set; } = 10080; // 7 days.
        public string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;

        public Claim[] Claims { get; set; }

        #endregion Public Methods
    }
}