using CarRentalApp.Entities;

namespace CarRentalApp.Models;

public class Role : BaseClass
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<User> Users { get; set; }
}