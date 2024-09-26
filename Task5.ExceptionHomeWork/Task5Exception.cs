namespace Task5.ExceptionHomeWork
{
    public static class Task5Exception
    {
        static UserManager userManager { get; set; }
        public static void Main()
        {
            userManager = new UserManager("users.txt");
            userManager.ReadFileUsers();

            ExecutionAction();
        }

        public static int GetAction()
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1 - Вывести весь список пользователей");
            Console.WriteLine("2 - Добавить пользователя");
            Console.WriteLine("3 - Поиск пользователя по id");
            Console.WriteLine("4 - Удалить информацию о пользователе");

            Console.WriteLine("<Esc> - Завершить работу и закрыть программу");
            Console.WriteLine();

            return (int)Console.ReadKey(true).Key;
        }

        static void ExecutionAction()
        {
            int action = GetAction();

            try
            {
                if (action == (int)ConsoleKey.D1 || action == (int)ConsoleKey.NumPad1)
                {
                    foreach (var user in userManager.ListUsers())
                    {
                        Console.WriteLine(user);
                    }
                }
                else if (action == (int)ConsoleKey.D2 || action == (int)ConsoleKey.NumPad2)
                {
                    userManager.AddUser();
                }
                else if (action == (int)ConsoleKey.D3 || action == (int)ConsoleKey.NumPad3)
                {
                    int id = userManager.GetUserId();
                    Console.WriteLine(userManager.GetUser(id));
                }
                else if (action == (int)ConsoleKey.D4 || action == (int)ConsoleKey.NumPad4)
                {
                    int id = userManager.GetUserId();
                    userManager.RemoveUser(id);
                }
                else if (action == (int)ConsoleKey.Escape)
                {
                    userManager.SaveUserList();
                    Environment.Exit(0);
                }
                else
                {
                    throw new InvalidActionException("Не верно выбрано действие");
                }
            }
            catch (InvalidActionException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidIdException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (UserNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (UserAlreadyExistsException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine();
                ExecutionAction();
            }
        }
    }
}
