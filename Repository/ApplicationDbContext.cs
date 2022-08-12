
using IRepository;
using Entities;
using Microsoft.EntityFrameworkCore;
namespace Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SearchTopProducts> SearchTopProducts { get; set; }
        public DbSet<Request> Request { get; set; }
        public DbSet<SearchRequest> SearchRequest { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasKey(e => e.Rid);
                entity.Property(e => e.Kind).IsRequired();
                entity.Property(e => e.Timestamp).IsRequired();
            });

            modelBuilder.Entity<SearchRequest>(entity =>
            {
                entity.HasKey(e => e.Rid);
                entity.Property(e => e.Hits).IsRequired();
                entity.Property(e => e.SuccessInd).IsRequired();
                entity.Property(e => e.Search).IsRequired();

            });
     
    modelBuilder.Entity<SearchTopProducts>()
                //.HasName("PK_DevTest_SearchTopProducts") /*===>>> Not able to assign the composite primary key name here due to reference error in current version.*/
                .HasKey(c => new { c.Rid, c.Order });                 
        }
    }
}