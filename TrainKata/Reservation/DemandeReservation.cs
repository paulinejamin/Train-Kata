namespace TrainKata.Domain
{
    public class DemandeReservation
    {
        public IdentifiantTrain IdentifiantTrain { get; }

        public int NombreSiege { get; }

        public DemandeReservation(IdentifiantTrain identifiantTrain , int nombreSiege)
        {
            IdentifiantTrain = identifiantTrain;
            NombreSiege = nombreSiege;
        }
    }
}
