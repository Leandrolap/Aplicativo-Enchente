using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using static SaveItUPX.Model.Previsao;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SaveItUPX.Service
{
    public class WeatherAPI
    {
        public static async Task<Tempo> GetPrevisaoDoTempo(string cidade)
        {
            string appId = "sua Id API";
            string queryString = "https://api.openweathermap.org/data/2.5/weather?q=" + cidade + "&units=metric" + "&appid=" + appId + "&lang=pt";
            dynamic resultado = await WeatherAPI.getDataFromService(queryString).ConfigureAwait(false);
            if (resultado["weather"] != null)
            {
                Tempo previsao = new Tempo();
                //Weather weather = new Weather();
                previsao.Title = (string)resultado["name"];
                previsao.Temperature = (double)resultado["main"]["temp"];
                previsao.Wind = (string)resultado["wind"]["speed"] + " mph";
                previsao.Humidity = (string)resultado["main"]["humidity"] + " %";
                previsao.Description = (string)resultado["weather"][0]["description"];
                previsao.Icon = (string)resultado["weather"][0]["icon"];

                DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                DateTime sunrise = time.AddSeconds((double)resultado["sys"]["sunrise"]);
                DateTime sunset = time.AddSeconds((double)resultado["sys"]["sunset"]);

                return previsao;
            }
            else
            {
                return null;
            }
        }

        public static async Task<dynamic> getDataFromService(string queryString)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(queryString);
            dynamic data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject(json);
            }
            return data;
        }
        public static async Task<dynamic> getDataFromServiceByCity(string city)
        {
            string appId = "numero_da_sua_API_KEY";
            string url = string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&units=metric&cnt=1&APPID={1}", city.Trim(), appId);
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            dynamic data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject(json);
            }
            return data;
        }
    }
}
