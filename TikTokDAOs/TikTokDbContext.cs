using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TikTokDAOs.Entities;

namespace TikTokDAOs;

public partial class TikTokDbContext : DbContext
{
    public TikTokDbContext()
    {
    }

    public TikTokDbContext(DbContextOptions<TikTokDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:DefaultConnectionStringDB"];
        return strConn;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3213E83F34D0B52A");

            entity.ToTable("Account");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Avatar)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("avatar");
            entity.Property(e => e.Contact)
                .HasMaxLength(255)
                .HasColumnName("contact");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Followed).HasColumnName("followed");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .HasColumnName("fullName");
            entity.Property(e => e.Liked).HasColumnName("liked");
            entity.Property(e => e.NickName)
                .HasMaxLength(50)
                .HasColumnName("nickName");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Video__3213E83F40487D80");

            entity.ToTable("Video");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Commented).HasColumnName("commented");
            entity.Property(e => e.IdAccount).HasColumnName("idAccount");
            entity.Property(e => e.Liked).HasColumnName("liked");
            entity.Property(e => e.Shared).HasColumnName("shared");
            entity.Property(e => e.SrcVideo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("srcVideo");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.IdAccountNavigation).WithMany(p => p.Videos)
                .HasForeignKey(d => d.IdAccount)
                .HasConstraintName("PK_IDACCOUNT");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
