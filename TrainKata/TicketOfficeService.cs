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

            List<Seat> seats = new List<Seat>();
            if (coachWithEnoughAvailableSeats != null)
            {
                var list = new List<Seat>();
                int limit = reservationRequest.SeatCount;
                foreach (Seat seat1 in coachWithEnoughAvailableSeats.Seats)
                {
                    if ("".Equals(seat1.BookingReference))
                    {
                        if (limit-- == 0) break;
                        Seat seat = new Seat(seat1.Coach, seat1.SeatNumber, null);
                        list.Add(seat);
                    }
                }
                seats = list;
            }

            if (seats.Any())
            {
                ReservationResponseDto reservation = new ReservationResponseDto(reservationRequest.TrainId, _bookingReferenceClient.GenerateBookingReference(), seats);
                return "{" +
                        "\"train_id\": \"" + reservation.TrainId + "\", " +
                        "\"booking_reference\": \"" + reservation.BookingId + "\", " +
                        "\"seats\": [" + string.Join(", ", reservation.Seats.Select(s => "\"" + s.SeatNumber + s.Coach + "\"")) + "]" +
                        "}";
            }
            else
            {
                return "{\"train_id\": \"" + reservationRequest.TrainId + "\", \"booking_reference\": \"\", \"seats\": []}";
            }
        }
    }
}
