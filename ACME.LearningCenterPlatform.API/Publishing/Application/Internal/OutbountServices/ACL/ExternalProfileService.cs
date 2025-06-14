using ACME.LearningCenterPlatform.API.Profiles.Interfaces.ACL;

namespace ACME.LearningCenterPlatform.API.Publishing.Application.Internal.OutbountServices.ACL;

public class ExternalProfileService(IProfilesContextFacade profilesContextFacade)
{

    public async Task<int> CreateProfile(string firstName, string lastName, string email, string street, string number,
        string city,
        string postalCode, string country)
    {
        return await profilesContextFacade.CreateProfile(firstName, lastName, email, street, number, city, postalCode, country);
    }
    
    public async Task<int> FetchProfileIdByEmail(string email)
    {
        return await profilesContextFacade.FetchProfileIdByEmail(email);
    }

}