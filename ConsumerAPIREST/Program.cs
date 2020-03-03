using Newtonsoft.Json;
using System;
using System.Net;
using System.Collections.Generic;


namespace ConsumerAPIREST
{
    public class ProductRequest
    {
        public string Category { get; set; }
    }

    class Program
    {

        static void Main(string[] args)
        {
            // Create a ProductRequest for sending our request to our WebAPI
            ProductRequest productRequest = new ProductRequest();
            productRequest.Category = "test";

            object products = postWebApi(productRequest, new Uri("http://192.168.11.9:1895/getListBlocks"));

            object products2 = getWebApi("", new Uri("http://192.168.11.9:1895/getEquipments"));
        }

        public static object postWebApi(object data, Uri webApiUrl)
        {
            // Create a WebClient to POST the request
            WebClient client = new WebClient();

            // Set the header so it knows we are sending JSON
            client.Headers[HttpRequestHeader.ContentType] = "application/json";

            // Serialise the data we are sending in to JSON
            string serialisedData = JsonConvert.SerializeObject(data);

            // Make the request
            var response = client.UploadString(webApiUrl, serialisedData);

            // Deserialise the response into a GUID
            return JsonConvert.DeserializeObject(response);
        }

        public static object getWebApi(string queryString, Uri webApiUrl)
        {
            // Create a WebClient to GET the request
            WebClient client = new WebClient();

            // Set the header so it knows we are sending JSON
            client.Headers[HttpRequestHeader.ContentType] = "application/json";

            //string queryString = "";

            //if (data != null)
            //{
            //    // Separate the KeyValuePairs in to a query string
            //    foreach (var pair in data)
            //    {
            //        if (queryString.Length != 0)
            //        {
            //            queryString += "&";
            //        }

            //        queryString += pair.Key + "=" + pair.Value;
            //    }
            //}

            // Make the request
            var response = client.DownloadString(webApiUrl + "?" + queryString);

            // Deserialise the response into a GUID
            return JsonConvert.DeserializeObject(response);
        }


    }
}
