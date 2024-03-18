namespace Api.Groups;

public class AuthEndpointFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context, 
        EndpointFilterDelegate next)
    {
        var identity = context.HttpContext.User.Identity;
        return identity is null || !identity.IsAuthenticated
            ? Results.Unauthorized() 
            : await next(context);
    }
}