using Api.Models.Enums;

namespace Api.Models;

public class Patient : User
{
    public new Role Role => Role.Patient;

    public List<Visit> Visits { get; set; }
}