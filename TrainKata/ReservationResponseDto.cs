using System.Collections.Generic;

namespace TrainKata
{
    public class ReservationResponseDto
    {
        public string TrainId { get; set; }
        public string BookingId { get; set; }
        public List<Seat> Seats { get; set; }

        public ReservationResponseDto(string trainId, string bookingId, List<Seat> seats)
        {
            TrainId = trainId;
            BookingId = bookingId;
            Seats = seats;
        }

        public override string ToString()
        {
            return "ReservationResponseDto{" +
                "trainId='" + TrainId + '\'' +
                ", bookingId='" + BookingId + '\'' +
                ", seats=" + Seats +
                '}';
        }
    }
}
