using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class Review
{
    public int Id { get; set; }
    
    [Range(1, 5)]
    public int Mark { get; set; }

    public Visit Visit { get; set; } = null!;
}