namespace Zafiria.Application.DTOs;

public class ReservaDto
{
    public int Id { get; set; }
    public string NombreCliente { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime FechaReserva { get; set; }
    public string Estado { get; set; } = string.Empty;
    public string Notas { get; set; } = string.Empty;
    public string JoyaNombre { get; set; } = string.Empty;
}
