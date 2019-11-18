namespace TrainKata
{
    public class ReservationRequestDto
    {
        public string TrainId { get; private set; }
        public int SeatCount { get; private set; }

        public ReservationRequestDto(string trainId, int seatCount)
        {
            TrainId = trainId;
            SeatCount = seatCount;
        }
    }
}