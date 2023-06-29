using AgustinDonalisioProyectoPNT1.Models;
using AgustinDonalisioProyectoPNT1.Models.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AgustinDonalisioProyectoPNT1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Wine> Wines { get; set; }
        public DbSet<Cellar> Cellars { get; set; }
        public DbSet<CellarWine> CellarWines { get; set;}

        public DbSet<CreateWineModel> CreateWineModels { get; set; }

        public DbSet<HistoryWine> HistoryWines { get; set; }
    }
}