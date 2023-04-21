using Newshore_BackEnd.DataAcces;
using Newshore_BackEnd.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace Newshore_BackEnd.Bussines
{
    public class FlightBussines
    {

        public string FindAllRoutes(string origin, string destination, List<VueloApi> flights)
        {
            // Construye un diccionario con las conexiones entre estaciones
            Dictionary<string, Dictionary<string, VueloApi>> connections = new Dictionary<string, Dictionary<string, VueloApi>>();
            foreach (var flight in flights)
            {
                string departure = flight.departureStation;
                string arrival =flight.arrivalStation;
                double price = flight.price;
                if (!connections.ContainsKey(departure))
                {
                    connections[departure] = new Dictionary<string, VueloApi>();
                }
                connections[departure][arrival] = flight;
            }

            // Implementa el algoritmo de Dijkstra
            var heap = new List<(double, string, List<string>)>();
            var flightsRes = new List<FlightModel>();
            heap.Add((0, origin, new List<string>()));
            var flightsRes2 = new List<FlightModel>();
            var routes = new List<Dictionary<string, object>>();
            while (heap.Any())
            {
                (double cost, string node, List<string> path) = heap.OrderBy(item => item.Item1).First();
                heap.Remove((cost, node, path));

                if (node == destination)
                {
                    // Si hemos llegado al destino, añadimos la ruta a la lista de rutas
                 
                    path.Add(node);
                    routes.Add(new Dictionary<string, object> { { "route", path }, { "cost", cost } });


                    //[{"route":["MZL","MDE"],"cost":200.0}]
                }
                else
                {
                    foreach (var connection in connections.GetValueOrDefault(node, new Dictionary<string, VueloApi>()))
                    {
                        string neighbor = connection.Key;
                        double neighbor_cost = connection.Value.price;
                        VueloApi localFlight = connection.Value;
                        if (!path.Contains(neighbor))
                        {
                            heap.Add((cost + neighbor_cost, neighbor, path.Concat(new List<string> { node }).ToList()));
                            flightsRes.Add(new FlightModel
                            {
                                Origin=localFlight.departureStation,
                                Destination=localFlight.arrivalStation,
                                Price= localFlight.price,
                                transport= new Transport
                                {
                                    FlightCarrier=localFlight.flightCarrier,
                                    FlightNumber=localFlight.flightNumber
                                }
                            }
                            );
                        }
                    }
                }
            }

            // Retorna la lista de rutas como un objeto JSON
            return JsonConvert.SerializeObject(routes);
        }

    }
}
