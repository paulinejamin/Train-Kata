namespace TrainKata
{
    public interface ITrainDataClient
    {
        //get the topology of the train
        string GetTopology(string trainId);
    }
}
