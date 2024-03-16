using Models.Enums;

namespace Models;

public class User
{
    public int Id { get; set; }
    
    public string Login { get; set; }
    public string PasswordHashed { get; set; }
    
    public string Name { get; set; }
    public string Surname { get; set; }
    
    public Role Role { get; set; }
}