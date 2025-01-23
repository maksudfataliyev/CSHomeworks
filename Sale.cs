namespace FinalProject;

public class Sale
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ShowroomId { get; set; } 
    public Guid CarId { get; set; } 
    public Guid UserId { get; set; } 
    public DateTime SaleDate { get; set; }
    public int Price { get; set; }

    public Sale(Guid showroomId, Guid carId, Guid userId, DateTime saleDate, int price)
    {
        Id = Guid.NewGuid();
        ShowroomId = showroomId;
        CarId = carId;
        UserId = userId;
        SaleDate = saleDate;
        Price = price;
        
    }

    public void print()
    {
        Console.WriteLine($"SaleId: {Id}");
        Console.WriteLine($"SaleDate: {SaleDate}");
        Console.WriteLine($"Price: {Price}");
    }
}