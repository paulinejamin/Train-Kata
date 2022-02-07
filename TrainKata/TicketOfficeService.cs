using System.Collections.Generic;
using System.Linq;

namespace TrainKata
{
    public class TicketOfficeService
    {
        private readonly IProvideTrains _provideTrains;
        private readonly IBookingReferenceClient _bookingReferenceClient;

        public TicketOfficeService(IBookingReferenceClient bookingReferenceClient, IProvideTrains provideTrains)
        {
            _bookingReferenceClient = bookingReferenceClient;
            _provideTrains = provideTrains;
        }

        public string MakeReservation(ReservationRequestDto reservationRequest)
        {
            var train = _provideTrains.GetTrain(reservationRequest.TrainId);

            var coachWithEnoughAvailableSeats = train.FindCoachWithEnoughAvailableSeats(reservationRequest.SeatCount);

            if (coachWithEnoughAvailableSeats == null)
            {
                var reservation = new ReservationResponseDto(reservationRequest.TrainId,
                    "", new List<Seat>());
                return reservation.ToString();
            }

            var seats = coachWithEnoughAvailableSeats.GetAvailableSeats(reservationRequest.SeatCount);
            var bookingReference = _bookingReferenceClient.GenerateBookingReference();
            var reservation2 = new ReservationResponseDto(reservationRequest.TrainId,
                bookingReference, seats);

            return reservation2.ToString();
        }
    }
}