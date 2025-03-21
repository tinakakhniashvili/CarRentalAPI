namespace CarRentalApp.DTOs;

public class CarDTO
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public string Description { get; set; }
    public decimal PricePerDay { get; set; }
    public bool IsAvailable { get; set; }
    public string ImageUrl { get; set; }
}