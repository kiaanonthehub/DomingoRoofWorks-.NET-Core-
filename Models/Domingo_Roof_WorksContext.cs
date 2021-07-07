using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DomingoRoofWorks.Models;

#nullable disable

namespace DomingoRoofWorks.Models
{
    public partial class Domingo_Roof_WorksContext : DbContext
    {
        public Domingo_Roof_WorksContext()
        {
        }

        public Domingo_Roof_WorksContext(DbContextOptions<Domingo_Roof_WorksContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<JobEmployee> JobEmployees { get; set; }
        public virtual DbSet<JobMaterial> JobMaterials { get; set; }
        public virtual DbSet<JobType> JobTypes { get; set; }
        public virtual DbSet<Material> Materials { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=KIAAN;Initial Catalog=Domingo_Roof_Works;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("EmployeeID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.HasKey(e => e.JobCardId)
                    .HasName("PK__Job__98F41DDF3D847537");

                entity.ToTable("Job");

                entity.Property(e => e.JobCardId)
                    .ValueGeneratedNever()
                    .HasColumnName("JobCardID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.JobTypeId).HasColumnName("JobTypeID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Job__CustomerID__3E52440B");

                entity.HasOne(d => d.JobType)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.JobTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Job__JobTypeID__3F466844");
            });

            modelBuilder.Entity<JobEmployee>(entity =>
            {
                entity.ToTable("Job_Employee");

                entity.Property(e => e.JobEmployeeId).HasColumnName("JobEmployeeID");

                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("EmployeeID");

                entity.Property(e => e.JobCardId).HasColumnName("JobCardID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.JobEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Job_Emplo__Emplo__4316F928");

                entity.HasOne(d => d.JobCard)
                    .WithMany(p => p.JobEmployees)
                    .HasForeignKey(d => d.JobCardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Job_Emplo__JobCa__4222D4EF");
            });

            modelBuilder.Entity<JobMaterial>(entity =>
            {
                entity.ToTable("Job_Material");

                entity.Property(e => e.JobMaterialId).HasColumnName("JobMaterialID");

                entity.Property(e => e.JobCardId).HasColumnName("JobCardID");

                entity.Property(e => e.MaterialId).HasColumnName("MaterialID");

                entity.HasOne(d => d.JobCard)
                    .WithMany(p => p.JobMaterials)
                    .HasForeignKey(d => d.JobCardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Job_Mater__JobCa__46E78A0C");

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.JobMaterials)
                    .HasForeignKey(d => d.MaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Job_Mater__Mater__47DBAE45");
            });

            modelBuilder.Entity<JobType>(entity =>
            {
                entity.ToTable("JobType");

                entity.Property(e => e.JobTypeId).HasColumnName("JobTypeID");

                entity.Property(e => e.JobType1)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("JobType");

                entity.Property(e => e.Rate).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable("Material");

                entity.Property(e => e.MaterialId).HasColumnName("MaterialID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<DomingoRoofWorks.Models.Login> Login { get; set; }
    }
}
