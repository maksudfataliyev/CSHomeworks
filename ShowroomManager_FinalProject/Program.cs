using System.Text.Json; 
namespace FinalProject;


class Program
{
    static void Main()
    {
        string filepathshowroom = "finalprojectshowrooms.json";
        string filepathuser = "finalprojectuser.json";

        
        List<Showroom> showrooms = new List<Showroom>();
        List<User> AllUsers = new List<User>();

        if (File.Exists(filepathuser))
        {
            AllUsers = JsonSerializer.Deserialize<List<User>>(File.ReadAllText(filepathuser));
        }

        if (File.Exists(filepathshowroom))
        {
            showrooms = JsonSerializer.Deserialize<List<Showroom>>(File.ReadAllText(filepathshowroom));
        }

        
        bool exitorcontinue = true;
        User currentUser = null;
        bool exitorcontinuesmall = true;
        
        while (exitorcontinue)
        {
            Console.WriteLine("Войти(0) или зарегистрироваться(1) или выйти из программы(любое число)?");
            Console.WriteLine();

            int userInput;
            while (!int.TryParse(Console.ReadLine(), out userInput))
            {
                Console.WriteLine("Введи число а не строку");
                Console.WriteLine();

            }
            if (userInput == 0)
            {
                bool loggedIn = false;

                while (!loggedIn)
                {
                    Console.WriteLine("Юзернейм:");
                    Console.WriteLine();

                    string UsernameInput = Console.ReadLine();
                    Console.WriteLine("Пароль:");
                    Console.WriteLine();

                    string PasswordInput = Console.ReadLine();

                    for(int i = 0; i < AllUsers.Count; i++)
                    {
                        if (AllUsers[i].Username == UsernameInput && AllUsers[i].Password == PasswordInput)
                        {
                            Console.WriteLine("Вы успешно вошли в систему");
                            Console.WriteLine();

                            loggedIn = true;
                            currentUser = AllUsers[i];
                            break;
                        }
                    }

                    if (!loggedIn)
                    {
                        Console.WriteLine("Неверный логин или пароль попробуйте снова.");
                        Console.WriteLine();

                        for (int i = 0; i < AllUsers.Count; i++)
                        {
                            Console.WriteLine(AllUsers[i].Username);
                        }

                    }
                }
            }

    
            else if (userInput == 1)
            {
                Guid userGuid = Guid.Empty;
                Console.WriteLine("Хотитеи ввести свой айди(Да) или создать его автоматически(Любые символы)?");
                Console.WriteLine();

                string userchoice = Console.ReadLine().ToLower();
                if (userchoice == "да")
                {
                    Console.WriteLine(
                        "Напишите свой айди:(пример:3f2504e0-4f89-11d3-9a0c-0305e82c3301) сгенерируйте его и вставьте сюда если он будет неправильным то я сам создам тебе айди");
                    Console.WriteLine();

                    string q = Console.ReadLine();
                    if (Guid.TryParse(q, out Guid qw))
                    {
                        Console.WriteLine("Получилось");
                        Console.WriteLine();

                        userGuid = qw;
                    }
                    else
                    {
                        Console.WriteLine("Сам создаю");
                        Console.WriteLine();

                        userGuid = Guid.NewGuid();
                    }
                }
                else
                {
                    Guid qw = Guid.NewGuid();
                    userGuid = qw;
                }
                Guid showroomguidusers = Guid.Empty;
                int cc = 0;
                Console.WriteLine("Введите айди салона если введете неправильно программма не запишет его или же напиши 'нет' если без салона еслли ввод неправильный то будете без айди салона");
                Console.WriteLine();

                string a = Console.ReadLine();
                if (a == "нет")
                {
                    Console.WriteLine("Вы без салона");
                    Console.WriteLine();

                }
                else if (Guid.TryParse(a, out Guid showroomguidusers1))
                {
                    for (int i = 0; i < showrooms.Count; i++)
                    {
                        if (showrooms[i].Id == showroomguidusers1)
                        {
                            Console.WriteLine("Получилось");
                            Console.WriteLine();

                            showroomguidusers = showroomguidusers1;
                            cc = 1;
                            break;
                        }
                    }

                    if (cc == 0)
                    {
                        Console.WriteLine("Салона с таким айди не существует так что у вас будет пустой айди салона");
                        Console.WriteLine();

                    }
                    
                }
                else
                {
                    Console.WriteLine("Ошибка, ваш айди салона пустой"); 
                    Console.WriteLine();

                }

                string username = "a";
                Console.WriteLine("Введите юзернейм");
                
                Console.WriteLine();

                bool usernameexists = true;
                while (usernameexists)
                {
                    string username1 = Console.ReadLine();
                    usernameexists = false;
                    for(int i = 0; i < AllUsers.Count; i++)
                    {
                        if (AllUsers[i].Username == username1)
                        {
                            Console.WriteLine("Этот юзернейм уже существует, попробуйте другой.");
                            Console.WriteLine();

                            usernameexists = true;
                            break;
                        }
                    }

                    if (!usernameexists)
                    {
                        username = username1;
                        break;
                    }

                }


                Console.WriteLine("Введите пароль:");
                Console.WriteLine();

                string password = Console.ReadLine();

                User user = new User(userGuid, showroomguidusers, username, password);
                
                
                AllUsers.Add(user);
                
                
                if (File.Exists(filepathuser))
                {
                    File.Delete(filepathuser);
                }
                string  json = JsonSerializer.Serialize(AllUsers);
                File.WriteAllText(filepathuser, json);
                
                Console.WriteLine("Вы успешно зарегестрировались");
                Console.WriteLine();

                currentUser = user;
                

            }
            else
            {
                exitorcontinue = false;
            }

            while (exitorcontinuesmall)
            {
                Console.WriteLine("Что хотите сделать:");
                Console.WriteLine("Создать салон(1)");
                Console.WriteLine("Редактировать информацию салона(2)");
                Console.WriteLine("Удалить салон(3)");
                Console.WriteLine("Создать машину(4)");
                Console.WriteLine("Удалить машину(5)");
                Console.WriteLine("Изменить информацию об машине(6)");
                Console.WriteLine("Посмотреть все машины(7)");
                Console.WriteLine("Продать машину салона(8)");
                Console.WriteLine("Вывести статистику продаж салона за День,неделю, месяц, год(9)");
                Console.WriteLine("Вывести статистику по марке автомобиля(10)");
                Console.WriteLine("Вывести информацию салона(11)");
                Console.WriteLine("Удалить всю информацию в программе(12)");
                Console.WriteLine("Выйти(любое число)");
                
                int userchoiceafterloggedin;
                while (!int.TryParse(Console.ReadLine(), out userchoiceafterloggedin))
                {
                    Console.WriteLine("Пиши число а не строку");
                    Console.WriteLine();

                    Console.WriteLine();
                }
                switch (userchoiceafterloggedin)
                {
                    case 1:
                        int a = 1;
                        Console.WriteLine("Как будет назывться салон:");
                        Console.WriteLine();

                        string showroomname = Console.ReadLine();
                        Console.WriteLine("Максимум машин в салоне:");
                        Console.WriteLine();

                        int showroomcapacity;
                        while (!int.TryParse(Console.ReadLine(), out showroomcapacity))
                        {
                            Console.WriteLine("Надо число же");
                            Console.WriteLine();

                        }
                        for(int i = 0; i < showrooms.Count; i++)
                        {
                            if (currentUser.ShowroomId == showrooms[i].Id)
                            {
                                Console.WriteLine("Вы уходите из салона и создаете свой");
                                Console.WriteLine();

                                showrooms.Remove(showrooms[i]);
                                a = 2;
                            }
                        }

                        if (a == 2)
                        {
                            Showroom showroom = new Showroom(showroomname, showroomcapacity);
                            showrooms.Add(showroom);
                            currentUser.ShowroomId = showroom.Id;
                            
                        }

                        else
                        {
                            Console.WriteLine("Салон успешно создан");
                            Console.WriteLine();

                            Showroom showroom = new Showroom(showroomname, showroomcapacity);
                            showrooms.Add(showroom);
                            currentUser.ShowroomId = showroom.Id;
                            
                        }
                        if (File.Exists(filepathshowroom))
                        {
                            File.Delete(filepathshowroom);
                        }
                        string json234 = JsonSerializer.Serialize(showrooms);
                        File.WriteAllText(filepathshowroom, json234);

                        if (File.Exists(filepathuser))
                        {
                            File.Delete(filepathuser);
                        }
                        
                        string json111 = JsonSerializer.Serialize(AllUsers);
                        File.WriteAllText(filepathuser, json111);
                        
                        
                        break;
                    case 2:

                        if (currentUser.ShowroomId == Guid.Empty)
                        {
                            Console.WriteLine("У вас нет автосалона");
                            Console.WriteLine();

                            break;
                        }
                        Console.WriteLine(
                            "Менять название(0), купить машину(любое число)");
                        Console.WriteLine();

                        int choice;
                        while (!int.TryParse(Console.ReadLine(), out choice))
                        {
                            Console.WriteLine("Число пиши");
                            Console.WriteLine();

                        }
                        if (choice == 0)
                        {
                            Console.WriteLine("Новое название:");
                            Console.WriteLine();

                            string newname = Console.ReadLine();
                            for (int i = 0; i < showrooms.Count; i++)  
                            {
                                if (showrooms[i].Id == currentUser.ShowroomId)
                                {
                                    showrooms[i].Name = newname;
                                }
                            }
                            
                            
                            Console.WriteLine("Название изменено");
                            Console.WriteLine();

                        }

                        else
                        {
                            for(int i = 0; i < showrooms.Count; i++)
                            {
                                if (showrooms[i].Id == currentUser.ShowroomId)
                                {
                                    Car? p = showrooms[i].buycar();
                                    if (p == null)
                                    {
                                        break;
                                    }

                                    Random random = new Random();

                                    int randomnumber = random.Next(1, 3);
                                    if (randomnumber == 1)
                                    {
                                        Console.WriteLine("Такой машины нет");
                                        Console.WriteLine();

                                        break;
                                    }

                                    showrooms[i].Cars.Add(p);
                                    Console.WriteLine("Нашел такую машину и добавил машину");
                                    Console.WriteLine();

                                    break;
                                }
                            }
                        }
                        if (File.Exists(filepathshowroom))
                        {
                            File.Delete(filepathshowroom);
                        }
                        string json2344 = JsonSerializer.Serialize(showrooms);
                        File.WriteAllText(filepathshowroom, json2344);

                        if (File.Exists(filepathuser))
                        {
                            File.Delete(filepathuser);
                        }
                        
                        string json1114 = JsonSerializer.Serialize(AllUsers);
                        File.WriteAllText(filepathuser, json1114);
                        break;

                    case 3:
                        if (currentUser.ShowroomId == Guid.Empty)
                        {
                            Console.WriteLine("У вас нет автосалона");
                            Console.WriteLine();

                            break;
                        }
                        for(int i = 0; i < showrooms.Count; i++)   
                        {
                            if (showrooms[i].Id == currentUser.ShowroomId)
                            {
                                showrooms.Remove(showrooms[i]);
                                currentUser.ShowroomId = Guid.Empty;
                                Console.WriteLine("Салон удален");
                                Console.WriteLine();

                            }
                        }
                        if (File.Exists(filepathshowroom))
                        {
                            File.Delete(filepathshowroom);
                        }
                        string json2324 = JsonSerializer.Serialize(showrooms);
                        File.WriteAllText(filepathshowroom, json2324);

                        if (File.Exists(filepathuser))
                        {
                            File.Delete(filepathuser);
                        }
                        
                        string json131 = JsonSerializer.Serialize(AllUsers);
                        File.WriteAllText(filepathuser, json131);
                        break;
                    case 4:
                        if (currentUser.ShowroomId == Guid.Empty)
                        {
                            Console.WriteLine("У вас нет автосалона");
                            Console.WriteLine();

                            break;
                        }
                        for(int i = 0; i < showrooms.Count; i++)
                        {
                            if (showrooms[i].Id == currentUser.ShowroomId)
                            {
                                showrooms[i].CreateCar();
                            }
                        }
                        if (File.Exists(filepathshowroom))
                        {
                            File.Delete(filepathshowroom);
                        }
                        string json1234 = JsonSerializer.Serialize(showrooms);
                        File.WriteAllText(filepathshowroom, json1234);

                        if (File.Exists(filepathuser))
                        {
                            File.Delete(filepathuser);
                        }
                        
                        string json12111 = JsonSerializer.Serialize(AllUsers);
                        File.WriteAllText(filepathuser, json12111);
                        break;
                    case 5:
                        if (currentUser.ShowroomId == Guid.Empty)
                        {
                            Console.WriteLine();

                            Console.WriteLine("У вас нет автосалона");
                            break;
                        }
                        int b = 1;
                        Console.WriteLine("Модель машины:");
                        Console.WriteLine();

                        string mod = Console.ReadLine();
                        for (int i = 0; i < showrooms.Count; i++)
                        {
                            if (showrooms[i].Id == currentUser.ShowroomId)
                            {
                                for (int j = 0; j < showrooms[i].Cars.Count; j++)
                                {
                                    if (showrooms[i].Cars[j].Model == mod)
                                    {
                                        showrooms[i].Cars.Remove(showrooms[i].Cars[j]);
                                        Console.WriteLine("Машина удалена");
                                        Console.WriteLine();

                                        b = 2;
                                        break;
                                    }
                                }
                                    
                            }
                        }
                        if (b == 2)
                        {
                            break;
                        }
                        Console.WriteLine("Такой машины итак не существует у вас");
                        Console.WriteLine();
                        if (File.Exists(filepathshowroom))
                        {
                            File.Delete(filepathshowroom);
                        }
                        string json123234 = JsonSerializer.Serialize(showrooms);
                        File.WriteAllText(filepathshowroom, json123234);

                        if (File.Exists(filepathuser))
                        {
                            File.Delete(filepathuser);
                        }
                        
                        string json123111 = JsonSerializer.Serialize(AllUsers);
                        File.WriteAllText(filepathuser, json123111);

                        break;
                    case 6:
                        if (currentUser.ShowroomId == Guid.Empty)
                        {
                            Console.WriteLine("У вас нет автосалона");
                            Console.WriteLine();

                            break;
                        }
                        Console.WriteLine("Айди:");
                        Console.WriteLine();

                        Guid carid;
                        while (!Guid.TryParse(Console.ReadLine(), out carid))
                        {
                            Console.WriteLine("Пиши нормально айди");
                            Console.WriteLine();

                        }
                        for (int i = 0; i < showrooms.Count; i++)
                        {
                            if (showrooms[i].Id == currentUser.ShowroomId)
                            {
                                for (int j = 0; j < showrooms[i].Cars.Count; j++)
                                {
                                    if (showrooms[i].Cars[j].Id == carid)
                                    {
                                        Console.WriteLine("Что менять модель(1) бренд(2) год создания(любое число)");
                                        Console.WriteLine();

                                        int userchoicee;
                                        while (!int.TryParse(Console.ReadLine(), out userchoicee))
                                        {
                                            Console.WriteLine("Пиши число");
                                            Console.WriteLine();

                                        }
                                        if (userchoicee == 1)
                                        {
                                            Console.WriteLine("Новая модель:");
                                            Console.WriteLine();

                                            string model = Console.ReadLine();
                                            showrooms[i].Cars[j].Model = model;
                                        }
                                        else if (userchoicee == 2)
                                        {
                                            Console.WriteLine("Новый бренд:");
                                            Console.WriteLine();

                                            string brand = Console.ReadLine();
                                            showrooms[i].Cars[j].Make = brand;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Новый год создания");
                                            Console.WriteLine();

                                            DateTime year;
                                            while (!DateTime.TryParse(Console.ReadLine(), out year))
                                            {
                                                Console.WriteLine("Нормально вводи дату");
                                                Console.WriteLine();

                                            }
                                            showrooms[i].Cars[j].Year = year;
                                        }
                                        break;
                                    }
                                }

                                Console.WriteLine("Машины с таким айди не существует");
                                Console.WriteLine();

                                break;
                            }
                        }
                        if (File.Exists(filepathshowroom))
                        {
                            File.Delete(filepathshowroom);
                        }
                        string json234123 = JsonSerializer.Serialize(showrooms);
                        File.WriteAllText(filepathshowroom, json234123);

                        if (File.Exists(filepathuser))
                        {
                            File.Delete(filepathuser);
                        }
                        
                        string json111123 = JsonSerializer.Serialize(AllUsers);
                        File.WriteAllText(filepathuser, json111123);
                        break;
                    case 7:
                        if (currentUser.ShowroomId == Guid.Empty)
                        {
                            Console.WriteLine("У вас нет автосалона");
                            Console.WriteLine();

                            break;
                        }
                        for (int i = 0; i < showrooms.Count; i++)
                        {
                            if (currentUser.ShowroomId == showrooms[i].Id)
                            {
                                for (int j = 0; j < showrooms[i].Cars.Count; j++)
                                {
                                    showrooms[i].Cars[j].print();
                                }
                            }
                        }
                        if (File.Exists(filepathshowroom))
                        {
                            File.Delete(filepathshowroom);
                        }
                        string json23445 = JsonSerializer.Serialize(showrooms);
                        File.WriteAllText(filepathshowroom, json23445);

                        if (File.Exists(filepathuser))
                        {
                            File.Delete(filepathuser);
                        }
                        
                        string json11145 = JsonSerializer.Serialize(AllUsers);
                        File.WriteAllText(filepathuser, json11145);
                        break;
                    case 8:
                        int c = 0;
                        if (currentUser.ShowroomId == Guid.Empty)
                        {
                            Console.WriteLine("У вас нет автосалона");
                            Console.WriteLine();

                            break;
                        }

                        for (int i = 0; i < showrooms.Count; i++)
                        {
                            if (showrooms[i].Id == currentUser.ShowroomId)
                            {
                                for (int j = 0; j < showrooms[i].Cars.Count; j++)
                                {
                                    showrooms[i].Cars[j].print();                                    
                                }
                            }
                        }
                        Console.WriteLine("Какую машину продать?(напишите айди)");
                        Console.WriteLine();


                        Guid id;
                        while (!Guid.TryParse(Console.ReadLine(), out id))
                        {
                            Console.WriteLine("Нормально пиши айди");
                            Console.WriteLine();

                        }
                        for (int i = 0; i < showrooms.Count; i++)
                        {
                            if (showrooms[i].Id == currentUser.ShowroomId)
                            {
                                for (int j = 0; j < showrooms[i].Cars.Count; j++)
                                {
                                    if (showrooms[i].Cars[j].Id == id)
                                    {
                                        Console.WriteLine("Цена:");
                                        Console.WriteLine();

                                        int price;
                                        while (!int.TryParse(Console.ReadLine(), out price))
                                        {
                                            Console.WriteLine("Пиши число а не строку");
                                            Console.WriteLine();

                                        }
                                        Console.WriteLine("Дата:");
                                        Console.WriteLine();

                                        DateTime date;
                                        while (!DateTime.TryParse(Console.ReadLine(), out date))
                                        {
                                            Console.WriteLine("Нормально вводи дату");  
                                            Console.WriteLine();

                                        }
                                        currentUser.SellCar(showrooms, showrooms[i].Cars[j], price, date);
                                        c = 1;
                                        break;
                                    }
                                }
                            }
                        }

                        if (c == 0)
                        {
                            Console.WriteLine("В автосалоне нет машины с таким айди");
                            Console.WriteLine();

                        }
                        if (File.Exists(filepathshowroom))
                        {
                            File.Delete(filepathshowroom);
                        }
                        string json23433 = JsonSerializer.Serialize(showrooms);
                        File.WriteAllText(filepathshowroom, json23433);

                        if (File.Exists(filepathuser))
                        {
                            File.Delete(filepathuser);
                        }
                        
                        string json11133 = JsonSerializer.Serialize(AllUsers);
                        File.WriteAllText(filepathuser, json11133);
                        break;
                    case 9 :
                        int dayincase = 0;
                        int monthincase = 0;
                        int yearincase = 0;
                        if (currentUser.ShowroomId == Guid.Empty)
                        {
                            Console.WriteLine("У вас нет автосалона");
                            Console.WriteLine();

                            break;
                        }

                        Console.WriteLine("Вывести иенформацию по дню(0) месяцу(1) или году(люое число)");
                        Console.WriteLine();

                        int userchoice;
                        while (!int.TryParse(Console.ReadLine(), out userchoice))
                        {
                            Console.WriteLine("Введи число а не строку");
                            Console.WriteLine();

                        }
                        switch (userchoice)
                        {
                            case 0:
                                Console.WriteLine("Напиши номер дня:");
                                Console.WriteLine();

                                int day1;
                                while (!int.TryParse(Console.ReadLine(), out day1))
                                {
                                    Console.WriteLine("Число пиши");
                                    Console.WriteLine();

                                }
                                dayincase = day1;
                                break;
                            case 1:
                                Console.WriteLine("Напииши номер месяца");
                                Console.WriteLine();

                                int month1;
                                while (!int.TryParse(Console.ReadLine(), out month1))
                                {
                                    Console.WriteLine("Число пиши");
                                    Console.WriteLine();

                                }
                                monthincase = month1;
                                break;
                            default:
                                Console.WriteLine("Напиши год");
                                Console.WriteLine();

                                int year1;
                                while (!int.TryParse(Console.ReadLine(), out year1))
                                {
                                    Console.WriteLine("Число пиши");
                                    Console.WriteLine();

                                }
                                yearincase = year1;
                                break;
                        }
                        for (int i = 0; i < showrooms.Count; i++)
                        {
                            if (showrooms[i].Id == currentUser.ShowroomId)
                            {
                                if (userchoice == 0)
                                {
                                    for(int j = 0; j < showrooms[i].Sales.Count; j++)
                                    {
                                        if (showrooms[i].Sales[j].SaleDate.Day == dayincase)
                                        {
                                            showrooms[i].Sales[j].print();
                                            break;
                                        }
                                    }
                                }
                                else if (userchoice == 1)
                                {
                                    for (int j = 0; j < showrooms[i].Sales.Count; j++)
                                    {
                                        if (showrooms[i].Sales[j].SaleDate.Month == monthincase)
                                        {
                                            showrooms[i].Sales[j].print();
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int j = 0; j < showrooms[i].Sales.Count; i++)
                                    {
                                        if (showrooms[i].Sales[j].SaleDate.Year == yearincase)
                                        {
                                            showrooms[i].Sales[j].print();
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        if (File.Exists(filepathshowroom))
                        {
                            File.Delete(filepathshowroom);
                        }
                        string json23477 = JsonSerializer.Serialize(showrooms);
                        File.WriteAllText(filepathshowroom, json23477);

                        if (File.Exists(filepathuser))
                        {
                            File.Delete(filepathuser);
                        }
                        
                        string json11177 = JsonSerializer.Serialize(AllUsers);
                        File.WriteAllText(filepathuser, json11177);
                        break;
                    case 10:
                        int y = 1;
                        if (currentUser.ShowroomId == Guid.Empty)
                        {
                            Console.WriteLine("У вас нет автосалона");
                            Console.WriteLine();

                            break;
                        }

                        Console.WriteLine("Напиши марку машины");
                        Console.WriteLine();

                        string input = Console.ReadLine();
                        for(int i = 0; i < showrooms.Count; i++)
                        {
                            if (showrooms[i].Id == currentUser.ShowroomId)
                            {
                                for(int j = 0; j < showrooms[i].Cars.Count; j++)
                                {
                                    if (showrooms[i].Cars[j].Make == input)
                                    {
                                        y = 0;
                                        showrooms[i].Cars[j].print();
                                        break;
                                    }
                                }
                            }
                        }

                        if (y == 0)
                        {
                            break;
                        }
                        
                        Console.WriteLine("Машин с такой маркой нет");
                        Console.WriteLine();
                        
                        if (File.Exists(filepathshowroom))
                        {
                            File.Delete(filepathshowroom);
                        }
                        string json234321 = JsonSerializer.Serialize(showrooms);
                        File.WriteAllText(filepathshowroom, json234321);

                        if (File.Exists(filepathuser))
                        {
                            File.Delete(filepathuser);
                        }
                        
                        string json11131 = JsonSerializer.Serialize(AllUsers);
                        File.WriteAllText(filepathuser, json11131);
                        break;
                    case 11:
                        if (currentUser.ShowroomId == Guid.Empty)
                        {
                            Console.WriteLine("У вас нет автосалона");
                            Console.WriteLine();

                            break;
                        }

                        for (int i = 0; i < showrooms.Count; i++)
                        {
                            if (showrooms[i].Id == currentUser.ShowroomId)
                            {
                                showrooms[i].print();
                            }
                        }
                        if (File.Exists(filepathshowroom))
                        {
                            File.Delete(filepathshowroom);
                        }
                        string json234113 = JsonSerializer.Serialize(showrooms);
                        File.WriteAllText(filepathshowroom, json234113);

                        if (File.Exists(filepathuser))
                        {
                            File.Delete(filepathuser);
                        }
                        
                        string json111113 = JsonSerializer.Serialize(AllUsers);
                        File.WriteAllText(filepathuser, json111113);
                        break;
                    case 12:
                        Console.WriteLine("Удаляю всех пользователей");
                        AllUsers.Clear();
                        showrooms.Clear();
                        if (File.Exists(filepathshowroom))
                        {
                            File.Delete(filepathshowroom);
                        }
                        string json234312 = JsonSerializer.Serialize(showrooms);
                        File.WriteAllText(filepathshowroom, json234312);

                        if (File.Exists(filepathuser))
                        {
                            File.Delete(filepathuser);
                        }
                        
                        string json111312 = JsonSerializer.Serialize(AllUsers);
                        File.WriteAllText(filepathuser, json111312);
                        break;
                    default:
                        exitorcontinuesmall = false;
                        exitorcontinue = false;
                        break;
                }

            }


        }
        
        
    }

}

