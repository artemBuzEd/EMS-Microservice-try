using DAL.BogusSeed.Fakers;
using DAL.Entities;
using DAL.EntityConfig;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.BogusSeed;

public class DataSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<UserProfileDbContext>();
        
        await context.Database.EnsureCreatedAsync();
        
        if(context.UserProfiles.Any())
            return;

        var userProfileFaker = new UserProfileFaker();
        var userProfiles = userProfileFaker.Generate(50);
        
        await context.UserProfiles.AddRangeAsync(userProfiles);
        await context.SaveChangesAsync();

        var userComments = new UserCommentFaker()
            .RuleFor(f => f.user_id, f => f.PickRandom(userProfiles).user_id);
        
        await context.UserComments.AddRangeAsync(userComments.Generate(20));
        await context.SaveChangesAsync();
        
        var userEventCalendarFaker = new UserEventCalendarFaker()
            .RuleFor(f => f.user_id, f => f.PickRandom(userProfiles).user_id);
        
        await context.UserEventCalendars.AddRangeAsync(userEventCalendarFaker.Generate(50));
        await context.SaveChangesAsync();
    }
}