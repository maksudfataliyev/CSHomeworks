using System;

public class Program
{
    public static void Main(string[] args)
    {
        // Task 1: Fuzz Bruzz
        short number = short.Parse(Console.ReadLine());
        if (number >= 1 && number <= 100)
        {
            if (number % 3 == 0 && number % 5 == 0)
            {
                Console.WriteLine("Fuzz bruzz");
            }
            else if (number % 3 == 0)
            {
                Console.WriteLine("Fuzz");
            }
            else if (number % 5 == 0)
            {
                Console.WriteLine("Bruzz");
            }
            else
            {
                Console.WriteLine(number);
            }
        }
        else
        {
            Console.WriteLine("Invalid input");
            return;
        }

        // Task 2: Percentage Calculation
        double n = double.Parse(Console.ReadLine());
        double p = double.Parse(Console.ReadLine());
        double result = n * (p / 100.0);
        Console.WriteLine(result);

        // Task 3: Digit Conversion
        int a = int.Parse(Console.ReadLine());
        int b = int.Parse(Console.ReadLine());
        int c = int.Parse(Console.ReadLine());
        int d = int.Parse(Console.ReadLine());
        a = a * 1000;
        b = b * 100;
        c = c * 10;
        int e = a + b + c + d;
        Console.WriteLine(e);

        // Task 4: Swapping Digits
        int inputNumber = int.Parse(Console.ReadLine());
        if (inputNumber < 100000 || inputNumber > 999999)
        {
            Console.WriteLine("Wrong input");
        }
        else
        {
            int pos1 = int.Parse(Console.ReadLine());
            if (pos1 > 6 || pos1 < 1)
            {
                Console.WriteLine("Wrong input");
                return;
            }
            int pos2 = int.Parse(Console.ReadLine());
            if (pos2 > 6 || pos2 < 1)
            {
                Console.WriteLine("Wrong input");
                return;
            }
            if (pos1 == pos2)
            {
                Console.WriteLine(inputNumber);
            }
            else
            {
                string str = inputNumber.ToString();
                char[] arr = str.ToCharArray();
                char temp = arr[pos1 - 1];
                arr[pos1 - 1] = arr[pos2 - 1];
                arr[pos2 - 1] = temp;
                Console.WriteLine(new string(arr));
            }
        }

        // Task 5: Date and Season
        if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
        {
            string dayOfWeek = date.DayOfWeek.ToString();

            string season;
            int month = date.Month;
            if (month == 12 || month == 1 || month == 2)
            {
                season = "Winter";
            }
            else if (month >= 3 && month <= 5)
            {
                season = "Spring";
            }
            else if (month >= 6 && month <= 8)
            {
                season = "Summer";
            }
            else
            {
                season = "Autumn";
            }

            Console.WriteLine($"{season} {dayOfWeek}");
        }
        else
        {
            Console.WriteLine("Invalid date format");
            return;
        }

        // Task 6: Temperature Conversion
        double temperature = double.Parse(Console.ReadLine());
        Console.WriteLine("Convert to Celsius or Fahrenheit? (0,1)");
        int choice = int.Parse(Console.ReadLine());
        if (choice == 0)
        {
            double celsius = 5.0 / 9 * (temperature - 32);
            Console.WriteLine(celsius);
        }
        else if (choice == 1)
        {
            double fahrenheit = 9.0 / 5 * temperature + 32;
            Console.WriteLine(fahrenheit);
        }
        else
        {
            Console.WriteLine("Wrong input");
        }

        // Task 7: Print Even Numbers
        int start = int.Parse(Console.ReadLine());
        int end = int.Parse(Console.ReadLine());
        if (end < start)
        {
            for (int i = end; i < start; i = i + 1)
            {
                if (i % 2 == 0 && i != 0)
                {
                    Console.WriteLine(i);
                }
            }
        }
        else if (start == end)
        {
            return;
        }
        else
        {
            for (int i = start; i < end; i = i + 1)
            {
                if (i % 2 == 0 && i != 0)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}
