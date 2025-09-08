using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess_Layer.Models;

public partial class TimeSheetContext : DbContext
{
    public TimeSheetContext()
    {
    }

    public TimeSheetContext(DbContextOptions<TimeSheetContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Auditlog> Auditlogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=TimeSheet;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auditlog>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("PK_users.auditlog");

            entity.ToTable("auditlog", "users");

            entity.Property(e => e.AuditId)
                .ValueGeneratedNever()
                .HasColumnName("AuditID");
            entity.Property(e => e.ActionDetails)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ActionType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifedAt).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
