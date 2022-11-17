using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System.Collections.Generic;
using System.Linq;

namespace TestAppTest
{
    // Don't worry to evaluate my answer. Just tried for fun.
    [TestClass]
    public class GeoDBRestAPITest
    {
        const string ApiKey = "ae95c767ecmsh63eab66c80a09e5p11d5bajsn43bb0307d53b";
        const string ApiHost = "wft-geo-db.p.rapidapi.com";
        const string EndPoint = "https://wft-geo-db.p.rapidapi.com/v1/geo/countries";

        [TestMethod]
        public void Get_Countries_CurCode_LKR()
        {
            var client = new RestClient();
            var options = new Dictionary<string, string> { { "currencyCode", "LKR" } };
            var request = new RestRequest($"{EndPoint}?{string.Join('&', options.Select(kvp => $"{kvp.Key}={kvp.Value}"))}", Method.Get);
            request.AddHeader("X-RapidAPI-Key", ApiKey);
            request.AddHeader("X-RapidAPI-Host", ApiHost);
            RestResponse response = client.Execute(request);
            JsonNetSerializer serializer = new JsonNetSerializer();
            var result = serializer.Deserialize<GeoDBGetCountriesResponse>(response);
            Assert.IsNotNull(result, "Deserialized output cannot be null");
            Assert.IsNotNull(result.Data, "'data' array in the response cannot be null");
            Assert.IsTrue(result.Data.Any(p => p.Name == "Sri Lanka"), "Sri Lanka is not present in the response");
        }

        public class GeoDBGetCountriesResponse
        {
            public CountryData[]? Data { get; set; }

            public class CountryData
            {
                public string? Name { get; set; }
            }
        }
    }
}
