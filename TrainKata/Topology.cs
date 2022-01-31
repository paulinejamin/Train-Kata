using System.Collections.Generic;

namespace TrainKata
{
    public class Topology
    {
        public Dictionary<string, TopologySeat> seats;

        public class TopologySeat
        {
            public string booking_reference;
            public int seat_number;
            public string coach;
        }
    }
}
