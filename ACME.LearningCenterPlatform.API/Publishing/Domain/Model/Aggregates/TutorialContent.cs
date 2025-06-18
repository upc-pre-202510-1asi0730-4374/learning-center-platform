using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Entities;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.ValueObjects;

namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Aggregates;

public partial class Tutorial : IPublishable
{
    public Tutorial()
    {
        Title = string.Empty;
        Summary = string.Empty;
        Assets = new List<Asset>();
        Status = EPublishingStatus.Draft;
    }
    
    public ICollection<Asset> Assets { get; }
    public EPublishingStatus Status { get; protected set; }
    
    public void SendToEdit()
    {
        throw new NotImplementedException();
    }

    public void SendToApproval()
    {
        throw new NotImplementedException();
    }

    public void ApproveAndLock()
    {
        throw new NotImplementedException();
    }

    public void Reject()
    {
        throw new NotImplementedException();
    }

    public void ReturnToEdit()
    {
        throw new NotImplementedException();
    }
}