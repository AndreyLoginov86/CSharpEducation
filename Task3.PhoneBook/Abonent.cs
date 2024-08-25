using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.PhoneBook
{
    internal class Abonent
    {
        private string name;
        private string numberPhone;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        public string NumberPhone
        {
            get
            {
                return this.numberPhone;
            }
            set
            {
                this.numberPhone = value;
            }
        }

        /// <summary>
        /// Формируем строку с информацией об абоненте для вывода на экран
        /// </summary>
        /// <returns></returns>
        public string GetInfoAbonent()
        {
            var info = new StringBuilder();
            info.Append($"Имя: {this.name}; ");
            info.AppendLine($"Номер телефона: {this.NumberPhone}");

            return info.ToString();
        }

        /// <summary>
        /// Формируем строку с информацией об абоненте для записи в файл
        /// </summary>
        /// <returns></returns>
        public string GetStringAbonent()
        {
            var str = new StringBuilder();
            str.Append($"{this.name};");
            str.Append($"{this.NumberPhone}");
            return str.ToString();
        }

        public Abonent(string name, string numberPhone)
        {
            this.name = name;
            this.numberPhone = numberPhone;
        }
    }
}
