using Microsoft.IdentityModel.Tokens;

namespace Api;

public static class AuthOptions
{
    public static string Issuer => "GamerWebService";
    
    public static string Audience => "Gamer";

    public static DateTime Expires => DateTime.UtcNow.Add(TimeSpan.FromDays(7));

    public static SecurityKey SecurityKey { get; } = new SymmetricSecurityKey("smack_my_ass"u8.ToArray());
}