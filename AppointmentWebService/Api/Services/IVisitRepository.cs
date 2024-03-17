using Api.Models;

namespace Api.Services;

public interface IVisitRepository
{
    public Task<List<Visit>?> GetVisitsByPatientAsync(string patientUserName);
    public Task<Visit?> GetConcreteVisitAsync(int id);
    public void AddVisit(Visit visit);
    public void UpdateVisit(Visit visit);
}