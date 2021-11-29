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
    /// Interaction logic for ScheduleSearchView.xaml
    /// </summary>
    public partial class ScheduleSearchView : Window
    {
        int passengerId;
        ObservableCollection<string> allLocations;
        //List<string> allLocations;
        Intel_AirlineReservationService.Intel_AirlineReservationServiceClient client;
        ObservableCollection<string> classType;
        //List<string> classType;
        public ScheduleSearchView(int passengerId)
        {   
            InitializeComponent();
            this.classType = new ObservableCollection<string>();
            this.allLocations = new ObservableCollection<string>(); 
            this.client = new Intel_AirlineReservationService.Intel_AirlineReservationServiceClient();
            this.passengerId = passengerId;
            this.classType.Add("economy");
            this.classType.Add("firstClass");
            this.classType.Add("business");

            // TODO: this.isLoading = true; 
            List<string> locationNames = client.GetAllLocations().ToList();
            foreach (var locationName in locationNames)
                this.allLocations.Add(locationName);
            
            this.departureLocationComboBox.ItemsSource = this.allLocations;
            this.arrivalLocationComboBox.ItemsSource = this.allLocations;
            this.travelClassComboBox.ItemsSource = this.classType;
            // TODO: this.isLoading = false; 
        }

        private void RefreshView(object sender, RoutedEventArgs e)
        {          
            this.allLocations.Clear();
            // TODO: this.isLoading = true
            List<string> locationNames = this.client.GetAllLocations().ToList();
            foreach (var locationName in locationNames)
            {
                this.allLocations.Add(locationName);
            }
            // TODO: this.isLoading = false
        }

        private void GetBookingHistory(object sender, RoutedEventArgs e)
        {
            BookingHistoryView bookingHistoryView = new BookingHistoryView(passengerId);
            bookingHistoryView.ShowDialog();
            this.Close();
            // Write code to display custom user control
        }
        private void GetAvailableFlights(object sender, RoutedEventArgs e)
        {     
            string selectedDepartureLocationValue = (string)departureLocationComboBox.SelectedItem;
            string selectedArrivalLocationValue = (string)arrivalLocationComboBox.SelectedItem; 
            string selectedDateValue = this.depDateDatePicker.SelectedDate.GetValueOrDefault().ToShortDateString();
            string selectedTravelClass = (string) travelClassComboBox.SelectedItem;

            // ComboBox input values validation
            if (selectedArrivalLocationValue == null  || selectedDepartureLocationValue == null || selectedDateValue == null || selectedTravelClass  == null)
            {
                MessageBox.Show("Please fill all the details ! ");
                return;
            }
            else if (selectedDepartureLocationValue == selectedArrivalLocationValue)
            {
                MessageBox.Show("Please select different departure and arrival locations !");
                return;
            }
            /*
            // on successful validation, invoke AvailableFlightsView
            AvailableFlightsView availableFlightsView = new AvailableFlightsView();
            availableFlightsView.ShowDialog();
            this.Close();
            */
        }
    }
}
