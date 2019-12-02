using Newtonsoft.Json;
using System.Collections.Generic;
using TrainKata.Domain;

namespace TrainKata.Infra
{
    public class GetTopologieTrain : IGetTopologieTrain
    {
        private readonly ITrainDataClient _trainDataClient;

        public GetTopologieTrain(ITrainDataClient trainDataClient)
        {
            _trainDataClient = trainDataClient;
        }

        public List<Siege> GetTopologie(IdentifiantTrain trainId)
        {
            List<Siege> seats = new List<Siege>();
            var trainTopology = _trainDataClient.GetTopology(trainId.IdTrain);

            var topologySeats = JsonConvert.DeserializeObject<Topologie>(trainTopology).seats.Values;

            foreach (Topologie.TopologieSeat seat in topologySeats)
            {
                seats.Add(new Siege(new IdentifiantWagon(seat.coach), seat.seat_number, EstSiegeDisponible(seat)));
            }
            return seats;
        }

        private static bool EstSiegeDisponible(Topologie.TopologieSeat y)
        {
            return string.IsNullOrEmpty(y.booking_reference);
        }
    }
}
