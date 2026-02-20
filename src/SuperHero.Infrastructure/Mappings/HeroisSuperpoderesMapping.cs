using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperHero.Domain.Entities;

namespace SuperHero.Infrastructure.Mappings;

public class HeroisSuperpoderesMapping : IEntityTypeConfiguration<HeroisSuperpoderes>
{
    public void Configure(EntityTypeBuilder<HeroisSuperpoderes> builder)
    {
        builder.HasKey(hs => new { hs.HeroiId, hs.SuperpoderId });
        
        builder.HasOne(hs => hs.Heroi)
            .WithMany(h => h.HeroisSuperpoderes)
            .HasForeignKey(hs => hs.HeroiId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(hs => hs.Superpoder)
            .WithMany(sp => sp.HeroisSuperpoderes)
            .HasForeignKey(hs => hs.SuperpoderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}