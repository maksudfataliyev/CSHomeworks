using System.Numerics;
//Задание 1
int[] a = { 1, 1, 2, 3, 5, 6, 7, 8 };
int unique_count = 0;
int odd_count = 0;
int even_count = 0;
int count = 0;
Console.WriteLine("Array:");
foreach (int i in a)
{
    Console.Write(i);
    if (i % 2 == 0)
    {
        even_count++;
    }
    if (i % 2 == 1)
    {
        odd_count++;
    }
    for (int j = 0; j < a.Length; j++)
    {
        if (j == i)
        {
            count++;
        }
    }
    if (count == 1)
    {
        unique_count++;
    }
    count = 0;
}
Console.WriteLine();
Console.WriteLine($"Unique count:{unique_count}");
Console.WriteLine($"Odd count:{odd_count}");
Console.WriteLine($"Even count:{even_count}");



//Задание 2

int[] b = { 5, 4, 7, 2, 6, 9, 1 };
int c = 0;
Console.WriteLine("Input your number");
int input = int.Parse(Console.ReadLine());
Console.WriteLine("Array:");
foreach (int i in b)
{
    Console.Write(i);

    if (i < input)
    {
        c++;
    }
}
Console.WriteLine();
Console.WriteLine($"Numbers less than your input : {c}");

//Задание 3
Console.WriteLine("Array:");
int[] arr = { 3, 5, 7, 2, 7, 9, 2, 6, 9, 2 };
int s = 0;
int ee = 0;
foreach (int i in arr)
{
    Console.Write(i);
}
Console.WriteLine();

Console.WriteLine("Input your number");
int input1 = int.Parse(Console.ReadLine());
Console.WriteLine("Input your number");
int input2 = int.Parse(Console.ReadLine());
Console.WriteLine("Input your number");
int input3 = int.Parse(Console.ReadLine());

for (int i = 0; i < arr.Length; i++)
{
    if (arr[i] == input1)
    {
        s = 1;
        continue;
    }
    if (s == 1)
    {
        if (arr[i] == input2)
        {
            s = 2;
            continue;
        }
        else
        {
            s = 0;
        }
    }
    if (s == 2)
    {
        if (arr[i] == input3)
        {
            ee = ee + 1;
            s = 0;
        }
        else
        {
            s = 0;
        }
    }
}

Console.WriteLine($"Count:{ee}");


//Задание 4

int[] arr1 = new int[4] { 1, 2, 4, 5 };
int[] arr2 = new int[4] { 1, 5, 2, 0 };
Console.WriteLine("Первый массив");
for (int i = 0; i < arr1.Length; i++)
{
    Console.Write(arr1[1]);
}
Console.WriteLine("Второй массив");
for (int i = 0; i < arr2.Length; i++)
{
    Console.Write(arr2[i]);
}

Console.WriteLine();

List<int> numbers = new List<int>();

for (int i = 0; i < arr1.Length; i++)
{
    for (int j = 0; j < arr2.Length; j++)
    {
        if (arr1[i] == arr2[j] && !numbers.Contains(arr1[i]))
        {
            numbers.Add(arr1[i]);
            break;
        }
    }
}

Console.WriteLine("Общие элементы без повторений:");
foreach (int num in numbers)
{
    Console.Write(num + " ");
}
//Задание 5

int[,] array = new int[3, 3]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };
for (int i = 0;i < array.GetLength(0); i++)
{
    for(int j = 0;j < array.GetLength(1); j++)
    {
        Console.Write(array[i,j] + " ");
    }
}
int max = array[0, 0];
int min = array[0, 0];

foreach (int num in array)
{
    if (num > max) max = num;
    if (num < min) min = num;
}

Console.WriteLine($"Min:{min}");
Console.WriteLine($"Max:{max}");

//Задание 6

Console.WriteLine("Введите предложение:");
string inpu = Console.ReadLine();

int wordCount = inpu.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
Console.WriteLine(wordCount);

//Задание 7

Console.WriteLine("Введите предложение:");
string inp = Console.ReadLine();

string[] words = inp.Split(' ', StringSplitOptions.RemoveEmptyEntries);

string result = "";
for (int i = 0; i < words.Length; i++)
{
    char[] wordArray = words[i].ToCharArray();  
    Array.Reverse(wordArray);                    
    result += new string(wordArray) + " ";       
}

Console.WriteLine(result.TrimEnd());

// Задание 8

Console.WriteLine("Введите предложение:");
string Z = Console.ReadLine();
string[] ww = Z.Split(' ', StringSplitOptions.RemoveEmptyEntries);

int ccount = 0;

string vowels = "aeiouAEIOU";

foreach (string word in ww)
{
    foreach (char ch in word)
    {
        for (int i = 0;i < vowels.Length; i++)
        {
            if (ch == vowels[i])
            {
                ccount++;
            }
        }
    }
}
Console.WriteLine($"Количество гласных букв: {ccount}");


//Задание 9
Console.WriteLine("Введите предложение:");
string z = Console.ReadLine();
string[] www = z.Split(' ', StringSplitOptions.RemoveEmptyEntries);
Console.WriteLine("Введите подстроку");
string sub = Console.ReadLine();
int cccount = 0;
foreach (string word in www)
{
    if (word == sub)
    {
        cccount++;
    }
}
Console.WriteLine($"Количество вхождений в строку:{cccount}");
