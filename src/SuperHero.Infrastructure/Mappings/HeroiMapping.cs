using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperHero.Domain.Entities;

namespace SuperHero.Infrastructure.Mappings;

public class HeroiMapping : IEntityTypeConfiguration<Heroi>
{
    public void Configure(EntityTypeBuilder<Heroi> builder)
    {
        builder
            .Property(h => h.Nome)
            .HasMaxLength(120)
            .IsRequired();

        builder
            .Property(h => h.NomeHeroi)
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(h => h.Altura)
            .HasColumnType("float")
            .IsRequired();
        
        builder.Property(h => h.Peso)
            .HasColumnType("float")
            .IsRequired();
    }
}