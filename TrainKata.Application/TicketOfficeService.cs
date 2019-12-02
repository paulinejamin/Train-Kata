using System.Linq;
using TrainKata.Domain;
using TrainKata.Infra;

namespace TrainKata.Application
{
    public class TicketOfficeService
    {
        private readonly ITrainDataClient _trainDataClient;
        private readonly IBookingReferenceClient _bookingReferenceClient;

        public TicketOfficeService(ITrainDataClient trainDataClient, IBookingReferenceClient bookingReferenceClient)
        {
            _trainDataClient = trainDataClient;
            _bookingReferenceClient = bookingReferenceClient;
        }

        public string MakeReservation(ReservationRequestDto demandeDeReservation)
        {
            var reserver = new Reserver(new GetTopologieTrain(_trainDataClient), new GetReferenceReservation(_bookingReferenceClient));

            var reservation = reserver.FaireReservation(new DemandeReservation(new IdentifiantTrain(demandeDeReservation.TrainId),
                demandeDeReservation.SeatCount));

            if (reservation.Sieges.Any())
            {
                return "{" +
                        "\"train_id\": \"" + demandeDeReservation.TrainId + "\", " +
                        "\"booking_reference\": \"" + reservation.ReferenceReservation.Reference + "\", " +
                        "\"seats\": [" + string.Join(", ", reservation.Sieges.Select(s => "\"" + s.NumeroSiege + s.NumeroWagon.idWagon + "\"")) + "]" +
                        "}";
            }
            else
            {
                return "{\"train_id\": \"" + demandeDeReservation.TrainId + "\", \"booking_reference\": \"\", \"seats\": []}";
            }
        }

    }
}
