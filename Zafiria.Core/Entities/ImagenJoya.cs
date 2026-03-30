namespace Zafiria.Core.Entities;

public class ImagenJoya
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public int Orden { get; set; } = 1;

    public int JoyaId { get; set; }
    public Joya? Joya { get; set; }
}