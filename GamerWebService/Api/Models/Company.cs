namespace Api.Models;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Game> Games;
}