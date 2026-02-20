using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperHero.Domain.Entities;

namespace SuperHero.Infrastructure.Mappings;

public class SuperpoderMapping : IEntityTypeConfiguration<Superpoder>
{
    public void Configure(EntityTypeBuilder<Superpoder> builder)
    {
        builder.Property(sp => sp.SuperPoder)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(sp => sp.Descricao)
            .HasMaxLength(250)
            .IsRequired(false);
    }
}