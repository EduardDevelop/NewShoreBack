namespace Newshore_BackEnd.Models
{
    public class FlightModel
    {

        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
        public Transport transport { get; set; }
    }


}
