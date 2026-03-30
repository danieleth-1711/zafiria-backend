using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zafiria.Application.DTOs;
using Zafiria.Infrastructure.Data;
using Zafiria.Core.Entities;

namespace Zafiria.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservasController : ControllerBase
{
    private readonly ZafiriaDbContext _context;

    public ReservasController(ZafiriaDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CrearReservaDto dto)
    {
        var joya = await _context.Joyas.FindAsync(dto.JoyaId);
        if (joya == null)
            return NotFound(ApiResponse<string>.Error("Joya no encontrada", 404));

        if (!joya.Disponible || joya.Stock <= 0)
            return BadRequest(ApiResponse<string>.Error("La joya no esta disponible", 400));

        var reserva = new Reserva
        {
            NombreCliente = dto.NombreCliente,
            Telefono = dto.Telefono,
            Email = dto.Email,
            Notas = dto.Notas,
            JoyaId = dto.JoyaId,
            FechaReserva = DateTime.UtcNow,
            Estado = "Pendiente"
        };

        joya.Stock--;
        if (joya.Stock == 0)
            joya.Disponible = false;

        _context.Reservas.Add(reserva);
        await _context.SaveChangesAsync();

        return StatusCode(201, ApiResponse<Reserva>.Success(reserva, "Reserva creada exitosamente", 201));
    }

    [HttpGet]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> GetAll()
    {
        var reservas = await _context.Reservas
            .Include(r => r.Joya)
            .Select(r => new ReservaDto
            {
                Id = r.Id,
                NombreCliente = r.NombreCliente,
                Telefono = r.Telefono,
                Email = r.Email,
                FechaReserva = r.FechaReserva,
                Estado = r.Estado,
                Notas = r.Notas,
                JoyaNombre = r.Joya!.Nombre
            })
            .ToListAsync();

        return Ok(ApiResponse<List<ReservaDto>>.Success(reservas, "Reservas obtenidas exitosamente"));
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> UpdateEstado(int id, [FromBody] string estado)
    {
        var reserva = await _context.Reservas.FindAsync(id);
        if (reserva == null)
            return NotFound(ApiResponse<string>.Error("Reserva no encontrada", 404));

        reserva.Estado = estado;
        await _context.SaveChangesAsync();

        return Ok(ApiResponse<string>.Success(estado, "Estado actualizado exitosamente"));
    }
}