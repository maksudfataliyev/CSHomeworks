namespace FinalProject;
public class Showroom
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public List<Car> Cars { get; set; } = new List<Car>();
    
    public List<Sale> Sales { get; set; } =  new List<Sale>();
    public int CarCapacity { get; set; } 
    public int CarCount => Cars.Count; 
    public int SalesCount => Sales.Count;
    
    public void CreateCar()
    {   
        if (CarCount == CarCapacity)
        {
            Console.WriteLine("У вас уже максимум машин в автосалоне добавлять нельзя");
            return;
        }
        Console.WriteLine("Марка:");
        string make = Console.ReadLine();
        Console.WriteLine("Модель:");
        string model = Console.ReadLine();
        Console.WriteLine("Дата создания:");
        DateTime date;
        while (!DateTime.TryParse(Console.ReadLine(), out date))
        {
            Console.WriteLine("Некорректный ввод");
        }
        Car car = new Car(make,model,date);
        Cars.Add(car);
        Console.WriteLine("Машина создана");

    }

    public Showroom(Guid id, string name, int carCapacity)
    {
        Id = id;
        Name = name;
        CarCapacity = carCapacity;
    }
    public Showroom(string name,
        int carCapacity)
    {
        Id = Guid.NewGuid();
        Name = name;
        CarCapacity = carCapacity;
    }
    
    
    public Car? buycar()
    {
        if (Cars.Count == CarCapacity)
        {
            Console.WriteLine("У вас уже максимум машин в автосалоне добавлять нельзя");
            return null;
        }
        
        Console.WriteLine("Марка:");
        string make = Console.ReadLine();
        Console.WriteLine("Модель:");
        string model = Console.ReadLine();
        Console.WriteLine("Дата создания:");
        DateTime date;
        while (!DateTime.TryParse(Console.ReadLine(), out date))
        {
            Console.WriteLine("Некорректный ввод даты");
        }
        Car car = new Car(make,model,date);
        Cars.Add(car);
        
        return car;

    }

    public void print()
    {
        Console.WriteLine($"ID:{Id}");
        Console.WriteLine($"Имя: {Name}");
        Console.WriteLine("Все машины:");
        for (int i = 0; i < Cars.Count; i++)
        {
            Cars[i].print();
        }

        Console.WriteLine("Все продажи:");
        for (int i = 0; i < Sales.Count; i++)
        {
            Sales[i].print();
        }

        Console.WriteLine();

        Console.WriteLine($"Максимум машин: {CarCapacity}");
        Console.WriteLine($"Количество машин: {CarCount}");
        Console.WriteLine($"Количество продаж: {SalesCount}");
        
    }
    
    public Showroom(){}
    
}