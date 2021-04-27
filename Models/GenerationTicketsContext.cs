using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace GenerationTicketsWPF.Models
{
    public partial class GenerationTicketsContext : DbContext
    {
        public GenerationTicketsContext()
        {
        }

        public GenerationTicketsContext(DbContextOptions<GenerationTicketsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Chairman> Chairmans { get; set; }
        public virtual DbSet<Discipline> Disciplines { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Specialty> Specialties { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Teaching> Teachings { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TypesTask> TypesTasks { get; set; }
        public virtual DbSet<Worker> Workers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=GenerationTickets;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Chairman>(entity =>
            {
                entity.Property(e => e.ChairmanId).HasColumnName("Chairman_ID");

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("LName");

                entity.Property(e => e.Sname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SName");
            });

            modelBuilder.Entity<Discipline>(entity =>
            {
                entity.Property(e => e.DisciplineId).HasColumnName("Discipline_ID");

                entity.Property(e => e.DisciplineName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Discipline_name");

                entity.Property(e => e.SpecialtyId)
                    .IsRequired()
                    .HasMaxLength(8)
                    .HasColumnName("Specialty_ID");

                entity.HasOne(d => d.Specialty)
                    .WithMany(p => p.Disciplines)
                    .HasForeignKey(d => d.SpecialtyId)
                    .HasConstraintName("FK__Disciplin__Speci__7A521F79");
            });

            modelBuilder.Entity<Level>(entity =>
            {
                entity.Property(e => e.LevelId).HasColumnName("Level_ID");

                entity.Property(e => e.LeverDecryption)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Lever_Decryption");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).HasColumnName("Role_ID");

                entity.Property(e => e.RoleDecryption)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Role_decryption");
            });

            modelBuilder.Entity<Specialty>(entity =>
            {
                entity.ToTable("Specialty");

                entity.Property(e => e.SpecialtyId)
                    .HasMaxLength(8)
                    .HasColumnName("Specialty_ID");

                entity.Property(e => e.ChairmanId).HasColumnName("Chairman_ID");

                entity.Property(e => e.SpecialtyDecryption)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Specialty_decryption");

                entity.HasOne(d => d.Chairman)
                    .WithMany(p => p.Specialties)
                    .HasForeignKey(d => d.ChairmanId)
                    .HasConstraintName("FK__Specialty__Chair__7775B2CE");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(e => new { e.TaskId, e.DisciplineId })
                    .HasName("PK_UNIQIE_Tasks");

                entity.Property(e => e.TaskId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Task_ID");

                entity.Property(e => e.DisciplineId).HasColumnName("Discipline_ID");

                entity.Property(e => e.LevelId).HasColumnName("Level_ID");

                entity.Property(e => e.TaskDecryption)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Task_decryption");

                entity.Property(e => e.TypesTaskId).HasColumnName("Types_Task_ID");

                entity.Property(e => e.WorkerId).HasColumnName("Worker_ID");

                entity.HasOne(d => d.Level)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.LevelId)
                    .HasConstraintName("FK__Tasks__Level_ID__0D64F3ED");

                entity.HasOne(d => d.TypesTask)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.TypesTaskId)
                    .HasConstraintName("FK__Tasks__Types_Tas__0C70CFB4");

                entity.HasOne(d => d.Teaching)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => new { d.DisciplineId, d.WorkerId })
                    .HasConstraintName("FK__Tasks__0B7CAB7B");
            });

            modelBuilder.Entity<Teaching>(entity =>
            {
                entity.HasKey(e => new { e.DisciplineId, e.WorkerId })
                    .HasName("PK_UNIQUE_Teaching");

                entity.ToTable("Teaching");

                entity.Property(e => e.DisciplineId).HasColumnName("Discipline_ID");

                entity.Property(e => e.WorkerId).HasColumnName("Worker_ID");

                entity.HasOne(d => d.Discipline)
                    .WithMany(p => p.Teachings)
                    .HasForeignKey(d => d.DisciplineId)
                    .HasConstraintName("FK__Teaching__Discip__03DB89B3");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.Teachings)
                    .HasForeignKey(d => d.WorkerId)
                    .HasConstraintName("FK__Teaching__Worker__04CFADEC");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => new { e.TicketId, e.TaskNumber })
                    .HasName("PK_UNIQUE_Tickets");

                entity.Property(e => e.TicketId).HasColumnName("Ticket_ID");

                entity.Property(e => e.DisciplineId).HasColumnName("Discipline_ID");

                entity.Property(e => e.TaskId).HasColumnName("Task_ID");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => new { d.TaskId, d.DisciplineId })
                    .HasConstraintName("FK__Tickets__10416098");
            });

            modelBuilder.Entity<TypesTask>(entity =>
            {
                entity.ToTable("TypesTask");

                entity.Property(e => e.TypesTaskId).HasColumnName("Types_Task_ID");

                entity.Property(e => e.TypesTaskDecryption)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Types_Task_Decryption");
            });

            modelBuilder.Entity<Worker>(entity =>
            {
                entity.HasIndex(e => e.WorkerLogin, "UQ__Workers__913667C7D366FA7D")
                    .IsUnique();

                entity.Property(e => e.WorkerId).HasColumnName("Worker_ID");

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("FName");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("LName");

                entity.Property(e => e.RoleId).HasColumnName("Role_ID");

                entity.Property(e => e.Sname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SName");

                entity.Property(e => e.WorkerLogin)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Worker_Login")
                    .UseCollation("Latin1_General_CS_AS");

                entity.Property(e => e.WorkerPassword)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Worker_password")
                    .UseCollation("Latin1_General_CS_AS");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Workers)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Workers__Role_ID__000AF8CF");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
