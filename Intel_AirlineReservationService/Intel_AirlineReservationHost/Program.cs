using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Intel_AirlineReservationHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new
              ServiceHost(typeof(Intel_AirlineReservationService.Intel_AirlineReservationService)))
            {
                host.Open();
                Console.WriteLine("Host started @ " + DateTime.Now.ToString());
                Console.ReadLine();
            }
        }
    }
}
