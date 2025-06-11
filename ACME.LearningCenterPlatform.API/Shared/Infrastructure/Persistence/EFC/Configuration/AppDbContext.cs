using ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Aggregates;
using ACME.LearningCenterPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ACME.LearningCenterPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Application database context for the Learning Center Platform
/// </summary>
/// <param name="options">
///     The options for the database context
/// </param>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
   /// <summary>
   ///     On configuring the database context
   /// </summary>
   /// <remarks>
   ///     This method is used to configure the database context.
   ///     It also adds the created and updated date interceptor to the database context.
   /// </remarks>
   /// <param name="builder">
   ///     The option builder for the database context
   /// </param>
   protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

   /// <summary>
   ///     On creating the database model
   /// </summary>
   /// <remarks>
   ///     This method is used to create the database model for the application.
   /// </remarks>
   /// <param name="builder">
   ///     The model builder for the database context
   /// </param>
   protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); 
        
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
        
        // Relationship Profile has many Orders
        builder.Entity<Profile>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Profile)
            .HasForeignKey(o => o.ProfileId)
            .OnDelete(DeleteBehavior.Restrict);
            
        
        
        builder.UseSnakeCaseNamingConvention();
    }
}