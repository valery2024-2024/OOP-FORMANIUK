using System;

namespace IndependentWork16
{
    interface IBookingRepository
    {
        void Save(string user, DateTime date);
    }
}
