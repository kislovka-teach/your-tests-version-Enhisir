using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class CompanyRepository(ApplicationContext dbContext): ICompanyRepository
{
    public async Task<Company?> GetCompanyAsync(int id)
    {
        return await dbContext.Companies.SingleOrDefaultAsync(c => id == c.Id);
    }

    public void AddCompany(Company company)
    {
        dbContext.Companies.Add(company);
        dbContext.SaveChanges();
    }
}