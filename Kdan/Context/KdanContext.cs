using Microsoft.EntityFrameworkCore;
using Kdan.Models.Configurations;
using Kdan.Models;

namespace Kdan.Context
{
    public class KdanContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public KdanContext(DbContextOptions<KdanContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var serverVersion = new MariaDbServerVersion(new Version(10, 11, 2));
            optionsBuilder.UseMySql(_configuration.GetConnectionString("DB"), serverVersion);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_bin").HasCharSet("utf8mb4");

            new KdanMembersEntityTypeConfiguration().Configure(modelBuilder.Entity<KdanMembers>());
        }
        public DbSet<KdanMembers> KdanMembers { get; set; }
    }
}
