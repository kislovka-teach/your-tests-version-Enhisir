using Api.Models;

namespace Api.Groups;

public class AdminEndpointFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context, 
        EndpointFilterDelegate next)
    => context.HttpContext.User.IsInRole(Role.Admin.ToString())
        ? Results.Forbid() 
        : await next(context);
}