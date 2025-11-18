using Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Infrastructure.Serialization;

public class LocationSerializer : SerializerBase<Location>
{
    public override Location Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var bsonReader = context.Reader;
        bsonReader.ReadStartDocument();

        string address = string.Empty;
        string city = string.Empty;
        string country = string.Empty;

        while (bsonReader.ReadBsonType() != BsonType.EndOfDocument)
        {
            var fieldName = bsonReader.ReadName();

            switch (fieldName)
            {
                case "address":
                    address = bsonReader.ReadString();
                    break;
                case "city":
                    city = bsonReader.ReadString();
                    break;
                case "country":
                    country = bsonReader.ReadString();
                    break;
                default:
                    bsonReader.SkipValue();
                    break;
            }
        }
        
        bsonReader.ReadEndDocument();
        return new Location(address, city, country);
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Location value)
    {
        var bsonWriter = context.Writer;
        bsonWriter.WriteStartDocument();
        bsonWriter.WriteName("address");
        BsonSerializer.Serialize(bsonWriter, value.Address);
        bsonWriter.WriteName("city");
        BsonSerializer.Serialize(bsonWriter, value.City);
        bsonWriter.WriteName("country");
        BsonSerializer.Serialize(bsonWriter, value.Country);
        bsonWriter.WriteEndDocument();
    }
}