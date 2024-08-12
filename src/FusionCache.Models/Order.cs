namespace FusionCache.Models;

public class Order
{
    public int Id { get; set; }

    public string Description { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public DateTime LastUpdateTime { get; set; }


}
