using System.Collections.Generic;

namespace TrainKata.Domain
{
    public class Reservation
    {
        public Reservation(ReferenceReservation referenceReservation, List<Siege> sieges)
        {
            ReferenceReservation = referenceReservation;
            Sieges = sieges;
        }

        public ReferenceReservation ReferenceReservation { get; }
        public List<Siege> Sieges { get; }

    }
}