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
        public virtual DbSet<Categories> Category { get; set; }
        public virtual DbSet<CategoryMachines> CategoryMachine { get; set; }
        public virtual DbSet<Machines> Machine { get; set; }
        public virtual DbSet<MachineProduces> MachineProduce { get; set; }
        public virtual DbSet<MachineWareHouses> MachineWareHouse { get; set; }

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

            modelBuilder.Entity<Machines>(entity =>
            {
                entity.ToTable("Machine", SchemaApp);
                entity.HasOne(a => a.MachineProduces)
                       .WithOne(b => b.Machines)
                .HasForeignKey<MachineProduces>(a => a.MachineId);

                entity.HasOne(a => a.MachineWarehouses)
                       .WithOne(c => c.Machines)
                       .HasForeignKey<MachineWareHouses>(a => a.MachineId);

                entity.HasMany(a => a.ChildrenMachine)
                        .WithOne(b => b.ParentMachine)
                        .HasForeignKey(a => a.ParentId);

            });
            modelBuilder.Entity<CategoryMachines>(entity =>
            {
                entity.ToTable("CategoryMachine", SchemaApp);

                entity.HasOne(a => a.Category)
                        .WithMany(b => b.CategoryMachines)
                        .HasForeignKey(a => a.CategoryId);

                entity.HasOne(a => a.Machine)
                       .WithMany(b => b.CategoryMachines)
                       .HasForeignKey(a => a.MachineId);
            });
            modelBuilder.Entity<MachineProduces>(entity =>
            {
                entity.ToTable("MachineProduce", SchemaApp);
            });
            modelBuilder.Entity<MachineWareHouses>(entity =>
            {
                entity.ToTable("MachineWareHouse", SchemaApp);
            });



        }
    }
}
