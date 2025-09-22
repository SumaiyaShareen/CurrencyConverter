using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Models;

public partial class CurrencyConverterContext : DbContext
{
    public CurrencyConverterContext()
    {
    }

    public CurrencyConverterContext(DbContextOptions<CurrencyConverterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ConversionHistory> ConversionHistories { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<ExchangeRate> ExchangeRates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\MSSQLSERVER01;Database=CurrencyConverter;Trusted_Connection=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConversionHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__Conversi__4D7B4ABD67602D4F");

            entity.ToTable("ConversionHistory");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ConversionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ConvertedAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FromCurrency)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.RateUsed).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.ToCurrency)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.FromCurrencyNavigation).WithMany(p => p.ConversionHistoryFromCurrencyNavigations)
                .HasForeignKey(d => d.FromCurrency)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConversionHistory_From");

            entity.HasOne(d => d.ToCurrencyNavigation).WithMany(p => p.ConversionHistoryToCurrencyNavigations)
                .HasForeignKey(d => d.ToCurrency)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConversionHistory_To");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.CurrencyCode).HasName("PK__Currenci__408426BEF9DA4C3A");

            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CurrencyName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Symbol).HasMaxLength(10);
        });

        modelBuilder.Entity<ExchangeRate>(entity =>
        {
            entity.HasKey(e => e.RateId).HasName("PK__Exchange__58A7CF5C853DE353");

            entity.Property(e => e.BaseCurrency)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Rate).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.TargetCurrency)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.BaseCurrencyNavigation).WithMany(p => p.ExchangeRateBaseCurrencyNavigations)
                .HasForeignKey(d => d.BaseCurrency)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExchangeRates_Base");

            entity.HasOne(d => d.TargetCurrencyNavigation).WithMany(p => p.ExchangeRateTargetCurrencyNavigations)
                .HasForeignKey(d => d.TargetCurrency)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExchangeRates_Target");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
