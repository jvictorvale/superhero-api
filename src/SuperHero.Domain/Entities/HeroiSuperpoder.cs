namespace SuperHero.Domain.Entities;

public class HeroisSuperpoderes
{
    public int HeroiId { get; set; }
    public int SuperpoderId { get; set; }
    
    public Heroi Heroi { get; set; }
    public Superpoder Superpoder { get; set; }
}