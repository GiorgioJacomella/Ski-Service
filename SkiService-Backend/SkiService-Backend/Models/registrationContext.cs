using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SkiService_Backend.Models;

public partial class registrationContext : DbContext
{
    public registrationContext()
    {
    }

    public registrationContext(DbContextOptions<registrationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Registration> Registrations { get; set; }


    public virtual DbSet<UserInfo> UserInfos { get; set; }

    public virtual DbSet<UserSession> UserSessions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\;Database=skiService;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__registra__3214EC27AD5CD482");

            entity.ToTable("registrations");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FinishDate)
                .HasColumnType("date")
                .HasColumnName("finishDate");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Note)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("note");
            entity.Property(e => e.Priority)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("priority");
            entity.Property(e => e.Service)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("service");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("startDate");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.Tel)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("tel");
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__userInfo__3214EC27093D6596");

            entity.ToTable("userInfo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("userName");
        });

        modelBuilder.Entity<UserSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__userSess__3214EC27FE77DB4B");

            entity.ToTable("userSessions");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SessionKey)
                .HasMaxLength(65535)
                .IsUnicode(false)
                .HasColumnName("session_key");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.User).WithMany(p => p.UserSessions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__userSessi__userI__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
