namespace CarRentalApp.DTOs;

public class CreateRentalDTO
{
    public int CarId { get; set; }
    public int UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
}