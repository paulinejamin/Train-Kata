using System.Collections.Generic;

namespace TrainKata.Domain
{
    public interface IGetTopologieTrain
    {
        List<Siege> GetTopologie(IdentifiantTrain trainId);
    }
}