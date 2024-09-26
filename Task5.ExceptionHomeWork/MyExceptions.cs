using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Task5.ExceptionHomeWork
{
    /// <summary>
    /// Не верно введён Id пользователя например текст вместо числа
    /// </summary>
    public class InvalidIdException : Exception 
    {
        public InvalidIdException(string message): base(message) { }
    }

    public class InvalidActionException : Exception
    {
        public InvalidActionException(string message) : base(message) { }
    }

    /// <summary>
    /// Пользователь не найден
    /// </summary>
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message): base(message) { }
    }

    /// <summary>
    /// Такой пользователь уже есть в списке
    /// </summary>
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string message) : base(message) { }
    }
}
