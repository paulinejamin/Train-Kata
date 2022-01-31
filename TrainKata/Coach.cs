using System.Collections.Generic;

namespace TrainKata
{
    public class Coach
    {
        public Coach(string name)
        {
            Name = name;
            Seats = new List<Seat>();
        }

        public string Name { get; }
        public List<Seat> Seats { get; }

        public void AddSeat(int seatNumber, string bookingReference)
        {
            Seats.Add(new Seat(Name, seatNumber, bookingReference));
        }

        public bool HasEnoughAvailableSeats(int numberOfSeatToBook)
        {
            long count = 0;
            foreach (Seat seat in Seats)
            {
                if (seat.IsAvailable)
                {
                    count++;
                }

                if (count >= numberOfSeatToBook)
                {
                    return true;
                }
            }

            return false;
        }
    }
}