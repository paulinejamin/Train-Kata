namespace TrainKata.Domain
{
    public class ReferenceReservation  
    {
        public ReferenceReservation(string reference)
        {
            Reference = reference;
        }

        public string Reference { get; }
    }
}