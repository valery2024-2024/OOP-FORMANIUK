using System;

namespace IndependentWork16
{
    class Program
    {
        static void Main()
        {
            var service = new BookingService(
                new BookingValidator(),
                new AvailabilityChecker(),
                new BookingRepository(),
                new ConfirmationSender()
            );

            service.CreateBooking("Valerii", DateTime.Now);
        }
    }
}
