using System.Threading.Channels;

namespace ConsoleApp2;


interface ICalculatorOperation
{
    double Execute(double a, double b);
    string Name { get; }
}

class Addition : ICalculatorOperation
{
    public double Execute (double a, double b)
    {
        return a + b;    
    }
    public string Name { get; } = "Addition";
}

class Subtraction : ICalculatorOperation
{
    public double Execute(double a, double b)
    {
        return a - b;
    }
    public string Name { get; } = "Subtraction";
}

class Multiplication : ICalculatorOperation
{
    public double Execute(double a, double b)
    {
        return a * b;
    }
    public string Name { get; } = "Multiplication";
}

class Division : ICalculatorOperation
{
    public double Execute  (double a, double b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException();
        }
        return a / b;
    }

    public string Name { get; } = "Division";
}


class InterfaceCalculator
{
    static void Main(string[] args)
    {
        List <double> numbers = new List<double>();
        ICalculatorOperation addition = new Addition();
        ICalculatorOperation subtraction = new Subtraction();
        ICalculatorOperation multiplication = new Multiplication();
        ICalculatorOperation division = new Division();
        bool a = true;
        while (a)
        {
            Console.WriteLine("Addition(0)");
            Console.WriteLine("Subtraction(1)");
            Console.WriteLine("Multiplication(2)");
            Console.WriteLine("Division(3)");
            Console.WriteLine("Exit(anything else)");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 0:
                    Console.WriteLine("Enter first number");
                    int num1 = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter second number");
                    int num2 = int.Parse(Console.ReadLine());
                    var res1 = addition.Execute(num1, num2);
                    Console.WriteLine($"{num1} + {num2} = {addition.Execute(num1, num2)}");
                    Console.WriteLine("Would you like to save your result?(Yes/No)");
                    string answer = Console.ReadLine().ToLower();
                    if (answer == "yes")
                    {
                        Console.WriteLine("Added the result");
                        numbers.Add(res1);
                        Console.WriteLine();
                        foreach (var num in numbers)
                        {
                            Console.Write(num);
                            Console.Write(",");
                        }
                        Console.WriteLine();

                        
                    }
                    else if (answer == "no")
                    {
                        Console.WriteLine("Removed the result");
                        numbers.Remove(res1);
                        Console.WriteLine();
                        foreach (var num in numbers)
                        {
                            Console.Write(num);
                            Console.Write(",");

                        }
                        Console.WriteLine();

                    }
                    else
                    {
                        Console.WriteLine("Wrong input, no changes have been made");
                    }
                    break;
                case 1:
                    Console.WriteLine("Enter first number");
                    int num3 = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter second number");
                    int num4 = int.Parse(Console.ReadLine());
                    var res2 = subtraction.Execute(num3, num4);
                    Console.WriteLine($"{num3} - {num4} = {res2}");
                    Console.WriteLine("Would you like to save your result?(Yes/No)");
                    string answer2 = Console.ReadLine().ToLower();
                    if (answer2 == "yes")
                    {
                        Console.WriteLine("Added the result");
                        numbers.Add(res2);
                        Console.WriteLine();
                        foreach (var num in numbers)
                        {
                            Console.Write(num);
                            Console.Write(",");

                        }
                        Console.WriteLine();

                        
                    }
                    else if (answer2 == "no")
                    {
                        Console.WriteLine("Removed the result");
                        numbers.Remove(res2);
                        Console.WriteLine();
                        foreach (var num in numbers)
                        {
                            Console.Write(num);
                            Console.Write(",");

                        }
                        Console.WriteLine();

                    }
                    else
                    {
                        Console.WriteLine("Wrong input, no changes have been made");
                    }
                    break;
                case 2:
                    Console.WriteLine("Enter first number");
                    int num5 = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter second number");
                    int num6 = int.Parse(Console.ReadLine());
                    var res3 = multiplication.Execute(num5, num6);
                    Console.WriteLine($"{num5} * {num6} = {res3}");
                    Console.WriteLine("Would you like to save your result?(Yes/No)");
                    string answer3 = Console.ReadLine().ToLower();
                    if (answer3 == "yes")
                    {
                        Console.WriteLine("Added the result");
                        numbers.Add(res3);
                        Console.WriteLine();
                        foreach (var num in numbers)
                        {
                            Console.Write(num);
                            Console.Write(",");

                        }
                        Console.WriteLine();

                        
                    }
                    else if (answer3 == "no")
                    {
                        Console.WriteLine("Removed the result");
                        numbers.Remove(res3);
                        Console.WriteLine();
                        foreach (var num in numbers)
                        {
                            
                            Console.Write(num);
                            Console.Write(",");

                        }
                        Console.WriteLine();

                    }
                    else
                    {
                        Console.WriteLine("Wrong input, no changes have been made");
                    }
                    break;
                case 3:
                    Console.WriteLine("Enter first number");
                    int num7 = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter second number");
                    int num8 = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine($"{num7} * {num8} = {division.Execute(num7, num8)}");
                    }
                    catch (DivideByZeroException)
                    {
                        Console.WriteLine("Error, you cant divide by zero");
                        break;
                    }
                    var res4 = division.Execute(num7, num8);

                    Console.WriteLine("Would you like to save your result?(Yes/No)");
                    string answer4 = Console.ReadLine().ToLower();
                    if (answer4 == "yes")
                    {
                        Console.WriteLine("Added the result");
                        numbers.Add(res4);
                        Console.WriteLine();
                        foreach (var num in numbers)
                        {
                            Console.Write(num);
                            Console.Write(",");

                        }

                        Console.WriteLine();
                    }
                    else if (answer4 == "no")
                    {
                        Console.WriteLine("Removed the result");
                        numbers.Remove(res4);
                        Console.WriteLine();
                        foreach (var num in numbers)
                        {
                            Console.Write(num);
                            Console.Write(",");

                        }
                        Console.WriteLine();

                    }
                    else
                    {
                        Console.WriteLine("Wrong input, no changes have been made");
                    }
                    break;
                default:
                    Console.WriteLine("Closing the program");
                    a = false;
                    break;
            }
        }
    }
}