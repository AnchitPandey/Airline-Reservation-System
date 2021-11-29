using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel_AirlineReservationService.Serializable_Models
{
   public class AvailableFlightInformation
    {
        public int scheduleId { get; set; }
        public TimeSpan departureTime { get; set;}
        public DateTime departureDate { get; set; }
        public TimeSpan arrivalTime { get; set; }
        public DateTime arrivalDate { get; set; }
        public string flightNumber { get; set; }
        public string flightName { get; set; }
        public string departureLocationName { get; set; }
        public string arrivalLocationName { get; set; }
        public double price { get; set; }
        public string classType { get; set;}
    }
}
