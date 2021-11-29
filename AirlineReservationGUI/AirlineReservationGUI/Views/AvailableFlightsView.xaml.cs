using AirlineReservationGUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AirlineReservationGUI.Views
{
    /// <summary>
    /// Interaction logic for AvailableFlightsView.xaml
    /// </summary>
    public partial class AvailableFlightsView : Window
    {
       ObservableCollection<Intel_AirlineReservationService.AvailableFlightInformation> flightInfo;
       ObservableCollection<AvailableFlightInformation> passengerFlightInfo;
       public Intel_AirlineReservationService.Intel_AirlineReservationServiceClient client;

       ObservableCollection<string> departureLocations, arrivalLocations, travelClasses; 
       bool firstTimeVisibility = true;

        private int passengerId;

        public int PassengerId
        {
            get { return passengerId; }
            set { passengerId = value; }
        }

        public AvailableFlightsView(int passengerId)
        {
            InitializeComponent();

            this.placeHolderLabel.Visibility = Visibility.Visible;
            this.FlightData.Visibility = Visibility.Collapsed;
            this.noResultsLabel.Visibility = Visibility.Collapsed;

            this.passengerId = passengerId;
            client = new Intel_AirlineReservationService.Intel_AirlineReservationServiceClient();
            this.travelClasses = new ObservableCollection<string>();
            this.departureLocations = new ObservableCollection<string>();
            this.arrivalLocations = new ObservableCollection<string>();
            this.passengerFlightInfo = new ObservableCollection<AvailableFlightInformation>();
            this.flightInfo = new ObservableCollection<Intel_AirlineReservationService.AvailableFlightInformation>();
            travelClasses.Add("economy");
            travelClasses.Add("business");
            // TODO: this.isLoading = true;
            try
            {
                var locations = client.GetAllLocations();             
                foreach (var location in locations)
                {
                    this.departureLocations.Add(location);
                    this.arrivalLocations.Add(location);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Internal Error. Please refresh and try again later");
                return;
            }

            this.departureLocationComboBox.ItemsSource = this.departureLocations;
            this.arrivalLocationComboBox.ItemsSource = this.arrivalLocations;
            this.travelClassComboBox.ItemsSource = this.travelClasses;
            // TODO: Toggle Visibility
            FlightData.ItemsSource = this.passengerFlightInfo;    
            // TODO: this.isLoading = false
        }

        private void RefreshView(object sender, RoutedEventArgs e)
        {
            // Reset all the input fields
            this.passengerFlightInfo.Clear();
            this.departureLocations.Clear();
            this.arrivalLocations.Clear();

            this.departureLocationComboBox.SelectedIndex = -1;
            this.arrivalLocationComboBox.SelectedIndex = -1;
            this.travelClassComboBox.SelectedIndex = -1;
            this.departureDateComboBox.SelectedDate = null;
            this.departureDateComboBox.DisplayDate = DateTime.Today;
            this.placeHolderLabel.Visibility = Visibility.Visible;
            this.FlightData.Visibility = Visibility.Collapsed;
            this.noResultsLabel.Visibility = Visibility.Collapsed;


            // Reload newly added locations (if any) from WCF Service
            try
            {

                this.client = new Intel_AirlineReservationService.Intel_AirlineReservationServiceClient();
                
                // TODO: isLoading = true
                var locations = client.GetAllLocations();
                foreach (var location in locations)
                {
                    this.departureLocations.Add(location);
                    this.arrivalLocations.Add(location);
                }
                // TODO: isLoading = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Internal Error. Please try again later");
                return;
            }
        }

        private void ViewBookingHistory(object sender, RoutedEventArgs e)
        {
            BookingHistoryView bookingHistoryView = new BookingHistoryView(this.passengerId);
            bookingHistoryView.Show();
            this.Close();
        }

        private void SearchAvailableFlights(object sender, RoutedEventArgs e)
        {
            // Clear previously populated list
            this.passengerFlightInfo.Clear();
          
            string departureLocation = (string)departureLocationComboBox.SelectedItem;
            string arrivalLocation = (string)arrivalLocationComboBox.SelectedItem;
            string  travelClass= (string)travelClassComboBox.SelectedItem;
            DateTime departureDate = departureDateComboBox.SelectedDate.GetValueOrDefault().Date;

            // Verify none of the fields are empty
            if (departureLocation == null || arrivalLocation == null || travelClass == null || departureDate == null )
            {
                MessageBox.Show("Please fill all the fields !");
                return;
            }
            
            // Verify that departure location is different from arrival location
            if (departureLocation == arrivalLocation)
            {
                MessageBox.Show("Please make sure departure and arrival locations are different");
                return;
            }
            this.client = new Intel_AirlineReservationService.Intel_AirlineReservationServiceClient();
            try
            {
                //TODO: this.isLoading = true;

                // Get the list of available flight schedules from WCF Service
                List<Intel_AirlineReservationService.AvailableFlightInformation> tempFlightInfo = client.GetFlightSchedules(passengerId, departureLocation, arrivalLocation, departureDate, travelClass).ToList();
                foreach (var flight in tempFlightInfo)
                {
                    // create object
                    AvailableFlightInformation obj = new AvailableFlightInformation();
                    obj.passengerId = passengerId;
                    obj.flightName = flight.flightName;
                    obj.departureLocationName = flight.departureLocationName;
                    obj.arrivalLocationName = flight.arrivalLocationName;
                    obj.departureDate = flight.departureDate;
                    obj.arrivalDate = flight.arrivalDate;
                    obj.price = flight.price;
                    obj.departureTime = flight.departureTime;
                    obj.arrivalTime = flight.arrivalTime;
                    obj.classType = flight.classType;
                    obj.scheduleId = flight.scheduleId;

                    // Add it to Observable collection
                    this.passengerFlightInfo.Add(obj);
                }
                // this.isLoading = false
                this.placeHolderLabel.Visibility = Visibility.Collapsed;
                if (this.passengerFlightInfo.Count > 0)
                {
                    this.noResultsLabel.Visibility = Visibility.Collapsed;
                    this.FlightData.Visibility = Visibility.Visible;
                }
                else
                {
                    this.noResultsLabel.Visibility = Visibility.Visible;
                    this.FlightData.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Internal Error. Please try again later");
            }
        }
    }
}