using System;

namespace Taustajarjestelmat
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(args[0]);
        }
    }
    public interface ICityBikeDataFetcher
    {
    Task<int> GetBikeCountInStation(string stationName);
    }
    class RealTimeCityBikeDataFetcher : ICityBikeDataFetcher
    {

    }
}
