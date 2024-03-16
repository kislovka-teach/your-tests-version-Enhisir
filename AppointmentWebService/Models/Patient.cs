using Models.Enums;

namespace Models;

public class Patient : User
{
    public new Role Role => Role.Patient;
}