using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class Visit
{
    public int Id { get; set; }
    
    public DateTime Date { get; set; }
    
    public string DoctorUserName { get; set; }
    public Doctor Doctor { get; set; }
    
    public string PatientUserName { get; set; }
    public Doctor Patient { get; set; }
    
    public bool IsSuccessful { get; set; }
    
    [MaxLength(2000)]
    public string? Finding { get; set; }
}