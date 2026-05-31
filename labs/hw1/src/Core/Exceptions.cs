using System;

namespace hw1.Generics.Core
{
    // виняток для некоректних вхідних даних або параметрів
    public class InvalidInputException : Exception
    {
        // передає повідомлення у базовий клас Exception
        public InvalidInputException(string message) : base(message) { }
    }

    //  вийняток елемент не знайдено
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
