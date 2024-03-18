using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class DoctorRepository(ApplicationContext dbContext) : IDoctorRepository
{
    public async Task<List<Doctor>> GetDoctorsAsync(Specialization? specialization = null)
    {
        return specialization is null
            ? await dbContext.Doctors.ToListAsync()
            : await dbContext.Doctors
                .Where(d => d.SpecializationId == specialization.Id)
                .ToListAsync();
    }

    public async Task<Doctor?> GetConcreteDoctorAsync(
        string username)
    {
        var query = dbContext.Doctors.AsQueryable();
        
        return await query
            .SingleOrDefaultAsync(d => username.Equals(d.UserName));
    }
}