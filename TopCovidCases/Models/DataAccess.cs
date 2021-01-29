using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TopCovidCases.Models
{
    public class DataAccess : IDataAccess
    {
        protected const String url = "https://covid-19-statistics.p.rapidapi.com/";
        protected const String xRapidapiKey = "d25e6c454cmsh5ca44e85b41c2c9p13e731jsn2e5a92190bf5";
        protected const String xRapidapiHost = "covid-19-statistics.p.rapidapi.com";



        public async Task<IList<Region>> ListRegion() {
            IList<Region> regions = null;
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(url + "regions"),
                    Headers =
                        {
                            { "x-rapidapi-key", xRapidapiKey },
                            { "x-rapidapi-host", xRapidapiHost },
                        },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    JObject jObject = JObject.Parse(body);
                    regions = ((JArray)jObject["data"]).ToObject<IList<Region>>().OrderBy(r => r.name).ToList();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return regions;
        }




        public async Task<IList<Cases>> ListCases(String iso)
        {
            IList<Cases> cases = null;
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(url + "reports" + (iso != null ? "?iso=" + iso : "")),
                    Headers =
                    {
                        { "x-rapidapi-key", xRapidapiKey },
                        { "x-rapidapi-host", xRapidapiHost },
                    },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    JObject jObject = JObject.Parse(body);
                    cases = ((JArray)jObject["data"]).ToObject<IList<Cases>>().OrderByDescending(c => c.confirmed).Take(10).ToList();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return cases;
        }


    }
}
