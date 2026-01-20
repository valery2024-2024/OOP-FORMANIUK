using System;

namespace IndependentWork16
{
    class BookingValidator : IBookingValidator
    {
        public bool Validate(string user)
        {
            return !string.IsNullOrEmpty(user);
        }
    }
}
