using System;

namespace lab5v19.Exceptions
{
    // Власний виняток: кидається, якщо предмет не влазить у жоден бокс
    public class ItemTooLargeException : Exception
    {
        public ItemTooLargeException(string message) : base(message) { }
    }
}
