namespace Application.Validations.Helpers;
using MongoDB.Bson;
public class ValidationIdHelper
{
    public static bool BeAValidObjectId(string id)
    {
        return ObjectId.TryParse(id, out _);
    }
}