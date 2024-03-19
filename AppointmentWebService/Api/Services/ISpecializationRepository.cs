using Api.Models;

namespace Api.Services;

public interface ISpecializationRepository
{
    public Task<Specialization?> GetConcreteSpecialization(int id);
}