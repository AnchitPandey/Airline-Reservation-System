using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel_AirlineReservationService.Serializable_Models
{ 
    public class BookingHistory
    {
        public int bookingId { get; set; }
        public DateTime bookingDate { get; set; }
        public string nameOnTicket { get; set; }
        public string flightName { get; set; }
        public DateTime departureDate { get; set; }
        public TimeSpan departureTime { get; set; }
        public DateTime arrivalDate { get; set; }
        public TimeSpan arrivalTime { get; set; }
        public double flightPrice { get; set; }
        public int seatId { get; set; }
        public string departureLocationName { get; set; }
        public string arrivalLocationName { get; set; }
    }
}
