using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservationGUI.Models
{
    public class Booking
    {
       public int bookingId { get; set; }
       public DateTime bookingDate { get; set; }
       public int scheduleId { get; set; }
       public int passengerId { get; set; }
       public int seatId { get; set; }
       public float flightPrice { get; set; }
       public string nameOnTicket { get; set; }
    }
}
