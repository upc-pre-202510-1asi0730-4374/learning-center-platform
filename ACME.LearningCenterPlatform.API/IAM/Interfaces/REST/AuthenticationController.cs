using System.Net.Mime;
using ACME.LearningCenterPlatform.API.IAM.Domain.Services;
using ACME.LearningCenterPlatform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using ACME.LearningCenterPlatform.API.IAM.Interfaces.REST.Resources;
using ACME.LearningCenterPlatform.API.IAM.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ACME.LearningCenterPlatform.API.IAM.Interfaces.REST;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Authentication Endpoints")]
public class AuthenticationController(IUserCommandService userCommandService): ControllerBase
{

    [AllowAnonymous]
    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInResource signInResource)
    {
        var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(signInResource);
        var authenticatedUser = await userCommandService.Handle(signInCommand);

        var resource = AuthenticatedUserResourceFromEntityAssembler
                                .ToResourceFromEntity(authenticatedUser.user, authenticatedUser.token);
        
        return Ok(resource);
    }


    [AllowAnonymous]
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource signUpResource)
    {
        var signUpCommand = SignUpCommandFromResourceAssembler.ToCommandFromResource(signUpResource);
        await userCommandService.Handle(signUpCommand);
        return Ok(new { message = "User created successfully" });
    }


}