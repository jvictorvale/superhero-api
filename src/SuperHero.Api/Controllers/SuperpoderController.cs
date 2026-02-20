using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHero.Application.DTOs.Superpoder;
using SuperHero.Application.Interfaces;
using SuperHero.Application.Notifications;
using Swashbuckle.AspNetCore.Annotations;

namespace SuperHero.Api.Controllers;

[Route("api/[controller]")]
public class SuperpoderController : BaseController
{
    private readonly ISuperpoderService _superpoderService;
    
    public SuperpoderController(INotificator notificator, ISuperpoderService superpoderService) : base(notificator)
    {
        _superpoderService = superpoderService;
    }
    
    [HttpPost]
    [SwaggerOperation(Summary = "Criar um Superpoder", Tags = new[] { "Superpoder - Superpoderes" })]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Criar([FromBody] SuperpoderDto superpoderDto)
    {
        return OkResponse(await _superpoderService.Criar(superpoderDto));
    }
    
    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Atualizar um Superpoder", Tags = new[] { "Superpoder - Superpoderes" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Atualizar(int id, [FromBody] SuperpoderDto superpoderDto)
    {
        return OkResponse(await _superpoderService.Atualizar(id, superpoderDto));
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Deletar um Superpoder", Tags = new[] { "Superpoder - Superpoderes" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Deletar(int id)
    {
        await _superpoderService.Deletar(id);
        return NoContent();
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Obter todos os Superpoder", Tags = new[] { "Superpoder - Superpoderes" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> ObterTodos()
    {
        return OkResponse(await _superpoderService.ObterTodos());
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Obter um Superpoder", Tags = new[] { "Superpoder - Superpoderes" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> ObterPorId(int id)
    {
        return OkResponse(await _superpoderService.ObterPorId(id));
    }
}