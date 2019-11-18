using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace TrainKata
{
    public class TicketOfficeService
    {
        private readonly ITrainDataClient trainDataClient;
        private readonly IBookingReferenceClient bookingReferenceClient;

        public TicketOfficeService(ITrainDataClient trainDataClient, IBookingReferenceClient bookingReferenceClient)
        {
            this.trainDataClient = trainDataClient;
            this.bookingReferenceClient = bookingReferenceClient;
        }

        public string MakeReservation(ReservationRequestDto request)
        {
            var data = trainDataClient.GetTopology(request.TrainId);
            var dic = new Dictionary<string, List<Topologie.TopologieSeat>>();
            foreach(Topologie.TopologieSeat x in JsonConvert.DeserializeObject<Topologie>(data).seats.Values)
            {
                if (!dic.TryGetValue(x.coach, out List<Topologie.TopologieSeat> list))
                    dic.Add(x.coach, list = new List<Topologie.TopologieSeat>());
                list.Add(x);
            }
            KeyValuePair<string, List<Topologie.TopologieSeat>> found = new KeyValuePair<string, List<Topologie.TopologieSeat>>();
            foreach(KeyValuePair<string, List<Topologie.TopologieSeat>> kvp in dic)
            {
                long count = 0;
                foreach(Topologie.TopologieSeat y in kvp.Value)
                {
                    if (string.Empty.Equals(y.booking_reference))
                    {
                        count++;
                    }
                    if(count >= request.SeatCount)
                    {
                        found = kvp;
                        break;
                    }
                }
            }
            List<Seat> seats = new List<Seat>();
            if (!found.Equals(new KeyValuePair<string, List<Topologie.TopologieSeat>>()))
            {
                var list = new List<Seat>();
                long limit = request.SeatCount;
                foreach (Topologie.TopologieSeat y in found.Value)
                {
                    if ("".Equals(y.booking_reference))
                    {
                        if (limit-- == 0) break;
                        Seat seat = new Seat(y.coach, y.seat_number);
                        list.Add(seat);
                    }
                }
                seats = list;
            }

            if (seats.Any())
            {
                ReservationResponseDto reservation = new ReservationResponseDto(request.TrainId, bookingReferenceClient.GenerateBookingReference(), seats);
                return "{" +
                        "\"train_id\": \"" + reservation.TrainId + "\", " +
                        "\"booking_reference\": \"" + reservation.BookingId + "\", " +
                        "\"seats\": [" + string.Join(", ", reservation.Seats.Select(s => "\"" + s.SeatNumber + s.Coach + "\"")) + "]" +
                        "}";
            }
            else
            {
                return "{\"train_id\": \"" + request.TrainId + "\", \"booking_reference\": \"\", \"seats\": []}";
            }
        }

    }
}
