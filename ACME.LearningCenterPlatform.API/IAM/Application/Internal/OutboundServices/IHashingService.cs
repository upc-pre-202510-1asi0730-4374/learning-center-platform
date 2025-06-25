namespace ACME.LearningCenterPlatform.API.IAM.Application.Internal.OutboundServices;

public interface IHashingService
{
    string HashPassword(string password);
    
    bool VerifyHashedPassword(string password, string passwordHash);
}