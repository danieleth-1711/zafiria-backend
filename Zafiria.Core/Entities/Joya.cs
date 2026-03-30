namespace Zafiria.Core.Entities;

public class Joya
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public string Material { get; set; } = string.Empty;
    public bool Disponible { get; set; } = true;
    public int Stock { get; set; } = 0;
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }

    public int OcasionId { get; set; }
    public Ocasion? Ocasion { get; set; }

    public ICollection<ImagenJoya> Imagenes { get; set; } = new List<ImagenJoya>();
    public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}