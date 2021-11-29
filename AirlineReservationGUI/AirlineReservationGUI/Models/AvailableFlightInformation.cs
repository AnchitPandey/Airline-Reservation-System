using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservationGUI.Models
{
  public class AvailableFlightInformation
    {
        public int passengerId { get; set; }
        public int scheduleId { get; set; }
        public string flightName { get; set; }
        public string flightNumber { get; set; }
        public DateTime departureDate { get; set; }
        public DateTime arrivalDate { get; set; }
        public string duration { get; set; }
        public string arrivalLocationName { get; set; }
        public string departureLocationName { get; set; }
        public double price { get; set; }
        public TimeSpan departureTime { get; set; }
        public TimeSpan arrivalTime { get; set; }
        public string classType { get; set; }

    }
}
