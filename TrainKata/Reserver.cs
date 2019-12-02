using System.Collections.Generic;
using System.Linq;

namespace TrainKata.Domain
{
    public class Reserver
    {
        private IGetTopologieTrain _getTopologieTrain;
        private IGetReferenceReservation _getReferenceReservation;

        public Reserver(IGetTopologieTrain getTopologieTrain, IGetReferenceReservation getReferenceReservation)
        {
            _getTopologieTrain = getTopologieTrain;
            _getReferenceReservation = getReferenceReservation;
        }
        public Reservation FaireReservation(DemandeReservation demandeReservation)
        {
            var listeDeSieges = _getTopologieTrain.GetTopologie(demandeReservation.IdentifiantTrain);
            var wagonDisponible = TrouverWagonDisponible(demandeReservation, listeDeSieges);

            List<Siege> siegesReserves = new List<Siege>();
            if (WagonDisponible(wagonDisponible))
            {
                siegesReserves = wagonDisponible.Sieges.Where(siege => siege.Disponible)
                    .Take(demandeReservation.NombreSiege)
                    .ToList();
            }

            var referenceReservation = _getReferenceReservation.GenererReferenceReservation();

            return new Reservation(referenceReservation, siegesReserves);
        }

        private static Wagon TrouverWagonDisponible(DemandeReservation demandeReservation, List<Siege> listeDeSieges)
        {
            var train = new List<Wagon>();

            foreach (Siege siege in listeDeSieges)
            {
                var wagon = train.FirstOrDefault(w => w.Identifiant.idWagon == siege.NumeroWagon.idWagon);

                if (wagon == null)
                {
                    wagon = new Wagon(siege.NumeroWagon);
                    train.Add(wagon);
                }

                wagon.AjouterSiege(siege);
            }

            var wagonDisponible = train.FirstOrDefault(wagon =>
                wagon.ContientAssezDeSiegesDisponibles(demandeReservation.NombreSiege));

            return wagonDisponible;
        }

        private static bool WagonDisponible(Wagon wagon)
        {
            return wagon != null;
        }
    }
}
