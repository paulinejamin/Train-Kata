using System.Collections.Generic;
using System.Linq;

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

        public List<Seat> GetAvailableSeats(int numberOfSeat)
        {
            return Seats
                .FindAll(seat => seat.IsAvailable)
                .Take(numberOfSeat)
                .ToList();
        }
    }
}