namespace Domain;

public class Car
{
    public int CarId {get; set;}
    public string Model {get; set;} = "";
    public string Manufacturer {get; set;} = "";
    public int Year { get; set; }
    public decimal PricePerDay {get; set;}
}