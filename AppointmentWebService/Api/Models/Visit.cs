namespace Api.Models;

public class Visit
{
    public int Id { get; set; }
    
    public DateTime Date { get; set; }
    
    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; }
    
    public int PatientId { get; set; }
    public Doctor Patient { get; set; }
    
    public Review? Review { get; set; }
}