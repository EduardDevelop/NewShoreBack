using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newshore_BackEnd.Bussines;
using Newshore_BackEnd.DataAcces;
using Newshore_BackEnd.Models;

namespace Newshore_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class FlightController : ControllerBase
    {
        [HttpGet("{origin}/{destination}")]
        public string GetRoutes(string origin, string destination)
        {
            FlightDA fda = new FlightDA();
            List<VueloApi> obj = fda.GET();
            FlightBussines fb = new FlightBussines();
       
          return fb.FindAllRoutes(origin, destination, obj);

         
            
        }
    }
}
