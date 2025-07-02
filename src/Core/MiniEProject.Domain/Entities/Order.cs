namespace MiniEProject.Domain.Entities;

public class Order : BaseEntity
{

    public string? UserId { get; set; }
    public AppUser? User { get; set; }
    public decimal TotalPrice { get; }

    public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}
