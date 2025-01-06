using System;
using System.Collections.Generic;

namespace MyNamespace
{
    public enum TypeOfFuel
    {
        Petrol,
        Electric,
        Hybrid,
        Diesel
    }

    public enum TypeOfCar
    {
        Sedan,
        Bus,
        Tank,
        Jeep
    }

    public class Transport
    {
        public string Model { get; set; }
        public string Name { get; set; }
        public TypeOfFuel Fuel { get; set; }
        public TypeOfCar CarType { get; set; }

        public Transport(string model, string name, TypeOfFuel fuel, TypeOfCar carType)
        {
            Model = model;
            Name = name;
            Fuel = fuel;
            CarType = carType;
        }

        public virtual void Print()
        {
            Console.WriteLine($"Model: {Model}, Name: {Name}, Fuel Type: {Fuel}, Car Type: {CarType}");
        }
    }

    public class Car : Transport
    {
        public int NumberOfPeople { get; set; }

        public Car(int numberOfPeople, string model, string name, TypeOfFuel fuel, TypeOfCar carType)
            : base(model, name, fuel, carType)
        {
            NumberOfPeople = numberOfPeople;
        }

        public override void Print()
        {
            base.Print();
            Console.WriteLine($"Number of People: {NumberOfPeople}");
        }
    }

    public class Bus : Transport
    {
        public int PublishedYear { get; set; }

        public Bus(int publishedYear, string model, string name, TypeOfFuel fuel, TypeOfCar carType)
            : base(model, name, fuel, carType)
        {
            PublishedYear = publishedYear;
        }

        public override void Print()
        {
            base.Print();
            Console.WriteLine($"Published Year: {PublishedYear}");
        }
    }

    public class ParkingLot
    {
        private List<Transport> transports = new List<Transport>();

        public void AddTransport(Transport transport)
        {
            transports.Add(transport);
            Console.WriteLine("Transport added successfully.");
        }

        public void RemoveTransport(string model)
        {
            var transport = transports.Find(t => t.Model == model);
            if (transport != null)
            {
                transports.Remove(transport);
                Console.WriteLine("Transport removed successfully.");
            }
            else
            {
                Console.WriteLine("Transport not found.");
            }
        }

        public void EditTransport(string model, string newName, TypeOfFuel newFuel, TypeOfCar newCarType)
        {
            var transport = transports.Find(t => t.Model == model);
            if (transport != null)
            {
                transport.Name = newName;
                transport.Fuel = newFuel;
                transport.CarType = newCarType;
                Console.WriteLine("Transport edited successfully.");
            }
            else
            {
                Console.WriteLine("Transport not found.");
            }
        }

        public void ListTransports()
        {
            if (transports.Count == 0)
            {
                Console.WriteLine("No transports in the parking lot.");
                return;
            }

            foreach (var transport in transports)
            {
                transport.Print();
                Console.WriteLine("------------------");
            }
        }
    }

    public class Program
    {
        public static void Main()
        {
            ParkingLot parkingLot = new ParkingLot();

            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Add Transport");
                Console.WriteLine("2. Remove Transport");
                Console.WriteLine("3. Edit Transport");
                Console.WriteLine("4. List Transports");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter model: ");
                        string model = Console.ReadLine();
                        Console.Write("Enter name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter fuel type (Petrol, Electric, Hybrid, Diesel): ");
                        TypeOfFuel fuel = (TypeOfFuel)Enum.Parse(typeof(TypeOfFuel), Console.ReadLine(), true);
                        Console.Write("Enter car type (Sedan, Bus, Tank, Jeep): ");
                        TypeOfCar carType = (TypeOfCar)Enum.Parse(typeof(TypeOfCar), Console.ReadLine(), true);

                        if (carType == TypeOfCar.Bus)
                        {
                            Console.Write("Enter published year: ");
                            int year = int.Parse(Console.ReadLine());
                            parkingLot.AddTransport(new Bus(year, model, name, fuel, carType));
                        }
                        else
                        {
                            Console.Write("Enter number of people: ");
                            int people = int.Parse(Console.ReadLine());
                            parkingLot.AddTransport(new Car(people, model, name, fuel, carType));
                        }

                        break;
                    case "2":
                        Console.Write("Enter model to remove: ");
                        string modelToRemove = Console.ReadLine();
                        parkingLot.RemoveTransport(modelToRemove);
                        break;
                    case "3":
                        Console.Write("Enter model to edit: ");
                        string modelToEdit = Console.ReadLine();
                        Console.Write("Enter new name: ");
                        string newName = Console.ReadLine();
                        Console.Write("Enter new fuel type (Petrol, Electric, Hybrid, Diesel): ");
                        TypeOfFuel newFuel = (TypeOfFuel)Enum.Parse(typeof(TypeOfFuel), Console.ReadLine(), true);
                        Console.Write("Enter new car type (Sedan, Bus, Tank, Jeep): ");
                        TypeOfCar newCarType = (TypeOfCar)Enum.Parse(typeof(TypeOfCar), Console.ReadLine(), true);
                        parkingLot.EditTransport(modelToEdit, newName, newFuel, newCarType);
                        break;
                    case "4":
                        parkingLot.ListTransports();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}
