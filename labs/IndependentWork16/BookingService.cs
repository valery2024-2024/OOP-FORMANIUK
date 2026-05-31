using System;

namespace IndependentWork16
{
    class BookingService
    {
        private readonly IBookingValidator _validator;
        private readonly IAvailabilityChecker _checker;
        private readonly IBookingRepository _repository;
        private readonly IConfirmationSender _sender;

        public BookingService(
            IBookingValidator validator,
            IAvailabilityChecker checker,
            IBookingRepository repository,
            IConfirmationSender sender)
        {
            _validator = validator;
            _checker = checker;
            _repository = repository;
            _sender = sender;
        }

        public void CreateBooking(string user, DateTime date)
        {
            if (!_validator.Validate(user))
            {
                Console.WriteLine("Validation failed");
                return;
            }

            if (!_checker.IsAvailable(date))
            {
                Console.WriteLine("Date not available");
                return;
            }

            _repository.Save(user, date);
            _sender.Send(user);
        }
    }
}
