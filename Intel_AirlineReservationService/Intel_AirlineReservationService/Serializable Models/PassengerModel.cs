using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Intel_AirlineReservationService.Serializable_Models
{
    [DataContract]
    public class PassengerModel
    {
        [DataMember]
        public int passengerId { get; set; }
        [DataMember]
        public string emailId { get; set; }
        [DataMember]
        public string firstName { get; set; }
        [DataMember]
        public string lastName { get; set; }
        [DataMember]
        public DateTime dateOfBirth { get; set; }
        [DataMember]
        public string password { get; set; }

    }
}
