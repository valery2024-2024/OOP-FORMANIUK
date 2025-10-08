using System;

namespace lab5v11.Exceptions
{
    // Власний виняток, який сигналізує, що потрібний об’єкт не знайдено
    public class NotFoundException : Exception
    {
        public NotFoundException() { }
        public NotFoundException(string message) : base(message) { }
    }
}
