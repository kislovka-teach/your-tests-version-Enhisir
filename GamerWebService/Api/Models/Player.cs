using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class Player
{
    [Key]
    public string UserName { get; set; }
    
    public string PasswordHashed { get; set; }

    public Role Role { get; set; } = Role.User;
}