namespace TrainKata
{
    public class ReservationRequestDto
    {
        public string TrainId { get; set; }
        public int SeatCount { get; set; }

        public ReservationRequestDto(string trainId, int seatCount)
        {
            TrainId = trainId;
            SeatCount = seatCount;
        }
    }
}