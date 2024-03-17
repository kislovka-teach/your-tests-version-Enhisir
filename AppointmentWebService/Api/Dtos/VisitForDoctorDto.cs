using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class VisitForDoctorDto
{
    [MaxLength(2000)]
    public string Finding { get; set; }
}