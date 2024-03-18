using System.ComponentModel.DataAnnotations;
using Api.Models.Enums;
namespace Api.Models;

public class User
{
    [Key]
    public string UserName { get; set; }
    public string PasswordHashed { get; set; }
    
    public string Name { get; set; }
    public string Surname { get; set; }
    
    public Role Role { get; set; }
    
}