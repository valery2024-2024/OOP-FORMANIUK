using System;

namespace IndependentWork16
{
    class ConfirmationSender : IConfirmationSender
    {
        public void Send(string user)
        {
            Console.WriteLine("Confirmation sent to " + user);
        }
    }
}
