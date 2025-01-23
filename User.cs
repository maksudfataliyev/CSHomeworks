namespace FinalProject;

public class User
{
    public Guid Id { get; }
    public Guid ShowroomId { get; set; } 
    public string Username { get; set; }
    public string Password { get; set; }

    public User(Guid id, Guid showroomId, string username, string password)
    {
        Id = id;
        ShowroomId = showroomId;
        Username = username;
        Password = password;
    }
    
    public void SellCar(List<Showroom> showrooms, Car car, int price, DateTime SaleDate)
    {
        for (int i = 0; i < showrooms.Count; i++)
        {
            if (showrooms[i].Id == ShowroomId)
            {
                showrooms[i].Cars.Remove(car);
                Sale sale = new Sale(ShowroomId, car.Id, Id, SaleDate, price);
                showrooms[i].Sales.Add(sale);
            }
        }
    }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }
    public User(){}
    
}