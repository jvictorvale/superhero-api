using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHero.Application.DTOs.Heroi;
using SuperHero.Application.Interfaces;
using SuperHero.Application.Notifications;
using Swashbuckle.AspNetCore.Annotations;

namespace SuperHero.Api.Controllers;

[Route("api/[controller]")]
public class HeroiController : BaseController
{
    private readonly IHeroiService _heroiService;
    
    public HeroiController(INotificator notificator, IHeroiService heroiService) : base(notificator)
    {
        _heroiService = heroiService;
    }
    
    [HttpPost]
    [SwaggerOperation(Summary = "Criar um Herói", Tags = new[] { "Heroi" })]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Criar([FromBody] AdicionarHeroiDto heroiDto)
    {
        return OkResponse(await _heroiService.Criar(heroiDto));
    }
    
    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Atualizar um Herói", Tags = new[] { "Heroi" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarHeroiDto heroiDto)
    {
        return OkResponse(await _heroiService.Atualizar(id, heroiDto));
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Deletar um Herói", Tags = new[] { "Heroi" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Deletar(int id)
    {
        var sucesso = await _heroiService.Deletar(id);
        if (!sucesso) return BadRequest();

        return NoContent();
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Obter todos os Heróis", Tags = new[] { "Heroi" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> ObterTodos()
    {
        var resultado = await _heroiService.ObterTodos();
        
        if (resultado == null)
        {
            return NotFound(new { mensagem = "Não existe herói cadastrado" });
        }

        return OkResponse(resultado);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Obter um Herói por id", Tags = new[] { "Heroi" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> ObterPorId(int id)
    {
        return OkResponse(await _heroiService.ObterPorId(id));
    }
}