using AndidaTour.API.Entities;
using AndidaTour.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AndidaTour.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : BaseController
{
    private readonly IClientRepository _clients;
    public ClientsController(IClientRepository clients) => _clients = clients;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var clients = await _clients.GetAllByUserAsync(CurrentUserId);
        var result = clients.Select(c => new {
            id = c.Id,
            name = c.Name,
            email = c.Email,
            phone = c.Phone,
            document = c.Document,
            preferences = c.Preferences,
            notes = c.Notes,
            quotes = c.Quotes?.Count ?? 0,
            createdAt = c.CreatedAt
        });
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var c = await _clients.GetByIdAsync(id, CurrentUserId);
        if (c == null) return NotFound();
        return Ok(new {
            id = c.Id,
            name = c.Name,
            email = c.Email,
            phone = c.Phone,
            document = c.Document,
            preferences = c.Preferences,
            notes = c.Notes,
            quotes = c.Quotes?.Count ?? 0
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateClientRequest req)
    {
        var client = new ClientEntity
        {
            Name = req.Name,
            Email = req.Email,
            Phone = req.Phone,
            Document = req.Document,
            Preferences = req.Preferences,
            Notes = req.Notes,
            UserId = CurrentUserId
        };
        var created = await _clients.CreateAsync(client);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _clients.DeleteAsync(id, CurrentUserId);
        return deleted ? NoContent() : NotFound();
    }
}

public record CreateClientRequest(
    string Name, string Email, string Phone,
    string Document, string? Preferences, string? Notes);