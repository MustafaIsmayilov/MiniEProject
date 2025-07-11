﻿namespace MiniEProject.Domain.Entities;

public class Review : BaseEntity
{
    public string Content { get; set; } = string.Empty!;

    public int Rating { get; set; }

    public Guid ProductId { get; set; }
    public Product? Product { get; set; }

    public string? UserId { get; set; }
    public AppUser? User { get; set; }
}
