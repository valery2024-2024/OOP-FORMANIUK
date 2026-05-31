using System;

namespace IndependentWork16
{
    class BookingRepository : IBookingRepository
    {
        public void Save(string user, DateTime date)
        {
            Console.WriteLine("Booking saved");
        }
    }
}
