namespace TrainKata
{
    public class Seat
    {
        public string? BookingReference { get; set; }
        public string Coach { get; set; }
        public int SeatNumber { get; set; }

        public Seat(string coach, int seatNumber, string bookingReference)
        {
            Coach = coach;
            SeatNumber = seatNumber;
            BookingReference = bookingReference;
        }

        public bool IsAvailable
        {
            get { return string.Empty.Equals(BookingReference); }
        }
    }
}
