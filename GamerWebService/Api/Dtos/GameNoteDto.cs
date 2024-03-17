namespace Api.Dtos;

public class GameNoteDto
{
    public int GameId { get; set; }
    public bool Completed { get; set; }
    public bool Favourite { get; set; }
    public bool PlayLater { get; set; }
}