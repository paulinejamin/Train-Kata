using NUnit.Framework;
using System.Collections.Generic;
using TrainKata.Infra;

namespace TrainKata.Tests
{
    public class TicketOfficeServiceTest
    {
        private const string TrainId = "9043-2018-05-24";
        private const string BookingReference = "75bcd15";

        [Test]
        public void ReserveSeatsWhenTrainIsEmpty()
        {
            const int seatsRequestedCount = 3;
            TicketOfficeService service = BuildTicketOfficeService(TrainTopologies.With_10_available_seats());

            var reservation = service.MakeReservation(new ReservationRequestDto(TrainId, seatsRequestedCount));

            Assert.AreEqual("{\"train_id\": \"" + TrainId + "\", \"booking_reference\": \"" + BookingReference + "\", \"seats\": [\"1A\", \"2A\", \"3A\"]}", reservation);
        }

        [Test]
        public void NotReserveSeatsWhenNotEnoughFreePlace()
        {
            const int seatsRequestedCount = 5;
            TicketOfficeService service = BuildTicketOfficeService(TrainTopologies.With_10_seats_and_6_already_reserved());

            var reservation = service.MakeReservation(new ReservationRequestDto(TrainId, seatsRequestedCount));

            Assert.AreEqual("{\"train_id\": \"" + TrainId + "\", \"booking_reference\": \"\", \"seats\": []}", reservation);
        }

        [Test]
        public void ReserveSeatsWhenOneCoachIsFullAndOneIsEmpty()
        {
            int seatsRequestedCount = 3;
            TicketOfficeService service = BuildTicketOfficeService(TrainTopologies.With_2_coaches_and_the_first_coach_is_full());

            var reservation = service.MakeReservation(new ReservationRequestDto(TrainId, seatsRequestedCount));

            Assert.AreEqual("{\"train_id\": \"" + TrainId + "\", \"booking_reference\": \"" + BookingReference + "\", \"seats\": [\"1B\", \"2B\", \"3B\"]}", reservation);
        }

        [Test]
        public void ReserveAllSeatsInTheSameCoach()
        {
            int seatsRequestedCount = 2;
            TicketOfficeService service = BuildTicketOfficeService(TrainTopologies.With_2_coaches_and_9_seats_already_reserved_in_the_first_coach());

            var reservation = service.MakeReservation(new ReservationRequestDto(TrainId, seatsRequestedCount));

            Assert.AreEqual("{\"train_id\": \"" + TrainId + "\", \"booking_reference\": \"" + BookingReference + "\", \"seats\": [\"1B\", \"2B\"]}", reservation);
        }

        [Test]
        public void CannotReserveWhenTrainIsNotFullButNotCoachIsAvailable()
        {
            int seatsRequestedCount = 2;
            TicketOfficeService service = BuildTicketOfficeService(TrainTopologies.With_10_coaches_half_available());

            var reservation = service.MakeReservation(new ReservationRequestDto(TrainId, seatsRequestedCount));

            Assert.AreEqual("{\"train_id\": \"" + TrainId + "\", \"booking_reference\": \"\", \"seats\": []}", reservation);
        }

        [Test]
        public void NotReserveSeatsWhenItExceedMaxCapacityThreshold()
        {
            int seatsRequestedCount = 3;
            TicketOfficeService service = BuildTicketOfficeService(TrainTopologies.With_10_seats_and_6_already_reserved());

            var reservation = service.MakeReservation(new ReservationRequestDto(TrainId, seatsRequestedCount));

            Assert.AreEqual("{\"train_id\": \"" + TrainId + "\", \"booking_reference\": \"\", \"seats\": []}", reservation);
        }

        private static TicketOfficeService BuildTicketOfficeService(string topologies)
        {
            return new TicketOfficeService( new BookingReferenceClientStub(BookingReference), new TrainProviderFileAdapter(new TrainDataClientStub(topologies)));
        }

    }

    internal class BookingReferenceClientStub : IBookingReferenceClient
    {
        private readonly string bookingReference;

        public BookingReferenceClientStub(string bookingReference)
        {
            this.bookingReference = bookingReference;
        }

        public string GenerateBookingReference()
        {
            return bookingReference;
        }

        public void BookTrain(string trainId, string bookingReference, List<Seat> seats)
        {
        }
    }

    internal class TrainDataClientStub : ITrainDataClient
    {
        private readonly string topologies;

        public TrainDataClientStub(string topologies)
        {
            this.topologies = topologies;
        }

        public string GetTopology(string trainId)
        {
            return topologies;
        }
    }

}

