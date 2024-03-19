using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class VisitRepository(ApplicationContext dbContext) : IVisitRepository
{
    public async Task<List<Visit>?> GetVisitsByPatientAsync(string patientUserName)
    {
        var patient = await dbContext.Patients
            .Include(p => p.Visits)
            .SingleOrDefaultAsync(p => patientUserName.Equals(p.UserName));

        return patient?.Visits;
    }

    public async Task<Visit?> GetConcreteVisitAsync(int id)
        => await dbContext.Visits.SingleOrDefaultAsync(v => id == v.Id);

    public void AddVisit(Visit visit)
    {
        dbContext.Visits.Add(visit);
        dbContext.SaveChanges();
    }

    public void UpdateVisit(Visit visit)
    {
        dbContext.Visits.Update(visit);
        dbContext.SaveChanges();
    }
}