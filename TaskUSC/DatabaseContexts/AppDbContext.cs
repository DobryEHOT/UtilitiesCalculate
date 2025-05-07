using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using TaskUSC.Architecture.GoF.Creational;
using TaskUSC.Debug;
using TaskUSC.Models;

namespace TaskUSC.DatabaseContexts
{
    internal class AppDbContext : DbContext
    {
        public DbSet<UtilitiesData> UtilitiesData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=utilities.db");
        }
    }
}
