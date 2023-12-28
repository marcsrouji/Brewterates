using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Brewterates.Domain.Entities;

public partial class brewteratesDbContext : DbContext
{
    public brewteratesDbContext()
    {
    }

    public brewteratesDbContext(DbContextOptions<brewteratesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Beer> Beers { get; set; }

    public virtual DbSet<Brewery> Breweries { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<Wholesaler> Wholesalers { get; set; }

    public virtual DbSet<WholesalerBeerList> WholesalerBeerLists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Beer>(entity =>
        {
            entity.ToTable("Beer");

            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

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
