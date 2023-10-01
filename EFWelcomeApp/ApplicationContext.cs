using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EFWelcomeApp
{
    public class ApplicationContext : DbContext
    {
        string? connectionString = "";
        public DbSet<Employee> Employees { set; get; } = null!;
        public DbSet<Company> Companies { set; get; } = null!;

        public ApplicationContext(string? connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.LogTo(Console.WriteLine);
        }
    }
}
