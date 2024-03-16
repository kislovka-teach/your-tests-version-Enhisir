namespace Models;

public class Appointment
{
    public DateTime Date { get; set; }
    
    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; }
    
    public int PatientId { get; set; }
    public Doctor Patient { get; set; }
}