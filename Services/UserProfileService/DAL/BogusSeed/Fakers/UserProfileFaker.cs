using Bogus;
using DAL.Entities;

namespace DAL.BogusSeed.Fakers;

public class UserProfileFaker : Faker<UserProfile>
{
    public UserProfileFaker()
    {
        RuleFor(f => f.user_id, f => f.Random.AlphaNumeric(10));
        RuleFor(f => f.first_name, f => f.Person.FirstName);
        RuleFor(f => f.last_name, f => f.Person.LastName);
        RuleFor(f => f.bio, f => f.Lorem.Sentence());
        RuleFor(f => f.birth_date, f => f.Date.Past(18));
        RuleFor(f => f.created_at, f => f.Date.Past());
    }
}