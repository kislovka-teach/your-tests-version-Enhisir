using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class PatientRepository(ApplicationContext dbContext) : IPatientRepository
{
    public async Task<List<Patient>> GetPatientsByDoctorAsync(string doctorUserName)
    {
        return await dbContext.Patients
            .Where(p => doctorUserName.Equals(p.UserName))
            .ToListAsync();
    }

    public async Task<Patient?> GetConcretePatientAsync(string username, bool withVisits = false)
    {
        var query = dbContext.Patients.AsQueryable();
        
        if (withVisits)
        {
            query = query.Include(p => p.Visits);
        }
        
        return await query
            .SingleOrDefaultAsync(p => username.Equals(p.UserName));
    }
}