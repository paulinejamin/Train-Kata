using Newtonsoft.Json;

namespace TrainKata.Infra
{
    public class TrainProviderFileAdapter : IProvideTrains
    {
        private readonly ITrainDataClient _trainDataClient;

        public TrainProviderFileAdapter(ITrainDataClient trainDataClient)
        {
            _trainDataClient = trainDataClient;
        }

        public Train GetTrain(string trainId)
        {
            var trainRawData = _trainDataClient.GetTopology(trainId);
            var train = new Train();
            
            foreach (var seatRawData in JsonConvert.DeserializeObject<Topology>(trainRawData).seats.Values)
            {
                var coachName = seatRawData.coach;
                var coach = train.GetCoach(coachName);
                if (coach == null)
                {
                    coach = new Coach(coachName);
                    train.AddCoach(coach);
                }
                coach.AddSeat(seatRawData.seat_number, seatRawData.booking_reference);
            }

            return train;
        }
    }
}