namespace Zafiria.Core.Entities;

public class Reserva
{
    public int Id { get; set; }
    public string NombreCliente { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime FechaReserva { get; set; } = DateTime.UtcNow;
    public string Estado { get; set; } = "Pendiente";
    public string Notas { get; set; } = string.Empty;

    public int JoyaId { get; set; }
    public Joya? Joya { get; set; }
}