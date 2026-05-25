using AndidaTour.API.Entities;
using AndidaTour.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AndidaTour.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlertsController : BaseController
{
    private readonly IAlertRepository _alerts;
    public AlertsController(IAlertRepository alerts) => _alerts = alerts;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _alerts.GetAllByUserAsync(CurrentUserId));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAlertRequest req)
    {
        var alert = new PriceAlertEntity
        {
            FromCode = req.FromCode,
            ToCode = req.ToCode,
            PeriodStart = req.PeriodStart,
            PeriodEnd = req.PeriodEnd,
            MaxPrice = req.MaxPrice,
            MilesProgram = req.MilesProgram,
            Channels = string.Join(",", req.Channels),
            UserId = CurrentUserId
        };
        var created = await _alerts.CreateAsync(alert);
        return Ok(created);
    }

    [HttpPatch("{id}/toggle")]
    public async Task<IActionResult> Toggle(int id)
    {
        var toggled = await _alerts.ToggleAsync(id, CurrentUserId);
        return toggled ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _alerts.DeleteAsync(id, CurrentUserId);
        return deleted ? NoContent() : NotFound();
    }
}

public record CreateAlertRequest(
    string FromCode, string ToCode,
    DateTime PeriodStart, DateTime PeriodEnd,
    decimal MaxPrice, string? MilesProgram,
    List<string> Channels);