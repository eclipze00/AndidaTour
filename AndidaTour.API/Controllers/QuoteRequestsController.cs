using AndidaTour.API.Entities;
using AndidaTour.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AndidaTour.API.Controllers;

[ApiController]
[Route("api/quote-requests")]
public class QuoteRequestsController : BaseController
{
    private readonly IQuoteRequestRepository _repo;
    public QuoteRequestsController(IQuoteRequestRepository repo) => _repo = repo;

    // ── ROTAS DO CLIENTE ────────────────────────────────────

    // Cliente lista suas próprias solicitações
    [HttpGet("mine")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> GetMine() =>
        Ok(await _repo.GetByClientAsync(CurrentUserId));

    // Cliente cria nova solicitação
    [HttpPost]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> Create([FromBody] CreateQuoteRequestDto dto)
    {
        var request = new QuoteRequestEntity
        {
            FromCode      = dto.FromCode,
            ToCode        = dto.ToCode,
            DepartureDate = DateTime.SpecifyKind(DateTime.Parse(dto.DepartureDate), DateTimeKind.Utc),
            ReturnDate    = dto.ReturnDate != null
                              ? DateTime.SpecifyKind(DateTime.Parse(dto.ReturnDate), DateTimeKind.Utc)
                              : null,
            Passengers    = dto.Passengers,
            TripType      = dto.TripType,
            CabinClass    = dto.CabinClass,
            Notes         = dto.Notes,
            ClientUserId  = CurrentUserId
        };

        var created = await _repo.CreateAsync(request);
        return CreatedAtAction(nameof(GetMine), new { id = created.Id }, MapToDto(created));
    }

    // Cliente cancela uma solicitação
    [HttpPatch("{id}/cancel")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> Cancel(int id)
    {
        var ok = await _repo.CancelAsync(id, CurrentUserId);
        return ok ? NoContent() : NotFound();
    }

    // ── ROTAS DO ADMIN ──────────────────────────────────────

    // Admin lista todas as solicitações
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var list = await _repo.GetAllAsync();
        return Ok(list.Select(MapToDtoWithClient));
    }

    // Admin responde / atualiza uma solicitação
    [HttpPatch("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateQuoteRequestDto dto)
    {
        var req = await _repo.GetByIdAsync(id);
        if (req == null) return NotFound();

        if (dto.Status != null)
            req.Status = Enum.Parse<QuoteRequestStatus>(dto.Status, true);
        if (dto.AdminPrice.HasValue)
            req.AdminPrice = dto.AdminPrice;
        if (dto.AdminNotes != null)
            req.AdminNotes = dto.AdminNotes;

        await _repo.UpdateAsync(req);
        return Ok(MapToDtoWithClient(req));
    }

    // ── Helpers ─────────────────────────────────────────────

    private static QuoteRequestResponseDto MapToDto(QuoteRequestEntity q) => new(
        q.Id, q.FromCode, q.ToCode,
        q.DepartureDate.ToString("yyyy-MM-dd"),
        q.ReturnDate?.ToString("yyyy-MM-dd"),
        q.Passengers, q.TripType, q.CabinClass, q.Notes,
        q.Status.ToString().ToLower(),
        q.AdminPrice, q.AdminNotes,
        q.CreatedAt.ToString("yyyy-MM-dd HH:mm"),
        null, null
    );

    private static QuoteRequestResponseDto MapToDtoWithClient(QuoteRequestEntity q) => new(
        q.Id, q.FromCode, q.ToCode,
        q.DepartureDate.ToString("yyyy-MM-dd"),
        q.ReturnDate?.ToString("yyyy-MM-dd"),
        q.Passengers, q.TripType, q.CabinClass, q.Notes,
        q.Status.ToString().ToLower(),
        q.AdminPrice, q.AdminNotes,
        q.CreatedAt.ToString("yyyy-MM-dd HH:mm"),
        q.ClientUser?.FirstName + " " + q.ClientUser?.LastName,
        q.ClientUser?.Email
    );
}

// DTOs
public record CreateQuoteRequestDto(
    string FromCode, string ToCode,
    string DepartureDate, string? ReturnDate,
    int Passengers, string TripType, string CabinClass, string? Notes
);

public record UpdateQuoteRequestDto(
    string? Status, decimal? AdminPrice, string? AdminNotes
);

public record QuoteRequestResponseDto(
    int Id, string FromCode, string ToCode,
    string DepartureDate, string? ReturnDate,
    int Passengers, string TripType, string CabinClass, string? Notes,
    string Status, decimal? AdminPrice, string? AdminNotes,
    string CreatedAt,
    string? ClientName, string? ClientEmail
);