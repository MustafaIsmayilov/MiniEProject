namespace MiniEProject.Application.DTOs.FavouriteDtos;

public class FavouriteCreateDto
{
    public string UserId { get; set; } = null!;
    public Guid ProductId { get; set; }
}