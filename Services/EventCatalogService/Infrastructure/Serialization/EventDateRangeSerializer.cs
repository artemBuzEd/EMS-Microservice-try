using System.Globalization;
using Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Infrastructure.Serialization;

public class EventDateRangeSerializer : SerializerBase<EventDateRange>
{
    public override EventDateRange Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var bsonReader = context.Reader;
        bsonReader.ReadStartDocument();
        
        DateTime start = DateTime.MinValue;
        DateTime end = DateTime.MinValue;

        while (bsonReader.ReadBsonType() != BsonType.EndOfDocument)
        {
            var name = bsonReader.ReadName();

            switch (name)
            {
                case "start":
                    start = ReadDateTimeFlexible(bsonReader);
                    break;
                case "end":
                    end = ReadDateTimeFlexible(bsonReader);
                    break;
                default:
                    bsonReader.SkipValue();
                    break;
            }
        }

        bsonReader.ReadEndDocument();
        return new EventDateRange(start, end);
            
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, EventDateRange value)
    {
        var bsonWriter = context.Writer;
        bsonWriter.WriteStartDocument();
        bsonWriter.WriteName("start");
        BsonSerializer.Serialize(bsonWriter, value.Start);
        bsonWriter.WriteName("end");
        BsonSerializer.Serialize(bsonWriter, value.End);
        bsonWriter.WriteEndDocument();
    }

    private DateTime ReadDateTimeFlexible(IBsonReader reader)
    {
        // Support DateTime and ISO string values
        return reader.CurrentBsonType switch
        {
            BsonType.DateTime => BsonSerializer.Deserialize<DateTime>(reader),
            BsonType.String => DateTime.Parse(reader.ReadString(), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
            _ => BsonSerializer.Deserialize<DateTime>(reader)
        };
    }
}