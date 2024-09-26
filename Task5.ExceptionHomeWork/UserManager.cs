using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.ExceptionHomeWork
{
    public class UserManager
    { 
        public List<User> users = new List<User>();
        public string FileName { get; set; }

        public UserManager(string fileName)
        {  
            FileName = fileName; 
        }
        
        public List<User> ListUsers()
        {
          return this.users;
        }

        public void ReadFileUsers()
        {
            string[] fileTextlines = { };

            try
            {
                fileTextlines = File.ReadAllLines(this.FileName);
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine($"{this.FileName} не найден проверьте наличие файла и нажмите <Enter>");
                Console.ReadLine();
                ReadFileUsers();
            }            
            
            foreach (string line in fileTextlines)
            {
                string[] userInfo = line.Split(';');

                var user = new User(id: int.Parse(userInfo[0]), name: userInfo[1], email: userInfo[2]);
                users.Add(user);
            }
        }

        public int GetUserId()
        {
            Console.Write("Введите Id пользователя: ");
            string stringId = Console.ReadLine();
            int id;
            if (!int.TryParse(stringId, out id))
                throw new InvalidIdException("Не верно введён Id");

            return id;
        } 
        
        public bool ExistsUserId(int id)
        {
            return users.Exists(u => u.Id == id);
        }

        public bool ExistsUserName(string name)
        {
            return users.Exists(u => u.Name == name);
        }

        public User GetUser(int id)
        {
            if (!ExistsUserId(id))
              throw new UserNotFoundException("Пользователь с заданным ID не найден");
            
            User user = users.Find(u => u.Id == id);

            return user;
        }

        public void AddUser()
        {
            int userId = GetUserId();
            if (ExistsUserId(userId))
                throw new UserAlreadyExistsException("Пользователь с заданным Id уже есть в списке");

            Console.Write("Введите Имя пользователя: ");
            string userName = Console.ReadLine();
            if (ExistsUserName(userName))
                throw new UserAlreadyExistsException("Пользователь с заданным Именем уже есть в списке");

            Console.Write("Введите Email пользователя: ");
            string userEmail = Console.ReadLine();

            var user = new User(id: userId, name: userName, email: userEmail);
            users.Add(user);
            Console.WriteLine("Информация о пользователе добавлена");
        }

        public void RemoveUser(int id)
        {
            if (!ExistsUserId(id))
                throw new UserNotFoundException("Пользователь с заданным ID не найден");

            User user = users.Find(u => u.Id == id);
            users.Remove(user);
            Console.WriteLine("Информация о пользователе удалена");
        }

        public void SaveUserList()
        {
            string[] userListString = new string[users.Count];

            int i = 0;
            foreach (var item in users)
            {
                userListString[i] = item.GetStringUser();
                i++;
            }
            File.WriteAllLines(this.FileName, userListString);
        }
    }
}
