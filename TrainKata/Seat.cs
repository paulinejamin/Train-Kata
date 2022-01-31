namespace TrainKata
{
    public class Seat
    {
        public string Coach { get; set; }
        public int SeatNumber { get; set; }

        public Seat(string coach, int seatNumber)
        {
            Coach = coach;
            SeatNumber = seatNumber;
        }
    }
}
