using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainKata.Application;
using TrainKata.Infra;

namespace TrainKata.Tests
{
    [TestClass]
    public class TicketOfficeServiceTest
    {
        private const string TrainId = "9043-2018-05-24";
        private const string BookingReference = "75bcd15";

        [TestMethod]
        public void ReserveSeatsWhenTrainIsEmpty()
        {
            int seatsRequestedCount = 3;
            TicketOfficeService service = BuildTicketOfficeService(TrainTopologies.With_10_available_seats());

            var reservation = service.MakeReservation(new ReservationRequestDto(TrainId, seatsRequestedCount));

            Assert.AreEqual("{\"train_id\": \"" + TrainId + "\", \"booking_reference\": \"" + BookingReference + "\", \"seats\": [\"1A\", \"2A\", \"3A\"]}", reservation);
        }

        [TestMethod]
        public void NotReserveSeatsWhenNotEnoughFreePlace()
        {
            int seatsRequestedCount = 5;
            TicketOfficeService service = BuildTicketOfficeService(TrainTopologies.With_10_seats_and_6_already_reserved());

            var reservation = service.MakeReservation(new ReservationRequestDto(TrainId, seatsRequestedCount));

            Assert.AreEqual("{\"train_id\": \"" + TrainId + "\", \"booking_reference\": \"\", \"seats\": []}", reservation);
        }

        [TestMethod]
        public void ReserveSeatsWhenOneCoachIsFullAndOneIsEmpty()
        {
            int seatsRequestedCount = 3;
            TicketOfficeService service = BuildTicketOfficeService(TrainTopologies.With_2_coaches_and_the_first_coach_is_full());

            var reservation = service.MakeReservation(new ReservationRequestDto(TrainId, seatsRequestedCount));

            Assert.AreEqual("{\"train_id\": \"" + TrainId + "\", \"booking_reference\": \"" + BookingReference + "\", \"seats\": [\"1B\", \"2B\", \"3B\"]}", reservation);
        }

        [TestMethod]
        public void ReserveAllSeatsInTheSameCoach()
        {
            int seatsRequestedCount = 2;
            TicketOfficeService service = BuildTicketOfficeService(TrainTopologies.With_2_coaches_and_9_seats_already_reserved_in_the_first_coach());

            var reservation = service.MakeReservation(new ReservationRequestDto(TrainId, seatsRequestedCount));

            Assert.AreEqual("{\"train_id\": \"" + TrainId + "\", \"booking_reference\": \"" + BookingReference + "\", \"seats\": [\"1B\", \"2B\"]}", reservation);
        }

        [TestMethod]
        public void CannotReserveWhenTrainIsNotFullButNotCoachIsAvailable()
        {
            int seatsRequestedCount = 2;
            TicketOfficeService service = BuildTicketOfficeService(TrainTopologies.With_10_coaches_half_available());

            var reservation = service.MakeReservation(new ReservationRequestDto(TrainId, seatsRequestedCount));

            Assert.AreEqual("{\"train_id\": \"" + TrainId + "\", \"booking_reference\": \"\", \"seats\": []}", reservation);
        }

        [TestMethod, Ignore]
        public void NotReserveSeatsWhenItExceedMaxCapacityThreshold()
        {
            int seatsRequestedCount = 3;
            TicketOfficeService service = BuildTicketOfficeService(TrainTopologies.With_10_seats_and_6_already_reserved());

            var reservation = service.MakeReservation(new ReservationRequestDto(TrainId, seatsRequestedCount));

            Assert.AreEqual("{\"train_id\": \"" + TrainId + "\", \"booking_reference\": \"\", \"seats\": []}", reservation);
        }

        private static TicketOfficeService BuildTicketOfficeService(string topologies)
        {
            return new TicketOfficeService(new TrainDataClientStub(topologies), new BookingReferenceClientStub(BookingReference));
        }

    }

    internal class BookingReferenceClientStub : IBookingReferenceClient
    {
        private readonly string _bookingReference;

        public BookingReferenceClientStub(string bookingReference)
        {
            this._bookingReference = bookingReference;
        }

        public string GenerateBookingReference()
        {
            return _bookingReference;
        }

    }

    internal class TrainDataClientStub : ITrainDataClient
    {
        private readonly string _topologies;

        public TrainDataClientStub(string topologies)
        {
            this._topologies = topologies;
        }

        public string GetTopology(string trainId)
        {
            return _topologies;
        }
    }

}

