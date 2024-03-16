using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class Review
{
    [Range(1, 5)]
    public int Mark { get; set; }
    
    public int AppointmentId { get; set; }
    public Appointment Appointment { get; set; }
}