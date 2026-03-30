namespace Zafiria.Application.DTOs;

public class CrearReservaDto
{
    public string NombreCliente { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Notas { get; set; } = string.Empty;
    public int JoyaId { get; set; }
}