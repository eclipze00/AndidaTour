using AndidaTour.API.Entities;
using AndidaTour.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AndidaTour.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuotesController : BaseController
{
    private readonly IQuoteRepository _quotes;
    private readonly IClientRepository _clients;

    public QuotesController(IQuoteRepository quotes, IClientRepository clients)
    {
        _quotes = quotes;
        _clients = clients;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var quotes = await _quotes.GetAllByUserAsync(CurrentUserId);
        var result = quotes.Select(q => new QuoteResponse(
            q.Id,
            q.Client.Name,
            q.FromCode,
            q.ToCode,
            q.TravelDate.ToString("yyyy-MM-dd"),
            q.BestPrice,
            q.Status.ToString().ToLower(),
            q.CreatedAt.ToString("yyyy-MM-dd"),
            q.FlightDataJson
        ));
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var q = await _quotes.GetByIdAsync(id, CurrentUserId);
        if (q == null) return NotFound();
        return Ok(new QuoteResponse(
            q.Id,
            q.Client.Name,
            q.FromCode,
            q.ToCode,
            q.TravelDate.ToString("yyyy-MM-dd"),
            q.BestPrice,
            q.Status.ToString().ToLower(),
            q.CreatedAt.ToString("yyyy-MM-dd"),
            q.FlightDataJson
        ));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateQuoteRequest req)
    {
        // Verifica se o cliente pertence ao usuário logado
        var client = await _clients.GetByIdAsync(req.ClientId, CurrentUserId);
        if (client == null)
            return BadRequest(new { message = "Cliente não encontrado." });

        var quote = new FlightQuoteEntity
        {
            FromCode        = req.FromCode,
            ToCode          = req.ToCode,
            TravelDate      = DateTime.SpecifyKind(          // ← converte com UTC
                                DateTime.Parse(req.TravelDate),
                                DateTimeKind.Utc),
            BestPrice       = req.BestPrice,
            ClientId        = req.ClientId,
            FlightDataJson  = req.FlightDataJson,
            Status          = QuoteStatusEntity.Rascunho,
            UserId          = CurrentUserId
        };

        var created = await _quotes.CreateAsync(quote);

        // Recarrega com o cliente incluído
        var full = await _quotes.GetByIdAsync(created.Id, CurrentUserId);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, new QuoteResponse(
            full!.Id,
            full.Client.Name,
            full.FromCode,
            full.ToCode,
            full.TravelDate.ToString("yyyy-MM-dd"),
            full.BestPrice,
            full.Status.ToString().ToLower(),
            full.CreatedAt.ToString("yyyy-MM-dd"),
            full.FlightDataJson
        ));
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusRequest req)
    {
        var updated = await _quotes.UpdateStatusAsync(id, CurrentUserId, req.Status);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _quotes.DeleteAsync(id, CurrentUserId);
        return deleted ? NoContent() : NotFound();
    }
}

public record CreateQuoteRequest(
    string FromCode,
    string ToCode,
    string TravelDate,
    decimal BestPrice,
    int ClientId,
    string? FlightDataJson
);

public record UpdateStatusRequest(QuoteStatusEntity Status);

public record QuoteResponse(
    int Id,
    string ClientName,
    string From,
    string To,
    string TravelDate,
    decimal BestPrice,
    string Status,
    string CreatedAt,
    string? FlightDataJson
);