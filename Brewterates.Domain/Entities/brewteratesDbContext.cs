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

    public virtual DbSet<Quote> Quotes { get; set; }

    public virtual DbSet<QuoteItem> QuoteItems { get; set; }

    public virtual DbSet<Wholesaler> Wholesalers { get; set; }

    public virtual DbSet<WholesalerBeerCatalog> WholesalerBeerCatalogs { get; set; }

    public virtual DbSet<WholesalerStock> WholesalerStocks { get; set; }

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

        modelBuilder.Entity<Quote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Order");

            entity.ToTable("Quote");

            entity.Property(e => e.ClientName).HasMaxLength(100);
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InitialTotalAmount).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 4)");

            entity.HasOne(d => d.Wholesaler).WithMany(p => p.Quotes)
                .HasForeignKey(d => d.WholesalerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Brewery");
        });

        modelBuilder.Entity<QuoteItem>(entity =>
        {
            entity.ToTable("QuoteItem");

            entity.HasOne(d => d.Quote).WithMany(p => p.QuoteItems)
                .HasForeignKey(d => d.QuoteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuoteItem_Quote");
        });

        modelBuilder.Entity<Wholesaler>(entity =>
        {
            entity.ToTable("Wholesaler");

            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<WholesalerBeerCatalog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_WholesalerBeerList");

            entity.ToTable("WholesalerBeerCatalog");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");

            entity.HasOne(d => d.Beer).WithMany(p => p.WholesalerBeerCatalogs)
                .HasForeignKey(d => d.BeerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WholesalerBeerList_Beer");

            entity.HasOne(d => d.Wholesaler).WithMany(p => p.WholesalerBeerCatalogs)
                .HasForeignKey(d => d.WholesalerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WholesalerBeerList_Wholesaler");
        });

        modelBuilder.Entity<WholesalerStock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Stock");

            entity.ToTable("WholesalerStock");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");

            entity.HasOne(d => d.Beer).WithMany(p => p.WholesalerStocks)
                .HasForeignKey(d => d.BeerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stock_Beer");

            entity.HasOne(d => d.Wholesaler).WithMany(p => p.WholesalerStocks)
                .HasForeignKey(d => d.WholesalerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stock_Wholesaler");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
