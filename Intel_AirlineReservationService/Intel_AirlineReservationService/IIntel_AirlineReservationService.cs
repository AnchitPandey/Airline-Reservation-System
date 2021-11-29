using Intel_AirlineReservationService.Model;
using Intel_AirlineReservationService.Serializable_Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Intel_AirlineReservationService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IIntel_AirlineReservationService" in both code and config file together.
    [ServiceContract]
    public interface IIntel_AirlineReservationService
    {
        [OperationContract]
        int login(string email, string password);

        [OperationContract]
        int RegisterUser(String email, String firstName, String lastName, String password, String dateOfBirth);

        [OperationContract]
        List<string> GetAllLocations();

        [OperationContract]
        List<BookingHistory> GetBookingHistory(int passengerId);

        [OperationContract]
        List<AvailableFlightInformation> GetFlightSchedules(int passengerId, string departureLocation, string arrivalLocation, DateTime departureDate, string travelClass);

        [OperationContract]
        int AddFlightSchedule(string flightNumber, string flightName, DateTime departureDate, TimeSpan departureTime, string departureLocation, DateTime arrivalDate, TimeSpan arrivalTime, string arrivalLocation);

        [OperationContract]
        void addScheduleClassPrice(int scheduleId, float classPrice1, float classPrice2);

        [OperationContract]
        void addSeatAvailability(int scheduleId, List<int> economySeatIds, List<int> businessSeatIds);

        [OperationContract]
        List<int> GetAvailableSeats(int scheduleId, string classType);

        [OperationContract]
        int BookSeat(int scheduleId, int seatId);

        [OperationContract]
        void addBooking(int scheduleId, int passengerId, int seatId, double flightPrice, string nameOnTicket);

        [OperationContract]
        List<flightSchedule> GetAllSchedulesFlights();

    }
}