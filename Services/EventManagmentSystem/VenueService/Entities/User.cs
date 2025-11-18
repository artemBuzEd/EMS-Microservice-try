namespace EMS.DAL.ADO.NET.Entities;

public class User
{
    public int id { get; set; }
    public string name { get; set; } = null!;
    public string email { get; set; } = null!;
}