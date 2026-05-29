using AndidaTour.API.Entities;
using AndidaTour.API.Repositories;
using AndidaTour.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AndidaTour.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _users;
    private readonly JwtService _jwt;

    public AuthController(IUserRepository users, JwtService jwt)
    {
        _users = users;
        _jwt = jwt;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest req)
    {
        if (string.IsNullOrEmpty(req.Email))
            return BadRequest(new { message = "E-mail obrigatório." });

        var user = await _users.GetByEmailAsync(req.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
            return Unauthorized(new { message = "E-mail ou senha incorretos." });

        var token = _jwt.GenerateToken(user);

        return Ok(new
        {
            token,
            name = user.FirstName,
            userId = user.Id,
            role = user.Role.ToString().ToLower(), // ← adicionar
            expiresIn = 28800
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest req)
    {
        if (await _users.EmailExistsAsync(req.Email))
            return Conflict(new { message = "E-mail já cadastrado." });

        var user = new UserEntity
        {
            FirstName = req.FirstName,
            LastName = req.LastName,
            Email = req.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password),
            Role = req.Role?.ToLower() == "admin" ? UserRole.Admin : UserRole.Client // ← adicionar
        };

        var created = await _users.CreateAsync(user);
        var token = _jwt.GenerateToken(created);

        return Ok(new
        {
            token,
            name = created.FirstName,
            userId = created.Id,
            role = created.Role.ToString().ToLower(), // ← adicionar
            expiresIn = 28800
        });
    }

    // Atualizar os records:
    public record LoginRequest(string Email, string Password);
    public record RegisterRequest(string FirstName, string LastName, string Email, string Password, string? Role);

    // Endpoint para validar se o token ainda é válido
    [HttpGet("me")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public IActionResult Me()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                  ?? User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;
        var name = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
        var email = User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email)?.Value;

        return Ok(new { userId, name, email });
    }
}

public record LoginRequest(string Email, string Password);
public record RegisterRequest(string FirstName, string LastName, string Email, string Password);