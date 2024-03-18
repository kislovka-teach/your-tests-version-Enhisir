using System.Security.Claims;
using Api.Dtos;
using Api.Models;
using Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Api.EndpointGroups;

public static class AuthGroup
{
    
    public static RouteGroupBuilder MapAuthentication(this RouteGroupBuilder group)
    {
        group.MapPost("login", Login);
        group.MapPost("register/patient", RegisterPatient);
        group.MapPost("register/doctor", RegisterDoctor);
        return group;
    }

    private static async Task Login(
        HttpContext context,
        [FromBody] LoginDto dto,
        IUserRepository userService, 
        IPasswordHasherService passwordHasherService)
    {
        var user = await userService.GetUserByUserNameAsync(dto.UserName);
        if (user is not null 
            && passwordHasherService.Validate(user.PasswordHashed, dto.Password))
        {
            var usernameClaim = new Claim(ClaimTypes.Name, user.UserName);
            var roleClaim = new Claim(ClaimTypes.Role, user.Role.ToString());

            var userIdentity = new ClaimsIdentity(
                new []{ usernameClaim, roleClaim },
                "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(userIdentity);
            
            await context.SignInAsync(claimsPrincipal);
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var customResponse = new
            {
                status = 401,
                message = "Unauthorized. Please Provide Valid Credentials"
            };

            await context.Response.WriteAsJsonAsync(customResponse);
        }
    }
    
    private static async Task RegisterPatient(
        HttpContext context,
        [FromBody] RegisterPatientDto dto,
        IMapper mapper,
        IUserRepository userService, 
        IPasswordHasherService passwordHasherService)
    {
        var patient = mapper.Map<Patient>(dto);

        if (patient is null)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            return;
        }

        patient.PasswordHashed = passwordHasherService.Hash(patient.PasswordHashed);
        await userService.RegisterUserAsync(patient);
        context.Response.StatusCode = StatusCodes.Status201Created;
    }
    
    private static async Task RegisterDoctor(
        HttpContext context,
        [FromBody] RegisterDoctorDto dto,
        IMapper mapper,
        IUserRepository userService, 
        IPasswordHasherService passwordHasherService)
    {
        var patient = mapper.Map<Doctor>(dto);

        if (patient is null)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            return;
        }

        patient.PasswordHashed = passwordHasherService.Hash(patient.PasswordHashed);
        await userService.RegisterUserAsync(patient);
        context.Response.StatusCode = StatusCodes.Status201Created;
    }
}