namespace EMS.DAL.ADO.NET.Entities;

public class Venue
{
    public int id { get; set; }
    public string name { get; set; }
    public string address { get; set; }
    public string city { get; set; }
    public string country { get; set; }
    public decimal latitude { get; set; }
    public decimal longitude { get; set; }
   public int? capacity { get; set; }
}