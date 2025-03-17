namespace CarRentalApp.DTOs;

public class CreateCarDTO
{
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal PricePerDay { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public bool IsAvailable { get; set; }
}