using Api.Dtos;
using Api.Models;
using Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Groups;

public static class CompaniesGroup
{
    public static RouteGroupBuilder MapCompanies(this RouteGroupBuilder group)
    {
        group.MapPost("", AddCompany).AddEndpointFilter<AdminEndpointFilter>();

        return group;
    }

    private static Task AddCompany(
        HttpContext context,
        [FromBody] CompanyDto dto,
        IMapper mapper,
        ICompanyRepository companyRepository)
    {
        var company = mapper.Map<Company>(dto);
        companyRepository.AddCompany(company);
        
        return Task.CompletedTask;
    }
}