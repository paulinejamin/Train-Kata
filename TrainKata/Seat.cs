namespace TrainKata
{
    public class Seat
    {
        public string Coach { get; private set; }
        public int SeatNumber { get; private set; }

        public Seat(string coach, int seatNumber)
        {
            Coach = coach;
            SeatNumber = seatNumber;
        }
    }
}
