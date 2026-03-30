namespace Zafiria.Core.Entities;

public class Ocasion
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public ICollection<Joya> Joyas { get; set; } = new List<Joya>();
}