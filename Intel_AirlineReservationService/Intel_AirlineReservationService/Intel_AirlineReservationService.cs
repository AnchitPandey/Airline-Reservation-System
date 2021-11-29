using Intel_AirlineReservationService.Model;
using Intel_AirlineReservationService.Serializable_Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Intel_AirlineReservationService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Intel_AirlineReservationService" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class Intel_AirlineReservationService : IIntel_AirlineReservationService
    {
        public int login(string email, string password)
        {
            try
            {
                using (var context = new IntelAirlineReservationDBEntities())
                {
                    int? passengerId = context.passengers.Where(c => c.emailId == email).Select(c => c.passengerId).FirstOrDefault();
                    if (passengerId == null)
                        return -3;

                    passengerId = context.passengers.Where(c => c.emailId == email && c.password == password)
                                                         .Select(c => c.passengerId).FirstOrDefault();
                    if (passengerId == null)
                        return -1;
                    else
                        return (int)passengerId;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Connecting to Database !");
                // Add Log
                return -2;
            }
        }
        public int RegisterUser(string email, string firstName, string lastName, string password, string dateOfBirth)
        {
            try
            {
                using (var context = new IntelAirlineReservationDBEntities())
                {
                    bool userAlreadyRegistered = context.passengers.Where(c => c.emailId == email).Any();
                    if (userAlreadyRegistered)
                        return -1;

                    var user = new passenger()
                    {
                        firstName = firstName,
                        lastName = lastName,
                        emailId = email,
                        password = password,
                        dateOfBirth = DateTime.Parse(dateOfBirth)
                    };
                    context.passengers.Add(user);
                    context.SaveChanges();
                    return 1;
                }
            } catch (Exception e)
            {
                // TODO: Add to Logs
                Console.WriteLine("Error connecting to Database");
                return -2;
            }
        }

        public List<string> GetAllLocations()
        {
            using (var context = new IntelAirlineReservationDBEntities())
            {
                List<string> allLocations = context.locations.Select(c => c.locationName).ToList();
                return allLocations;
            }
        }

        public List<BookingHistory> GetBookingHistory (int passengerId)
        {
            using (var context = new IntelAirlineReservationDBEntities())
            {
                List<BookingHistory> bookingHistories = new List<BookingHistory>();

                var bookingHistory = context.bookings.Join(context.flightSchedules,
                    bk => bk.scheduleId, fs => fs.scheduleId,
                    (bk, fs) => new
                    {
                        bookingId = bk.bookingId,
                        bookingDate = bk.bookingDate,
                        nameOnTicket = bk.nameOnTicket,
                        flightName = fs.flightName,
                        flightNumber = fs.flightNumber,
                        departureDate = fs.departureDate,
                        departureTime = fs.departureTime,
                        arrivalDate = fs.arrivalDate,
                        arrivalTime = fs.arrivalTime,
                        flightPrice = bk.flightPrice,
                        seatId = bk.seatId,
                        passengerId = bk.passengerId,
                        departureLocationId = fs.departureLocationId,
                        arrivalLocationId = fs.arrivalLocationId
                    }
                    ).Where (c => c.passengerId == passengerId).OrderByDescending(c => c.bookingDate);

                var bhDepLocationName = bookingHistory.Join(context.locations, bk => bk.departureLocationId,
                    fs => fs.locationId, (bk, fs) => new
                    {
                        bookingId = bk.bookingId,
                        bookingDate = bk.bookingDate,
                        nameOnTicket = bk.nameOnTicket,
                        flightName = bk.flightName,
                        flightNumber = bk.flightNumber,
                        departureDate = bk.departureDate,
                        departureTime = bk.departureTime,
                        arrivalDate = bk.arrivalDate,
                        arrivalTime = bk.arrivalTime,
                        flightPrice = bk.flightPrice,
                        seatId = bk.seatId,
                        passengerId = bk.passengerId,
                        departureLocationId = bk.departureLocationId,
                        departureLocationName = fs.locationName,
                        arrivalLocationId = bk.arrivalLocationId
                    });

                var bhArrLocationName = bhDepLocationName.Join(context.locations, bk => bk.arrivalLocationId,
                    fs => fs.locationId, (bk, fs) => new
                    {
                        bookingId = bk.bookingId,
                        bookingDate = bk.bookingDate,
                        nameOnTicket = bk.nameOnTicket,
                        flightName = bk.flightName,
                        flightNumber = bk.flightNumber,
                        departureDate = bk.departureDate,
                        departureTime = bk.departureTime,
                        arrivalDate = bk.arrivalDate,
                        arrivalTime = bk.arrivalTime,
                        flightPrice = bk.flightPrice,
                        seatId = bk.seatId,
                        passengerId = bk.passengerId,
                        departureLocationId = bk.departureLocationId,
                        departureLocationName = bk.departureLocationName,
                        arrivalLocationId = bk.arrivalLocationId,
                        arrivalLocationName = fs.locationName
                    }).ToList();

                foreach (var curr in bhArrLocationName)
                {
                    BookingHistory bk = new BookingHistory();
                    bk.bookingId = curr.bookingId;
                    bk.bookingDate = curr.bookingDate;
                    bk.nameOnTicket = curr.nameOnTicket;
                    bk.flightName = curr.flightName;
                    bk.flightPrice = curr.flightPrice;
                    bk.seatId = curr.seatId;
                    bk.departureDate = curr.departureDate;
                    bk.departureTime = (TimeSpan)curr.departureTime;
                    bk.arrivalDate = curr.arrivalDate;
                    bk.arrivalTime = (TimeSpan) curr.arrivalTime;
                    bk.departureLocationName = curr.departureLocationName;
                    bk.arrivalLocationName = curr.arrivalLocationName;
                    bookingHistories.Add(bk);
                }
                return bookingHistories;
            }
        }

        public List<AvailableFlightInformation> GetFlightSchedules(int passengerId, string departureLocation, string arrivalLocation, DateTime departureDate, string travelClass)
        {             
             using (var context = new IntelAirlineReservationDBEntities())
             {
                List<AvailableFlightInformation> availableFlightInformation = new List<AvailableFlightInformation>();                    
                int departureLocationId = (int)context.locations.Where(c => c.locationName == departureLocation).Select(c => c.locationId).FirstOrDefault();
                int arrivalLocationId = (int)context.locations.Where(c => c.locationName == arrivalLocation).Select(c => c.locationId).FirstOrDefault();
               
                var availableSeatsOfParticularClass = context.seatingArrangements.Where(c => c.seatAvailability == true && c.classType == travelClass).
                    GroupBy(c => c.scheduleId).Select(c => new { scheduleId = c.Key });
                //Console.WriteLine(availableSeatsOfParticularClass);
              
                var availableFlights = context.flightSchedules.Where(c => c.departureLocationId == departureLocationId &&
                c.arrivalLocationId == arrivalLocationId && c.departureDate == departureDate).Join(context.flightClassPrices,
                    fs => fs.scheduleId, fc => fc.scheduleId,
                    (fs, fc) => new
                    {
                        scheduleId = fs.scheduleId,
                        departureTime = fs.departureTime,
                        departureDate = fs.departureDate,
                        departureLocationId = fs.departureLocationId,
                        arrivalTime = fs.arrivalTime,
                        arrivalDate = fs.arrivalDate,
                        arrivalLocationId = fs.arrivalLocationId,
                        flightNumber = fs.flightNumber,
                        flightName = fs.flightName,
                        price = fc.price,
                        classType = fc.classType
                    }).Where(c => c.classType == travelClass);

                var availableFlightsWithRequiredClassType = availableSeatsOfParticularClass.Join(availableFlights,
                    sa => sa.scheduleId, fs => fs.scheduleId, (sa, fs) => new
                    {
                        scheduleId = fs.scheduleId,
                        departureTime = fs.departureTime,
                        departureDate = fs.departureDate,
                        departureLocationId = fs.departureLocationId,
                        arrivalTime = fs.arrivalTime,
                        arrivalDate = fs.arrivalDate,
                        arrivalLocationId = fs.arrivalLocationId,
                        flightNumber = fs.flightNumber,
                        flightName = fs.flightName,
                        price = fs.price,
                        classType = fs.classType
                    });

                var convertDepartureIdToLocation = availableFlightsWithRequiredClassType.Join(context.locations,
                    af => af.departureLocationId, lo => lo.locationId, (af, lo) => new
                    {
                        scheduleId = af.scheduleId,
                        departureTime = af.departureTime,
                        departureDate = af.departureDate,
                        departureLocationId = af.departureLocationId,
                        arrivalTime = af.arrivalTime,
                        arrivalDate = af.arrivalDate,
                        arrivalLocationId = af.arrivalLocationId,
                        flightNumber = af.flightNumber,
                        flightName = af.flightName,
                        departureLocationName = lo.locationName,
                        price = af.price,
                        classType = af.classType
                    });
                var convertArrivalIdToLocation = convertDepartureIdToLocation.Join(context.locations,
                    af => af.arrivalLocationId, lo => lo.locationId, (af, lo) => new
                    {                       
                        scheduleId = af.scheduleId,
                        departureTime = af.departureTime,
                        departureDate = af.departureDate,
                        arrivalTime = af.arrivalTime,
                        arrivalDate = af.arrivalDate,
                        flightNumber = af.flightNumber,
                        flightName = af.flightName,
                        departureLocationName = af.departureLocationName,
                        arrivalLocationName = lo.locationName,
                        price = af.price,
                        classType = af.classType
                    }).ToList();


                /*
                // passenger previous booking
                var passengerPreviousBooking = context.bookings.Where(c => c.passengerId == passengerId).Join(context.seatingArrangements,
                    bk => bk.scheduleId, sa => sa.scheduleId, (bk, sa) => new
                    {
                        scheduleId = bk.scheduleId
                    });

                
                var filteredList = convertArrivalIdToLocation.Join(passengerPreviousBooking, fl => fl.scheduleId,
                    pb => pb.scheduleId, (fl, pb) => new 
                    {
                        scheduleId = fl.scheduleId,
                        departureTime = fl.departureTime,
                        departureDate = fl.departureDate,
                        arrivalTime = fl.arrivalTime,
                        arrivalDate = fl.arrivalDate,
                        flightNumber = fl.flightNumber,
                        flightName = fl.flightName,
                        departureLocationName = fl.departureLocationName,
                        arrivalLocationName = fl.arrivalLocationName,
                        price = fl.price,
                        classType = fl.classType
                    }).ToList();
                    */
             //   var passengerPreviousBooking = context.bookings.Where (c => c.passengerId == passengerId && c.)

                foreach( var temp in convertArrivalIdToLocation)
                {
                 // Console.WriteLine("flight price is " + temp.price);
                    AvailableFlightInformation av = new AvailableFlightInformation();
                    av.flightName = temp.flightName;
                    av.flightNumber = temp.flightNumber;
                    av.price = temp.price;
                    av.scheduleId = temp.scheduleId;
                    av.departureTime =(TimeSpan) temp.departureTime;
                    av.departureDate = temp.departureDate;
                    av.arrivalTime = (TimeSpan) temp.arrivalTime;
                    av.arrivalDate = temp.arrivalDate;
                    av.arrivalLocationName = temp.arrivalLocationName;
                    av.departureLocationName = temp.departureLocationName;
                    av.classType = temp.classType;
                    availableFlightInformation.Add(av);
                }       
                return availableFlightInformation;
            }
        }

        public void addScheduleClassPrice(int scheduleId, float classPrice1, float classPrice2)
        {
            using (var context = new IntelAirlineReservationDBEntities())
            {
                // economy class object
                flightClassPrice obj = new flightClassPrice();
                obj.scheduleId = scheduleId;
                obj.classType = "economy";
                obj.price = classPrice1;
                context.flightClassPrices.Add(obj);
                // business class object
                obj = new flightClassPrice();
                obj.scheduleId = scheduleId;
                obj.classType = "business";
                obj.price = classPrice2;
                context.flightClassPrices.Add(obj);
                context.SaveChanges();
            }
        }
        public int AddFlightSchedule(string flightNumber, string flightName, DateTime departureDate, TimeSpan departureTime, string departureLocation, DateTime arrivalDate, TimeSpan arrivalTime, string arrivalLocation)
        {
            using (var context = new IntelAirlineReservationDBEntities())
            {
                int departureLocationId = context.locations.Where(c => c.locationName == departureLocation).Select(c => c.locationId).First();
                int arrivalLocationId = context.locations.Where(c => c.locationName == arrivalLocation).Select(c => c.locationId).First();
                flightSchedule flightSchedule = new flightSchedule();
                flightSchedule.flightName = flightName;
                flightSchedule.flightNumber = flightNumber;
                flightSchedule.departureDate = departureDate;
                flightSchedule.arrivalDate = arrivalDate;
                flightSchedule.arrivalLocationId = arrivalLocationId;
                flightSchedule.departureLocationId = departureLocationId;
                flightSchedule.departureTime = departureTime;
                flightSchedule.arrivalTime = arrivalTime;
                context.flightSchedules.Add(flightSchedule);
                context.SaveChanges();
                int scheduleId = flightSchedule.scheduleId;
                return scheduleId;
            }
        }

        public void addSeatAvailability(int scheduleId, List<int> economySeatIds, List<int> businessSeatIds)
        {
            using (var context = new IntelAirlineReservationDBEntities())
            {
                foreach(int seatId in economySeatIds)
                {
                    seatingArrangement seatingArrangement = new seatingArrangement();
                    seatingArrangement.scheduleId = scheduleId;
                    seatingArrangement.seatAvailability = true;
                    seatingArrangement.seatId = seatId;
                    seatingArrangement.classType = "economy";
                    context.seatingArrangements.Add(seatingArrangement);
                }

                foreach (int seatId in businessSeatIds)
                {
                    seatingArrangement seatingArrangement = new seatingArrangement();
                    seatingArrangement.scheduleId = scheduleId;
                    seatingArrangement.seatAvailability = true;
                    seatingArrangement.seatId = seatId;
                    seatingArrangement.classType = "business";
                    context.seatingArrangements.Add(seatingArrangement);
                }
                context.SaveChanges();
            }
        }

        public List<int> GetAvailableSeats(int scheduleId, string classType)
        {
            using (var context = new IntelAirlineReservationDBEntities())
            {
                var getSeats = context.seatingArrangements.Where(c => c.scheduleId == scheduleId && c.classType == classType
               && c.seatAvailability == true).Select(c => c.seatId).ToList<int>();
                return getSeats;
            }
        }


        public int BookSeat(int scheduleId, int seatId)
        {
            seatingArrangement seatingArrangement = null;
            using (var context = new IntelAirlineReservationDBEntities())
            {
                seatingArrangement = context.seatingArrangements.Where(c => c.seatId == seatId && c.scheduleId == scheduleId && c.seatAvailability == true).FirstOrDefault();
                if (seatingArrangement == null)
                {
                    return -1;
                }

                seatingArrangement.seatAvailability = false;
                try
                {
                    context.Entry(seatingArrangement).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return 1;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                  //  Console.WriteLine("Concurrency Exception Occurred.");
                    return -1;
                }
            }    
        }

        public void addBooking(int scheduleId, int passengerId, int seatId, double flightPrice, string nameOnTicket)
        {
            try
            {
                using (var context = new IntelAirlineReservationDBEntities())
                {
                    booking booking = new booking();
                    booking.scheduleId = scheduleId;
                    booking.passengerId = passengerId;
                    booking.seatId = seatId;
                    booking.flightPrice = flightPrice;
                    booking.nameOnTicket = nameOnTicket;
                    booking.bookingDate = DateTime.Now;
                    context.bookings.Add(booking);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in addBooking() ");
                Console.WriteLine("Exception is ");
                Console.WriteLine(ex.ToString());
            }
        }

        public List<flightSchedule> GetAllSchedulesFlights()
        {
            using (var context = new IntelAirlineReservationDBEntities())
            {
                List<flightSchedule> flightSchedules = new List<flightSchedule>();
                var flights = context.flightSchedules.Select(c => c).ToList<flightSchedule>();            
                foreach (var flight in flights)
                {
                    flightSchedules.Add(flight);
                }
                return flightSchedules;
            }
        }
    }
}