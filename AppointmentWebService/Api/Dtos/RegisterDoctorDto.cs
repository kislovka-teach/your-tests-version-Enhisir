using Api.Models;

namespace Api.Dtos;

public class RegisterDoctorDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public Specialization SpecializationDto { get; set; }
}