using ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace ACME.LearningCenterPlatform.API.Profiles.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyProfilesConfiguration(this ModelBuilder builder)
    {
        
        // Profiles Context
        builder.Entity<Profile>().HasKey(x => x.Id);
        builder.Entity<Profile>().Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Profile>().OwnsOne(x => x.Name,
            n =>
            {
                n.WithOwner().HasForeignKey("Id");
                n.Property(p=> p.FirstName).HasColumnName("FirstName");
                n.Property(p=> p.LastName).HasColumnName("LastName");
            });

    }
}