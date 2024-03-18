using Microsoft.IdentityModel.Tokens;

namespace Api;

public static class AuthOptions
{
    public static string Issuer => "GamerWebService";
    
    public static string Audience => "Gamer";

    public static DateTime Expires => DateTime.UtcNow.Add(TimeSpan.FromDays(7));

    public static SecurityKey SecurityKey { get; } = new SymmetricSecurityKey(
        "smack_my_ass2595F2D1A9F724EC46D2B374709AFEFC"u8.ToArray());
}