namespace Task3.PhoneBook
{
    public class Task3PhoneBook
    {
        static PhoneBook phoneBook { get; set; }
        public static void Main()
        {
            phoneBook = PhoneBook.getInstance("phonebook.txt");
            
            phoneBook.GetAllPhoneBook(); //При запуске программы считываем телефонную книгу в коллекцию

            ExecutionAction();
        }

        static int GetAction()
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1 - Вывести весь список абонентов");
            Console.WriteLine("2 - Добавить абонента");
            Console.WriteLine("3 - Редактировать информацию об абоненте");
            Console.WriteLine("4 - Поиск абонента");
            Console.WriteLine("5 - Удалить информацию об абоненте");

            Console.WriteLine("<Esc> - Завершить работу и закрыть программу");
            Console.WriteLine();

            return (int) Console.ReadKey(true).Key;
        }
        
        /// <summary>
        /// Выполняем выбранное действие с телефонной книгой
        /// </summary>
        static void ExecutionAction()
        {
            int action = GetAction();

            if (action == (int)ConsoleKey.D1 || action == (int)ConsoleKey.NumPad1)
            {
                phoneBook.WriteAllPhoneBook();
            }
            else if (action == (int)ConsoleKey.D2 || action == (int)ConsoleKey.NumPad2)
            {
                phoneBook.AddAbonent();
            }
            else if (action == (int)ConsoleKey.D3 || action == (int)ConsoleKey.NumPad3)
            {
                Console.Write("Введите номер телефона либо имя редактируемого абаонента: ");
                string filterString = Console.ReadLine();

                int indexAbonent = phoneBook.FindAbonent(filterString);

                if (indexAbonent == -1)
                {
                    Console.WriteLine(phoneBook.listAbonent[indexAbonent].GetInfoAbonent());

                    Console.Write("Введите новое имя: ");
                    phoneBook.listAbonent[indexAbonent].Name = Console.ReadLine();

                    Console.Write("Введите новый номер телефона: ");
                    phoneBook.listAbonent[indexAbonent].NumberPhone = Console.ReadLine();
                }
                else
                    Console.WriteLine("Абонент не найден");

            }
            else if (action == (int)ConsoleKey.D4 || action == (int)ConsoleKey.NumPad4)
            {
                Console.Write("Введите номер телефона либо имя абонента: ");
                string filterString = Console.ReadLine();

                int indexAbonent = phoneBook.FindAbonent(filterString);

                if (indexAbonent == -1)
                    Console.WriteLine(phoneBook.listAbonent[indexAbonent].GetInfoAbonent());
                else
                    Console.WriteLine("Абонент не найден");
            }
            else if (action == (int)ConsoleKey.Escape)
            {
                phoneBook.SavePhoneBook(); // при завершении программы перезаписываем файл телефонной книги из коллекции
                Environment.Exit(0);
            }
            else if (action == (int)ConsoleKey.D5 || action == (int)ConsoleKey.NumPad5)
            {
                Console.Write("Введите номер телефона либо имя удаляемого абонента: ");
                string filterString = Console.ReadLine();

                int indexAbonent = phoneBook.FindAbonent(filterString);

                if (indexAbonent == -1)
                {
                    phoneBook.listAbonent.RemoveAt(indexAbonent);
                    Console.WriteLine("Информация об абоненте удалена");
                }
                else
                    Console.WriteLine("Абонент не найден");

            }
            else
            {
                Console.WriteLine("Не верный выбор");
                Console.WriteLine();
            }
            ExecutionAction();
        }
    }
}
