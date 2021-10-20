using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;

namespace WebApi.DataHelpers
{
    public class RealtorDataAccess
    {
        public RealtorDataAccess(string zipCode)
        {
            ZipCode = zipCode;
            Offset = 0;
            client = new HttpClient();
        }

        public RealtorDataAccess(string zipCode, int offset)
        {
            ZipCode = zipCode;
            Offset = offset;
            client = new HttpClient();
        }

        public string ZipCode { get; set; }
        public int Offset { get; set; }
        public HttpClient client { get; private set; }

        async public Task<string> GetRentData()
        {
            
           var testUri = QueryHelpers.AddQueryString("https://realty-in-us.p.rapidapi.com/properties/list-for-rent", new Dictionary<string,string>()
            {
                {"state_code",""},
                {"limit","200"},
                {"city",""},
                {"offset", Offset.ToString()},
                {"sqft_min","400"},
                {"postal_code",ZipCode},
                {"sort","relevance"}

            });
            //Old Endpoint... Do Not Use For Production
            /* var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://realtor.p.rapidapi.com/properties/list-for-rent?state_code=GA&limit=200&city=Atlanta&offset=" + Offset + "&sqft_min=400&postal_code=" + ZipCode + "&sort=relevance"),
                Headers =
                {
                    { "x-rapidapi-key", "1444b95a90msheb89124c8a7ae14p135fafjsna4f5d0eff8c0" },
                    { "x-rapidapi-host", "realtor.p.rapidapi.com" },
                },
            }; */
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://realty-in-us.p.rapidapi.com/properties/list-for-rent?state_code=%22%22&city=%22%22&limit=200&offset=" + Offset + "&postal_code="+ ZipCode +"&sort=relevance&sqft_min=400"),
                Headers =
                {
                    { "x-rapidapi-key", "1444b95a90msheb89124c8a7ae14p135fafjsna4f5d0eff8c0" },
                    { "x-rapidapi-host", "realty-in-us.p.rapidapi.com" },
                },
            };
            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    return body;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return "data retrieval failed";
            }
        }
    }
}