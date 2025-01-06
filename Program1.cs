using System;

namespace MyNamespace
{
    public class Book
    {
        private int year;
        private string title;
        private string author;
        public Book(int year, string title, string author)
        {
            this.year = year;
            this.title = title;
            this.author = author;
        }
        public void print(Book book)
        {
            Console.WriteLine($"Books author: {book.author}");
            Console.WriteLine($"Books title: {book.title}");
            Console.WriteLine($"Year when the book was published: {book.year}");
        }

        public int getYear()
        {
            return year;
        }
        public string getTitle()
        {
            return title;
        }
        public string getAuthor()
        {
            return author;
        }

    }

    public class Program1
    {
        public static void Main(string[] args)
        {
            List<Book> booklist = new List<Book>();
            while (true)
            {
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("Add book(1)");
                Console.WriteLine("Delete book(2)");
                Console.WriteLine("Print every single book(3)");
                Console.WriteLine("Exit from the programm(4)");
                int choice;
                choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    Console.WriteLine("Author:");
                    string author;
                    author = Console.ReadLine();
                    Console.WriteLine("Title");
                    string title;
                    title = Console.ReadLine();
                    Console.WriteLine("Year when the book was published:");
                    int year;
                    year = int.Parse(Console.ReadLine());
                    booklist.Add(new Book(year, title, author));
                }
                else if (choice == 2)
                {
                    Console.WriteLine("Title:");
                    string title;
                    title = Console.ReadLine();
                    for (int i = 0; i < booklist.Count; i++)
                    {
                        if (booklist[i].getTitle() == title)
                        {
                            Console.WriteLine("Deleted");
                            booklist.RemoveAt(i);
                            break;
                        }
                        if (i == booklist.Count - 1)
                        {
                            Console.WriteLine("No matching titles are found");
                            break;
                        }
                    }
                }
                else if (choice == 3)
                {
                    foreach (Book book in booklist)
                    {
                        book.print(book);
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Closing");
                    break;
                }
            }
        }
    }
}