using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class SpecializationRepository(ApplicationContext dbContext) : ISpecializationRepository
{
    public async Task<Specialization?> GetConcreteSpecialization(int id)
    {
        return await dbContext.Specializations.SingleOrDefaultAsync(s => id == s.Id);
    }
}