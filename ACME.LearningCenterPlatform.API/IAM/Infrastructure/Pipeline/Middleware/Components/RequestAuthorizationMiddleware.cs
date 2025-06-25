using System.Net;
using ACME.LearningCenterPlatform.API.IAM.Application.Internal.OutboundServices;
using ACME.LearningCenterPlatform.API.IAM.Domain.Model.Queries;
using ACME.LearningCenterPlatform.API.IAM.Domain.Services;

namespace ACME.LearningCenterPlatform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes.Components;

public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(
        HttpContext context,
        IUserQueryService userQueryService,
        ITokenService tokenService
        )
    {
        Console.WriteLine("Entering InvokeAsync");
        
        var allowAnonymous = context.Request.HttpContext.GetEndpoint()!.Metadata
            .Any(m => m.GetType() == typeof(AllowAnonymousAttribute));
        
        Console.WriteLine($"Allow Anonymous: {allowAnonymous}");
        if (allowAnonymous)
        {
            Console.WriteLine("Skipping authorization");
            await next(context);
            return;
        }
        Console.WriteLine("Entering Authorization");
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        
        if (token == null) throw new Exception("Authorization header not found or invalid");

        var userId = await tokenService.ValidateToken(token);
        
        if (userId == null) throw new Exception("Invalid token");
        
        var getUserByIdQuery = new GetUserByIdQuery(userId.Value);

        var user = await userQueryService.Handle(getUserByIdQuery);
        
        Console.WriteLine("Successfully authorization. Updating Context ...");
        context.Items["User"] = user;
        Console.WriteLine("Continuing with Middleware Pipeline");
        
        await next(context);
    }
    
}