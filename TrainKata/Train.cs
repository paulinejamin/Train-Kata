using System.Collections.Generic;
using System.Linq;

namespace TrainKata
{
    public class Train
    {
        public string TrainId { get; }
        public List<Coach> Coaches { get; } = new();

        public void AddCoach(Coach coach)
        {
            Coaches.Add(coach);
        }

        public Coach GetCoach(string coachName)
        {
            return Coaches.Find(c => c.Name.Equals(coachName));
        }

        public Coach FindCoachWithEnoughAvailableSeats(int numberOfSeatToBook)
        {
            return Coaches.FirstOrDefault(coach => coach.HasEnoughAvailableSeats(numberOfSeatToBook));
        }
    }
}