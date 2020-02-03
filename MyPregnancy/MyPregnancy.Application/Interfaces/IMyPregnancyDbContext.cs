namespace MyPregnancy.Application.Interfaces
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using MyPregnancy.Domain.Entities;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IMyPregnancyDbContext
    {
        DbSet<Patient> Patient { get; set; }
        DbSet<MedicalDetail> MedicalDetail { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
