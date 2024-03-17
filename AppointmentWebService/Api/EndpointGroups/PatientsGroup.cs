using System.Security.Claims;
using Api.Dtos;
using Api.Models.Enums;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.EndpointGroups;

public static class PatientsGroup
{
    public static RouteGroupBuilder MapPatients(this RouteGroupBuilder group)
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

            if (role is null || !role.Value.Equals(Role.Doctor.ToString()))
            {
                return Results.Forbid();
            }

            return await next(invocationContext);
        });

        group.MapGet("", GetPatientsByDoctor);
        group.MapGet("{patientUserName}/visits", GetPatientVisits);
        group.MapPatch("{patientUserName}/visits/{visitId:int}/accept", AcceptVisit);
        
        return group;
    }

    private static async Task GetPatientsByDoctor(
        HttpContext context,
        IPatientRepository patientRepository,
        IDoctorRepository doctorRepository)
    {
        var userName = context.User.FindFirst(ClaimTypes.Role)!.Value;
        
        var doctor = await doctorRepository.GetConcreteDoctorAsync(userName);
        if (doctor is null)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return;
        }
        
        var patients = await patientRepository
            .GetPatientsByDoctorAsync(userName);
        await context.Response.WriteAsJsonAsync(patients);
        context.Response.StatusCode = StatusCodes.Status200OK;
    }

    private static async Task GetPatientVisits(
        string patientUserName,
        HttpContext context,
        IVisitRepository visitRepository)
    {
        var visits = await visitRepository
            .GetVisitsByPatientAsync(patientUserName);

        if (visits is null)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return;
        }

        await context.Response.WriteAsJsonAsync(visits);
        context.Response.StatusCode = StatusCodes.Status200OK;
    }
    
    private static async Task AcceptVisit(
        string patientUserName,
        int visitId,
        HttpContext context,
        [FromBody] VisitForDoctorDto dto,
        IVisitRepository visitRepository)
    {
        var visit = await visitRepository
            .GetConcreteVisitAsync(visitId);
        
        if (visit is null)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return;
        }

        visit.IsSuccessful = true;
        visit.Finding = dto.Finding;
        visitRepository.UpdateVisit(visit);
        context.Response.StatusCode = StatusCodes.Status200OK;
    }
}