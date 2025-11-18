using Bogus;
using DAL.Entities;

namespace DAL.BogusSeed.Fakers;

public sealed class UserCommentFaker : Faker<UserComment>
{
    public UserCommentFaker()
    {
        RuleFor(f => f.comment, f => f.Lorem.Sentence());
        RuleFor(f => f.rating, f => f.Random.Number(0,5));
        RuleFor(f => f.event_id, f => f.Random.AlphaNumeric(24));
    }
}