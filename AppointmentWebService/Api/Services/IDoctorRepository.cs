using Api.Models;

namespace Api.Services;

public interface IDoctorRepository
{
    public Task<List<Doctor>> GetDoctorsAsync(Specialization? specialization = null);
    public Task<Doctor?> GetConcreteDoctorAsync(string username);
}