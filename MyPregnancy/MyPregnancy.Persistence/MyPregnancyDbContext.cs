namespace MyPregnancy.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using MyPregnancy.Application.Interfaces;
    using MyPregnancy.Domain.Entities;

    public class MyPregnancyDbContext : DbContext, IMyPregnancyDbContext
    {
        public MyPregnancyDbContext(DbContextOptions<MyPregnancyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patient { get; set; }
        public DbSet<MedicalDetail> MedicalDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyPregnancyDbContext).Assembly);
        }
    }
}
