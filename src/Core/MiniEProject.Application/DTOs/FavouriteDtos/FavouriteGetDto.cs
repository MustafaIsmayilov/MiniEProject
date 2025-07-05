namespace MiniEProject.Application.DTOs.FavouriteDtos;

public class FavouriteGetDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string ProductImage { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
}

