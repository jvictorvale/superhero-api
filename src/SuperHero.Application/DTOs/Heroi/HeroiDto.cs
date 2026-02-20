using SuperHero.Application.DTOs.HeroisSuperpoderes;

namespace SuperHero.Application.DTOs.Heroi;

public class HeroiDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string NomeHeroi { get; set; }
    public DateTime? DataNascimento { get; set; }
    public float Altura { get; set; }
    public float Peso { get;  set; }
    public List<HeroisSuperpoderesDto> Superpoderes { get; set; }
}