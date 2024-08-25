using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Task3.PhoneBook
{
    internal class PhoneBook
    {
        private static PhoneBook instance;

        
        public string NameFilePhoneBook { get; set; }
        
        /// <summary>
        /// Коллекция экземпляров класса абонент для работы с телефонной книгой
        /// </summary>
        public List<Abonent> listAbonent = new List<Abonent> { };

        private PhoneBook(string nameFilePhoneBook)
        {
            this.NameFilePhoneBook = nameFilePhoneBook;
        }

        public static PhoneBook getInstance(string nameFilePhoneBook)
        {
            if (instance == null)
                instance = new PhoneBook(nameFilePhoneBook);
            return instance;
        }

        /// <summary>
        /// Проверка существования файла телефонной книги
        /// </summary>
        private bool CheckExistsFile(string nameFilePhoneBoo)
        {
            FileInfo phoneBookFileInfo = new FileInfo(this.NameFilePhoneBook);

            return phoneBookFileInfo.Exists;
        }

        /// <summary>
        /// Загружаем все записи из файла телефонной книги в коллекцию
        /// </summary>
        public async void GetAllPhoneBook()
        {
            if (!CheckExistsFile(this.NameFilePhoneBook))
            {
                Console.WriteLine("Файл не найден");
            }
            else
            {
                string[] fileTextlines = File.ReadAllLines(this.NameFilePhoneBook);
                foreach (string line in fileTextlines)
                {
                    string[] abonentInfo = line.Split(';');

                    var abonent = new Abonent(name: abonentInfo[0], numberPhone: abonentInfo[1]);
                    listAbonent.Add(abonent);
                }
                
            }
        }

        /// <summary>
        /// Вывод на экран всех записей телефонной книги
        /// </summary>
        public void WriteAllPhoneBook()
        {
            for (int i = 0; i < listAbonent.Count; i++)
            {
                Console.WriteLine(i+1 + ") " + listAbonent[i].GetInfoAbonent());
            }
        }

        /// <summary>
        /// Сохранение (перерезапись) списка абоненто из коллекции в файл телефонной книги
        /// </summary>
        public void SavePhoneBook()
        {
           string[] abonentListString = new string[listAbonent.Count];

           int i = 0;
           foreach (var item in listAbonent)
           {
              abonentListString[i] = item.GetStringAbonent();
              i++;
           }
           File.WriteAllLines(this.NameFilePhoneBook, abonentListString);           
        }

        /// <summary>
        /// Добавление абонента
        /// </summary>
        public void AddAbonent()
        {
            int indexAbonent;
            
            Console.Write("Введите имя абонента: ");
            string nameAbonent = Console.ReadLine();

            indexAbonent = FindAbonent(nameAbonent);
            if (indexAbonent != -1)
            {
                Console.WriteLine("Абонент с таким именем уже есть в базе");
                return;
            }

            Console.Write("Введите номер телефона: ");
            string phone = Console.ReadLine();

            indexAbonent = FindAbonent(nameAbonent);
            if (indexAbonent != -1)
            {
                Console.WriteLine("Абонент с таким номером телефона уже есть в базе");
                return;
            }

            listAbonent.Add(new Abonent(name: nameAbonent, numberPhone: phone));
        }

        /// <summary>
        /// Поиск абонента по имени либо телефону
        /// </summary>
        /// <param name="filterString">имя или номер телефона</param>
        /// <returns>Возвращает индекс абонента в коллекции если абонент не найден возвращает -1 </returns>
        public int FindAbonent(string filterString)
        {
            int i = 0;
            foreach (var item in listAbonent)
            {
               if (item.Name == filterString || item.NumberPhone == filterString) 
               {
                    return i;
               }
               i++;
            }
            return -1;
        }
    }
}
