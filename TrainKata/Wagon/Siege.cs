namespace TrainKata.Domain
{
    public class Siege
    {
        public IdentifiantWagon NumeroWagon { get; }
        public int NumeroSiege { get; }
        public bool Disponible { get; }

        public Siege(IdentifiantWagon numeroWagon, int numeroSiege, bool disponible)
        {
            NumeroWagon = numeroWagon;
            NumeroSiege = numeroSiege;
            Disponible = disponible;
        }
    }
}
