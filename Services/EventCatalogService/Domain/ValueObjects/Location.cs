using Domain.Exceptions;

namespace Domain.ValueObjects;

public class Location : ValueObject
{
    public string Address { get; }
    public string City { get; }
    public string Country { get; }

    // Private constructor for BSON deserialization
    private Location() {}

    public Location(string address, string city, string country)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            throw new DomainException("City is required for a location.");
        }
        Address = address;
        City = city;
        Country = country;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Address;
        yield return City;
        yield return Country;
    }
}