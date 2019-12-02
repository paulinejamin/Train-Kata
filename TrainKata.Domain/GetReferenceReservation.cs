using TrainKata.Domain;

namespace TrainKata.Infra
{
    public class GetReferenceReservation: IGetReferenceReservation
    {
        private IBookingReferenceClient _bookingReferenceClient;

        public GetReferenceReservation(IBookingReferenceClient bookingReferenceClient)
        {
            _bookingReferenceClient = bookingReferenceClient;
        }

        public ReferenceReservation GenererReferenceReservation()
        {
            return new ReferenceReservation(_bookingReferenceClient.GenerateBookingReference());
        }
    }
}