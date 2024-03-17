namespace Api.Models;

public class GameNote
{
    public string PlayerId { get; set; }
    public Player Player { get; set; } = null!;
    
    public int GameId { get; set; }
    public Game Game { get; set; } = null!;
    
    public bool Completed { get; set; }
    public bool Favourite { get; set; }
    public bool PlayLater { get; set; }
}