using HD.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using HD.Station.Museum;

namespace HD.Station.Dashboard.SqlServer
{
    public class MuseumDbContext : DbContextBase
    {
        public virtual DbSet<Courses> Course { get; set; }
        public virtual DbSet<Enrollments> Enrollment { get; set; }
        public virtual DbSet<Students> Student { get; set; }
        public virtual DbSet<Students> Category { get; set; }

        private IOptionsSnapshot<StoreOption> _optionsSnapshot;
        public string Schema => _optionsSnapshot.Value?.Schema;
        public string SchemaApp => _optionsSnapshot.Value?.SchemaApp;
        public MuseumDbContext(IServiceProvider serviceProvider, IOptionsSnapshot<StoreOption> snapshot, DbContextOptions<MuseumDbContext> options) : base(serviceProvider, options)
        {
            _optionsSnapshot = snapshot;
        }
        protected override void OnModelCreatingImpl(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Courses>(entity =>
            {
                entity.ToTable("Course", SchemaApp);
            });
            modelBuilder.Entity<Enrollments>(entity =>
            {
                
                entity.ToTable("Enrollment", SchemaApp);
                entity.HasOne(a => a.Student)
                .WithMany(b => b.Enrollments)
                .HasForeignKey(a => a.StudentId);

                entity.HasOne(a => a.Courses)
               .WithMany(b => b.Enrollments)
               .HasForeignKey(a => a.CourseId);


            });
            modelBuilder.Entity<Students>(entity =>
            {
                entity.ToTable("Student", SchemaApp);
            });
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.ToTable("Category", SchemaApp);
                entity.HasMany(a => a.ChildrenCategory)
                .WithOne(b => b.ParentCategory)
                .HasForeignKey(a => a.ParentId);
            });

            //modelBuilder.Entity<Enrollments>()
            //.HasKey(sc => new { sc.StudentId, sc.CourseId });





        }
    }
}
