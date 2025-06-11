namespace ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Aggregates;

public class Order
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int ProfileId { get; set; }
    public Profile Profile { get; set; }
}