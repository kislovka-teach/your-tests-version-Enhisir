using Api.Models;
using Api.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class UserRepository(ApplicationContext dbContext) : IUserRepository
{
    public async Task<User?> GetUserByUserNameAsync(string username)
    {
        return await dbContext.Users
            .SingleOrDefaultAsync(u => username.Equals(u.UserName));
    }

    public async Task RegisterUserAsync(User user)
    {
        switch (user.Role)
        {
            case Role.Patient:
                await RegisterPatientAsync((Patient)user);
                break;
            case Role.Doctor:
                await RegisterDoctorAsync((Doctor)user);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(user.Role));
        }
    }
    
    public async Task RegisterPatientAsync(Patient patient)
    {
        await dbContext.Patients.AddAsync(patient);
    }

    public async Task RegisterDoctorAsync(Doctor doctor)
    {
        await dbContext.Doctors.AddAsync(doctor);
    }
}