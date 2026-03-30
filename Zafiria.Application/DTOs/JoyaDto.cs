namespace Zafiria.Application.DTOs;

public class JoyaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public string Material { get; set; } = string.Empty;
    public bool Disponible { get; set; }
    public int Stock { get; set; }
    public string CategoriaNombre { get; set; } = string.Empty;
    public string OcasionNombre { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
}