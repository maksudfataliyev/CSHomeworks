using System;

namespace MyNamespace
{
    public abstract class Transport
    {
        protected string type;
        protected string brand;
        protected string model;
        protected int year;
        protected int max_speed;

        protected Transport(string type, string brand, string model, int year, int max_speed)
        {
            this.type = type;
            this.brand = brand;
            this.model = model;
            this.year = year;
            this.max_speed = max_speed;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Type: {type}");
            Console.WriteLine($"Brand: {brand}");
            Console.WriteLine($"Model: {model}");
            Console.WriteLine($"Year: {year}");
            Console.WriteLine($"Max speed: {max_speed}");
        }

        public abstract void Move();
    }

    public class Car : Transport
    {
        private string FuelType;
        public Car(string brand, string model, int year, int max_speed, string fuelType) : base("Car", brand, model, year, max_speed)
        {
            this.FuelType = fuelType;
        }
        public override void Move()
        {
            Console.WriteLine($"Car {brand}, {model} is moving at speed up to {max_speed} km/h");
        }


    }
    public class Truck : Transport
    {
        private int loadcapacity;
        public Truck(string brand, string model, int year, int max_speed, int loadcapacity) : base("Truck", brand, model, year, max_speed)
        {
            this.loadcapacity = loadcapacity;
        }

        public override void Move()
        {
            Console.WriteLine($"Truck {brand}, {model} is carrying goods");
        }


    }
    public class Bike : Transport
    {
        bool hassidechair;
        public Bike(string brand, string model, int year, int max_speed, bool hassidechair) : base("Bike", brand, model, year, max_speed)
        {
            this.hassidechair = hassidechair;
        }

        public override void Move()
        {
            Console.WriteLine($"Bike {brand}, {model} is flying on the roads");
        }

    }

    public class Bus : Transport
    {
        int passangercapacity;
        public Bus(string brand, string model, int year, int max_speed, int passangercapacity) : base("Bus", brand, model, year, max_speed)
        {
            this.passangercapacity = passangercapacity;
        }

        public override void Move()
        {
            Console.WriteLine($"Bus {brand}, {model} is carrying passangers");
        }

    }

    public class Program
    {
        static List<Transport> transports = new List<Transport>();

        public static void Main()
        {
            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Add Transport");
                Console.WriteLine("2. Show All Transports");
                Console.WriteLine("3. Start Transport");
                Console.WriteLine("4. Delete Transport");
                Console.WriteLine("5. Filter by Transport Type");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTransport();
                        break;
                    case "2":
                        ShowAllTransports();
                        break;
                    case "3":
                        StartTransport();
                        break;
                    case "4":
                        DeleteTransport();
                        break;
                    case "5":
                        FilterByType();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        private static void AddTransport()
        {
            Console.WriteLine("Enter transport type (Car, Truck, Bike, Bus):");
            string type = Console.ReadLine();

            Console.WriteLine("Enter brand:");
            string brand = Console.ReadLine();

            Console.WriteLine("Enter model:");
            string model = Console.ReadLine();

            Console.WriteLine("Enter year:");
            int year = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter max speed:");
            int maxSpeed = int.Parse(Console.ReadLine());

            if (type.ToLower() == "car")
            {
                Console.WriteLine("Enter fuel type:");
                string fuelType = Console.ReadLine();
                transports.Add(new Car(brand, model, year, maxSpeed, fuelType));
            }
            else if (type.ToLower() == "truck")
            {
                Console.WriteLine("Enter load capacity:");
                int loadCapacity = int.Parse(Console.ReadLine());
                transports.Add(new Truck(brand, model, year, maxSpeed, loadCapacity));
            }
            else if (type.ToLower() == "bike")
            {
                Console.WriteLine("Does it have a side chair? (true/false):");
                bool hasSideChair = bool.Parse(Console.ReadLine());
                transports.Add(new Bike(brand, model, year, maxSpeed, hasSideChair));
            }
            else if (type.ToLower() == "bus")
            {
                Console.WriteLine("Enter passenger capacity:");
                int passengerCapacity = int.Parse(Console.ReadLine());
                transports.Add(new Bus(brand, model, year, maxSpeed, passengerCapacity));
            }
            else
            {
                Console.WriteLine("Invalid transport type.");
            }
        }

        private static void ShowAllTransports()
        {
            foreach (var transport in transports)
            {
                transport.ShowInfo();
            }
        }

        private static void StartTransport()
        {
            Console.WriteLine("Enter transport index to start:");
            int index = int.Parse(Console.ReadLine());

            if (index >= 0 && index < transports.Count)
            {
                transports[index].Move();
            }
            else
            {
                Console.WriteLine("Index out of range.");
            }
        }

        private static void DeleteTransport()
        {
            Console.WriteLine("Enter transport index to delete:");
            int index = int.Parse(Console.ReadLine());

            if (index >= 0 && index < transports.Count)
            {
                transports.RemoveAt(index);
                Console.WriteLine("Transport deleted.");
            }
            else
            {
                Console.WriteLine("Invalid index.");
            }
        }

        private static void FilterByType()
        {
            Console.WriteLine("Enter transport type to filter by (Car, Truck, Bike, Bus):");
            string type = Console.ReadLine().ToLower();

            foreach (var transport in transports)
            {
                if (transport.GetType().Name.ToLower() == type)
                {
                    transport.ShowInfo();
                }
            }
        }
    }
    }

