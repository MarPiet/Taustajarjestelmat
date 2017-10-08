using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Taustajarjestelmat
{
    class Program
    {       
        static void Main(string[] args)
        {                
            ICityBikeDataFetcher asd;  
                    
            if(args[1] == "offline")
            {
            asd = new OfflineCityBikeDataFetcher();
            Task<int> t = Task.Run(() => asd.GetBikeCountInStation(args[0]));       
            t.Wait();
            Console.WriteLine(t.Result);
            }

            else if(args[1] == "realtime")
            {
                try{
                asd = new RealTimeCityBikeDataFetcher();       
                Task<int> t = Task.Run(() => asd.GetBikeCountInStation(args[0]));  
                t.Wait();
                Console.WriteLine(t.Result);
                }     
                catch(ArgumentException e){
                Console.WriteLine(e);
                }
                catch(NotFoundException e){
                Console.WriteLine(e);
                }
            }
     
        }
    }
    public interface ICityBikeDataFetcher
    {
        Task<int> GetBikeCountInStation(string stationName);
    }
    class OfflineCityBikeDataFetcher : ICityBikeDataFetcher
    {
        public async Task<int> GetBikeCountInStation(string stationName)
        {
            string line;
            string a = string.Empty;
            StreamReader file = new StreamReader("bikes.txt");
            while((line = await  file.ReadLineAsync()) != null)  
            {  
                if(line.Contains(stationName)){
                   
                    for(int i = 0; i < line.Length; i++){
                        if(Char.IsDigit(line[i])) a+= line[i];
                    }
                  
                }   
            }
            return int.Parse(a);
        }
    }

    class RealTimeCityBikeDataFetcher : ICityBikeDataFetcher
    {
        public async Task<int> GetBikeCountInStation(string stationName)
        {       
               if(stationName.ToCharArray().Any(char.IsDigit)){
                throw new ArgumentException("Invalid argument");
            }
           

            var httpClient = new HttpClient();
            byte[] bytes = await (await httpClient.GetAsync("http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental")).Content.ReadAsByteArrayAsync();
            var str = System.Text.Encoding.UTF8.GetString(bytes);           
            RootObject olio = JsonConvert.DeserializeObject<RootObject>(str);
   

            foreach(var item in olio.stations){
                if(stationName  == item.name){
                    return item.bikesAvailable;
                }
            }
            throw new NotFoundException("Not Found: ");
                
        }

    }
    public class NotFoundException : Exception{
        public NotFoundException() {}
        public NotFoundException(string message) : base(message){}
    }


   public class Station
{
    public string id { get; set; }
    public string name { get; set; }
    public double x { get; set; }
    public double y { get; set; }
    public int bikesAvailable { get; set; }
    public int spacesAvailable { get; set; }
    public bool allowDropoff { get; set; }
    public List<string> networks { get; set; }
    public bool realTimeData { get; set; }
}

public class RootObject
{
    public List<Station> stations { get; set; }
}


}
