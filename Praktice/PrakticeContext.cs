using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Praktice
{
    public partial class PrakticeContext : DbContext
    {
        public PrakticeContext()
        {
        }

        public PrakticeContext(DbContextOptions<PrakticeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-GK4LR7T;Database=Praktice;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<File>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.File1)
                    .HasMaxLength(5000)
                    .HasColumnName("File");

                entity.HasOne(d => d.AuthorNavigation)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.Author)
                    .HasConstraintName("FK_Files_Students");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Text).HasMaxLength(500);

                entity.HasOne(d => d.AuthorNavigation)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.Author)
                    .HasConstraintName("FK_Messages_Users");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.StatusName)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Status_Name");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasIndex(e => e.Info, "IX_Students")
                    .IsUnique();

                entity.Property(e => e.Theme).HasMaxLength(50);

                entity.HasOne(d => d.CuratorNavigation)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.Curator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Students_Teachers");

                entity.HasOne(d => d.InfoNavigation)
                    .WithOne(p => p.Student)
                    .HasForeignKey<Student>(d => d.Info)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Students_Users");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.Property(e => e.Cathedra).HasMaxLength(50);

                entity.HasOne(d => d.InfoNavigation)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.Info)
                    .HasConstraintName("FK_Teachers_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(160)
                    .IsUnicode(false);

                entity.Property(e => e.SecondName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Second_Name");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Statuses");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
