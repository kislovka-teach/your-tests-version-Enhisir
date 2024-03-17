using System.Security.Claims;
using Api.Dtos;
using Api.Models;
using Api.Models.Enums;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.EndpointGroups;

public static class DoctorsGroup
{
    public static RouteGroupBuilder MapDoctors(this RouteGroupBuilder group)
    {
        group.AddEndpointFilter(async (invocationContext, next) => {
            var identity = invocationContext.HttpContext.User.Identity;

            return identity is null 
                ? Results.Unauthorized() 
                : await next(invocationContext);
        });
        
        group.AddEndpointFilter(async (invocationContext, next) => {
            var role = invocationContext.HttpContext.User
                .FindFirst(ClaimTypes.Role);

            if (role is null || !role.Value.Equals(Role.Patient.ToString()))
            {
                return Results.Forbid();
            }

            return await next(invocationContext);
        });
        
        group.MapGet("/visits", GetVisits);
        
        group.MapGet("", GetDoctors);
        group.MapGet("{doctorUserName}", GetConcreteDoctor);
        group.MapPost("{doctorUserName}/newVisit", AddVisit);
        
        return group;
    }
    
    private static async Task GetVisits(
        HttpContext context,
        IVisitRepository visitRepository)
    {
        var userName = context.User
            .FindFirst(ClaimTypes.Role)!.Value;

        var visits = await visitRepository
            .GetVisitsByPatientAsync(userName);
        
        await context.Response.WriteAsJsonAsync(visits);
        context.Response.StatusCode = StatusCodes.Status200OK;
    }

    private static async Task GetDoctors(
        HttpContext context,
        [FromQuery(Name = "spec")] int? specializationId,
        IDoctorRepository doctorRepository,
        ISpecializationRepository specializationRepository)
    {
        var specialization = 
            specializationId is null 
                ? null
                : await specializationRepository
                    .GetConcreteSpecialization(specializationId.Value);
        
        var patients = await doctorRepository
            .GetDoctorsAsync(specialization);
        
        await context.Response.WriteAsJsonAsync(patients);
        context.Response.StatusCode = StatusCodes.Status200OK;
    }
    
    private static async Task GetConcreteDoctor(
        string doctorUserName,
        HttpContext context,
        IDoctorRepository doctorRepository)
    {
        var doctor = await doctorRepository
            .GetConcreteDoctorAsync(doctorUserName);
        if (doctor is null)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return;
        }
        
        await context.Response.WriteAsJsonAsync(doctor);
        context.Response.StatusCode = StatusCodes.Status200OK;
    }
    
    private static async Task AddVisit(
        string doctorUserName,
        HttpContext context,
        [FromBody] VisitDto dto,
        IDoctorRepository doctorRepository,
        IVisitRepository visitRepository)
    {
        var userName = context.User
            .FindFirst(ClaimTypes.Role)!.Value;
        
        var doctor = await doctorRepository
                .GetConcreteDoctorAsync(doctorUserName);
        if (doctor is null)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return;
        }

        var visit = new Visit()
        {
            PatientId = userName,
            Doctor = doctor,
            Date = dto.Date
        };
        visitRepository.AddVisit(visit);
        context.Response.StatusCode = StatusCodes.Status201Created;
    }
}