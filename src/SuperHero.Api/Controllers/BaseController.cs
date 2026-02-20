using Microsoft.AspNetCore.Mvc;
using SuperHero.Api.Responses;
using SuperHero.Application.Notifications;

namespace SuperHero.Api.Controllers;

public abstract class BaseController : Controller
{
    private readonly INotificator _notificator;

    protected BaseController(INotificator notificator)
    {
        _notificator = notificator;
    }

    protected IActionResult NoContentResponse() => CustomResponse(NoContent());

    protected IActionResult CreatedResponse(string uri = "", object? result = null) =>
        CustomResponse(Created(uri, result));

    protected IActionResult OkResponse(object? result = null) => CustomResponse(Ok(result));

    protected IActionResult CustomResponse(IActionResult objectResult)
    {
        if (OperacaoValida)
        {
            return objectResult;
        }

        if (_notificator.IsNotFoundResource)
        {
            return NotFound();
        }

        var response = new BadRequestResponse(_notificator.GetNotifications().ToList());
        return BadRequest(response);
    }

    private bool OperacaoValida => !(_notificator.HasNotification || _notificator.IsNotFoundResource);
}