namespace Newshore_BackEnd.Models
{
    public class JorneyModel
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
        public List<FlightModel> Vuelos { get; set; }
    }
}
