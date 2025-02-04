﻿namespace FinalProject;

public class Car
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Make { get; set; }
    public string Model { get; set; }
    public DateTime Year { get; set; }

    public Car(string make, string model, DateTime year)
    {
        Make = make;
        Model = model;
        Year = year;
    }

    public void print()
    {
        Console.WriteLine($"Make: {Make}");
        Console.WriteLine($"Model: {Model}");
        Console.WriteLine($"Year: {Year}");
        Console.WriteLine($"Id: {Id}");

        Console.WriteLine();
        Console.WriteLine();
    }
}