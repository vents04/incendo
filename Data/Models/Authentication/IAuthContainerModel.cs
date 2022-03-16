using System.Security.Claims;

namespace Data.Models
{
    public interface IAuthContainerModel
    {
        #region Members

        string SecurityAlgorithm { get; set; }
        int ExpireMinutes { get; set; }

        Claim[] Claims { get; set; }

        #endregion Members
    }
}