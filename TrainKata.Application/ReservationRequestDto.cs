namespace TrainKata.Application
{
    public class ReservationRequestDto
    {
        public string TrainId { get; }
        public int SeatCount { get; }

        public ReservationRequestDto(string trainId, int seatCount)
        {
            TrainId = trainId;
            SeatCount = seatCount;
        }
    }
}