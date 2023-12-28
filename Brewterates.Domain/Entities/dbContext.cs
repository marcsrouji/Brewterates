using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Brewterates.Domain.Entities;

public partial class dbContext : DbContext
{
    public dbContext()
    {
    }

    public dbContext(DbContextOptions<dbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Beer> Beers { get; set; }

    public virtual DbSet<Brewery> Breweries { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<Wholesaler> Wholesalers { get; set; }

    public virtual DbSet<WholesalerBeerList> WholesalerBeerLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-A08PTV9\\SQLEXPRESS;Initial Catalog=Brewterates;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Beer>(entity =>
        {
            entity.ToTable("Beer");

            entity.Property(e => e.AlcoholIntent).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Brewery).WithMany(p => p.Beers)
                .HasForeignKey(d => d.BreweryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Beer_Brewery");
        });

        modelBuilder.Entity<Brewery>(entity =>
        {
            entity.ToTable("Brewery");

            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.ToTable("Stock");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");

            entity.HasOne(d => d.Beer).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.BeerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stock_Beer");

            entity.HasOne(d => d.Wholesaler).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.WholesalerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stock_Wholesaler");
        });

        modelBuilder.Entity<Wholesaler>(entity =>
        {
            entity.ToTable("Wholesaler");

            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<WholesalerBeerList>(entity =>
        {
            entity.ToTable("WholesalerBeerList");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");

            entity.HasOne(d => d.Beer).WithMany(p => p.WholesalerBeerLists)
                .HasForeignKey(d => d.BeerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WholesalerBeerList_Beer");

            entity.HasOne(d => d.Wholesaler).WithMany(p => p.WholesalerBeerLists)
                .HasForeignKey(d => d.WholesalerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WholesalerBeerList_Wholesaler");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
