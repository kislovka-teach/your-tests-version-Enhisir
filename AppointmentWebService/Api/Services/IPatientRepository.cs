using Api.Models;

namespace Api.Services;

public interface IPatientRepository
{
    public Task<List<Patient>> GetPatientsByDoctorAsync(string doctorUserName);
    public Task<Patient?> GetConcretePatientAsync(string username, bool withVisits = false);
}