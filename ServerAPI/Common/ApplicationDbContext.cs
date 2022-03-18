using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ServerAPI.Common
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<RSAKeyPair> RSAKeyPair { get; set; }
        public DbSet<CampaignConfiguration> CampaignConfiguration { get; set; }
        public DbSet<OrganisationUser> OrganisationUsers { get; set; }
    }
}