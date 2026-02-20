namespace SuperHero.Application.DTOs.Heroi;

public class AtualizarHeroiDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string NomeHeroi { get; set; }
    public DateTime? DataNascimento { get; set; }
    public float Altura { get; set; }
    public float Peso { get; set; }
    public List<int> SuperpoderId { get; set; }
}