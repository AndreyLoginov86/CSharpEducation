using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.ExceptionHomeWork
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public override string ToString() 
        {
            return $"ID: {this.Id}; Имя: {this.Name}; Email: {this.Email}";
        }

        public User(int id, string name, string email) 
        {
            this.Name = name;
            this.Email = email;
            this.Id = id;
        }

        public string GetStringUser()
        {
            var str = new StringBuilder();
            str.Append($"{this.Id};");
            str.Append($"{this.Name};");
            str.Append($"{this.Email}");
            return str.ToString();
        }
    }
}
