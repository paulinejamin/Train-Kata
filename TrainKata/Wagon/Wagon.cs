using System.Collections.Generic;
using System.Linq;

namespace TrainKata.Domain
{
    public class Wagon
    {
        public IdentifiantWagon Identifiant { get; }
        public List<Siege> Sieges { get; }

        public Wagon(IdentifiantWagon identifiant)
        {
            Identifiant = identifiant;
            Sieges = new List<Siege>();
        }

        public void AjouterSiege(Siege siege)
        {
            Sieges.Add(siege);
        }

        public bool ContientAssezDeSiegesDisponibles(int nombreDeSiege)
        {
            return Sieges.Count(siege => siege.Disponible) >= nombreDeSiege;
        }
    }


}