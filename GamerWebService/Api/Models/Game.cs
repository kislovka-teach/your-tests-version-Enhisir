using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class Game
{
    public int Id { get; set; }
    public string Title { get; set; }
    
    [Range(1965, double.MaxValue)]
    public int Year { get; set; }
    
    public int DeveloperId { get; set; }
    public Company Developer { get; set; } = null!;

    public int PublisherId { get; set; }
    public Company Publisher { get; set; }  = null!;
}