using Api.Models.Enums;

namespace Api.Models;

public class Doctor : User
{
    public new Role Role => Role.Doctor;
    
    public int SpecializationId { get; set; }
    public Specialization Specialization { get; set; } = null!;
}