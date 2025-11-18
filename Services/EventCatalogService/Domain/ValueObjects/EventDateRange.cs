using Domain.Exceptions;

namespace Domain.ValueObjects;

public class EventDateRange : ValueObject
{
    public DateTime Start { get; }
    public DateTime End { get; }

    // Private constructor for BSON deserialization
    private EventDateRange() {}

    public EventDateRange(DateTime start, DateTime end)
    {
        if (start >= end)
        {
            throw new DomainException("Event start date must be before the end date.");
        }
        Start = start;
        End = end;
    }
    
    // The components that define this value object's identity.
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
    }
}