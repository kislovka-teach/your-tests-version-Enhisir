using Api.Models;

namespace Api.Services;

public interface ICompanyRepository
{
    public Task<Company?> GetCompanyAsync(int id);
    public void AddCompany(Company company);
}