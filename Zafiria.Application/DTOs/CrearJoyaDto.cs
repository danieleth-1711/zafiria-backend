namespace Zafiria.Application.DTOs;

public class CrearJoyaDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public string Material { get; set; } = string.Empty;
    public int Stock { get; set; }
    public int CategoriaId { get; set; }
    public int OcasionId { get; set; }
}