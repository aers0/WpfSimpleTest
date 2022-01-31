using Microsoft.EntityFrameworkCore;

namespace WpfSimpleTest.Models
{
    class AppDbContext : DbContext
    {
        public DbSet<SettingsValue> SettingsValue { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=values.db");
            SQLitePCL.Batteries.Init();
        }
    }
}
