using System.Collections.Generic;
using System.Linq;

namespace TrainKata
{
    public class ReservationResponseDto
    {
        public string TrainId { get; }
        public string BookingId { get; }
        public List<Seat> Seats { get; }

        public ReservationResponseDto(string trainId, string bookingId, List<Seat> seats)
        {
            TrainId = trainId;
            BookingId = bookingId;
            Seats = seats;
        }

        public override string ToString()
        {
            return "{" +
                   "\"train_id\": \"" + TrainId + "\", " +
                   "\"booking_reference\": \"" + BookingId + "\", " +
                   "\"seats\": [" +
                   string.Join(", ", Seats.Select(s => "\"" + s.SeatNumber + s.Coach + "\"")) + "]" +
                   "}";
        }
    }
}