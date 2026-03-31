using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zafiria.Application.DTOs;
using Zafiria.Application.Validators;
using Zafiria.Infrastructure.Data;
using Zafiria.Core.Entities;

namespace Zafiria.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JoyasController : ControllerBase
{
    private readonly ZafiriaDbContext _context;
    private readonly CrearJoyaDtoValidator _validator;

    public JoyasController(ZafiriaDbContext context, CrearJoyaDtoValidator validator)
    {
        _context = context;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var joyas = await _context.Joyas
            .Include(j => j.Categoria)
            .Include(j => j.Ocasion)
            .Where(j => j.Disponible)
            .Select(j => new JoyaDto
            {
                Id = j.Id,
                Nombre = j.Nombre,
                Descripcion = j.Descripcion,
                Precio = j.Precio,
                Material = j.Material,
                Disponible = j.Disponible,
                Stock = j.Stock,
                CategoriaNombre = j.Categoria!.Nombre,
                OcasionNombre = j.Ocasion!.Nombre,
                FechaCreacion = j.FechaCreacion
            })
            .ToListAsync();

        return Ok(ApiResponse<List<JoyaDto>>.Success(joyas, "Joyas obtenidas exitosamente"));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var joya = await _context.Joyas
            .Include(j => j.Categoria)
            .Include(j => j.Ocasion)
            .Where(j => j.Id == id)
            .Select(j => new JoyaDto
            {
                Id = j.Id,
                Nombre = j.Nombre,
                Descripcion = j.Descripcion,
                Precio = j.Precio,
                Material = j.Material,
                Disponible = j.Disponible,
                Stock = j.Stock,
                CategoriaNombre = j.Categoria!.Nombre,
                OcasionNombre = j.Ocasion!.Nombre,
                FechaCreacion = j.FechaCreacion
            })
            .FirstOrDefaultAsync();

        if (joya == null)
            return NotFound(ApiResponse<JoyaDto>.Error("Joya no encontrada", 404));

        return Ok(ApiResponse<JoyaDto>.Success(joya, "Joya obtenida exitosamente"));
    }

    [HttpGet("categoria/{categoriaId}")]
    public async Task<IActionResult> GetByCategoria(int categoriaId)
    {
        var joyas = await _context.Joyas
            .Include(j => j.Categoria)
            .Include(j => j.Ocasion)
            .Where(j => j.CategoriaId == categoriaId && j.Disponible)
            .Select(j => new JoyaDto
            {
                Id = j.Id,
                Nombre = j.Nombre,
                Descripcion = j.Descripcion,
                Precio = j.Precio,
                Material = j.Material,
                Disponible = j.Disponible,
                Stock = j.Stock,
                CategoriaNombre = j.Categoria!.Nombre,
                OcasionNombre = j.Ocasion!.Nombre,
                FechaCreacion = j.FechaCreacion
            })
            .ToListAsync();

        return Ok(ApiResponse<List<JoyaDto>>.Success(joyas, "Joyas por categoria obtenidas"));
    }

    [HttpGet("ocasion/{ocasionId}")]
    public async Task<IActionResult> GetByOcasion(int ocasionId)
    {
        var joyas = await _context.Joyas
            .Include(j => j.Categoria)
            .Include(j => j.Ocasion)
            .Where(j => j.OcasionId == ocasionId && j.Disponible)
            .Select(j => new JoyaDto
            {
                Id = j.Id,
                Nombre = j.Nombre,
                Descripcion = j.Descripcion,
                Precio = j.Precio,
                Material = j.Material,
                Disponible = j.Disponible,
                Stock = j.Stock,
                CategoriaNombre = j.Categoria!.Nombre,
                OcasionNombre = j.Ocasion!.Nombre,
                FechaCreacion = j.FechaCreacion
            })
            .ToListAsync();

        return Ok(ApiResponse<List<JoyaDto>>.Success(joyas, "Joyas por ocasion obtenidas"));
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Create([FromBody] CrearJoyaDto dto)
    {
        var validation = await _validator.ValidateAsync(dto);
        if (!validation.IsValid)
        {
            var errors = validation.Errors.Select(e => e.ErrorMessage).ToList();
            return BadRequest(ApiResponse<string>.Error("Validacion fallida", 400, errors));
        }

        var joya = new Joya
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            Precio = dto.Precio,
            Material = dto.Material,
            Stock = dto.Stock,
            Disponible = dto.Stock > 0,
            CategoriaId = dto.CategoriaId,
            OcasionId = dto.OcasionId,
            FechaCreacion = DateTime.UtcNow
        };

        _context.Joyas.Add(joya);
        await _context.SaveChangesAsync();

        return StatusCode(201, ApiResponse<Joya>.Success(joya, "Joya creada exitosamente", 201));
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Update(int id, [FromBody] CrearJoyaDto dto)
    {
        var validation = await _validator.ValidateAsync(dto);
        if (!validation.IsValid)
        {
            var errors = validation.Errors.Select(e => e.ErrorMessage).ToList();
            return BadRequest(ApiResponse<string>.Error("Validacion fallida", 400, errors));
        }

        var joya = await _context.Joyas.FindAsync(id);
        if (joya == null)
            return NotFound(ApiResponse<Joya>.Error("Joya no encontrada", 404));

        joya.Nombre = dto.Nombre;
        joya.Descripcion = dto.Descripcion;
        joya.Precio = dto.Precio;
        joya.Material = dto.Material;
        joya.Stock = dto.Stock;
        joya.Disponible = dto.Stock > 0;
        joya.CategoriaId = dto.CategoriaId;
        joya.OcasionId = dto.OcasionId;

        await _context.SaveChangesAsync();

        return Ok(ApiResponse<Joya>.Success(joya, "Joya actualizada exitosamente"));
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Delete(int id)
    {
        var joya = await _context.Joyas.FindAsync(id);
        if (joya == null)
            return NotFound(ApiResponse<Joya>.Error("Joya no encontrada", 404));

        _context.Joyas.Remove(joya);
        await _context.SaveChangesAsync();

        return Ok(ApiResponse<string>.Success("Eliminado", "Joya eliminada exitosamente"));
    }
}