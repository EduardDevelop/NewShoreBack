using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Newshore_BackEnd.Models;

namespace Newshore_BackEnd.DataAcces
{
    public class FlightDA
    {
        public List<VueloApi>  GET()
        {
           using (var client = new HttpClient())
            {
                string url = "https://recruiting-api.newshore.es/api/flights/2";
                client.DefaultRequestHeaders.Clear();
                 var response = client.GetAsync(url).Result;
                var res = response.Content.ReadAsStringAsync().Result;
                JArray r = JArray.Parse(res);
                   return r.Select(x=>new VueloApi
                    {
                    departureStation= (string)x["departureStation"],
                        arrivalStation = (string)x["arrivalStation"],
                        flightCarrier= (string)x["flightCarrier"],
                        flightNumber= (string)x["flightNumber"],
                        price = (int)x["price"]

                    }).ToList();

                
            }

        }

    }
}
