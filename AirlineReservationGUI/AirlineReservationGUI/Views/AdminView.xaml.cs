using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : Window
    {
        ObservableCollection<string> allLocations;
        Intel_AirlineReservationService.Intel_AirlineReservationServiceClient client;
        ObservableCollection<int> businessSeatsIds, economySeatsIds;
        int numBusinessSeats, numEconomySeats = 0;

        private void openBusinessLayout(object sender, RoutedEventArgs e)
        {
            string numBusinessSeatsInput = numberOfBusinessSeatsInput.Text;   
            if (numBusinessSeatsInput== null || numBusinessSeatsInput.Length ==0)
            {
                MessageBox.Show("Please enter atleast one business seat");
                return;
            }
            Regex _regex = new Regex("[^0-9.-]+");
            if (_regex.IsMatch(numBusinessSeatsInput))
            {
                MessageBox.Show("Please enter numeric value for Business Seats");
                return;
            }
            // Call Seat Chart View
            this.numBusinessSeats = Int32.Parse(numBusinessSeatsInput);
            // Clearing previous selection
            this.businessSeatsIds.Clear();
            SeatChartView seatChartView = new SeatChartView(this.numBusinessSeats ,this.economySeatsIds);
            seatChartView.emitter += value => this.businessSeatsIds = value;
            //MessageBox.Show("businessSeatIds length is " + this.businessSeatsIds.Count.ToString());
            seatChartView.ShowDialog();
        }

        private void openEconomyLayout(object sender, RoutedEventArgs e)
        {
            string numEconomySeatsInput = numberOfEconomySeatsInput.Text;

            if (numEconomySeatsInput == null || numEconomySeatsInput.Length == 0)
            {
                MessageBox.Show("Please enter atleast one economy seat");
                return;
            }
            Regex _regex = new Regex("[^0-9.-]+");
            if (_regex.IsMatch(numEconomySeatsInput))
            {
                MessageBox.Show("Please enter numeric value for Economy Seats");
                return;
            }
            // Call Seat Chart View
            this.numEconomySeats = Int32.Parse(numEconomySeatsInput);
            // Clearing previous selection
            this.economySeatsIds.Clear();
            SeatChartView seatChartView = new SeatChartView(this.numEconomySeats, this.businessSeatsIds);
            seatChartView.emitter += value => this.economySeatsIds = value;
            //MessageBox.Show("economySeatIds length is " + this.economySeatsIds.Count.ToString());
            seatChartView.ShowDialog();
        }

        private void SetUpFlight(object sender, RoutedEventArgs e)
        {
            // Validate for flightName 
            string flightName = this.flightNameInput.Text;
            if (flightName == null || flightName.Length == 0)
            {
                MessageBox.Show("Please enter flight name");
                return;
            }
            // Validate for flightNumber
            string flightNumber = flightNumberInput.Text;
            if (flightNumber == null || flightNumber.Length == 0)
            {
                MessageBox.Show("Please enter flight number");
                return;
            }

            // Validate for business seats
            string numBusinessSeatsInput = numberOfBusinessSeatsInput.Text;
            if (numBusinessSeatsInput == null || numBusinessSeatsInput.Length == 0)
            {
                MessageBox.Show("Please enter atleast one business seat");
                return;
            }
            Regex _regex = new Regex("[^0-9.-]+");
            if (_regex.IsMatch(numBusinessSeatsInput))
            {
                MessageBox.Show("Please enter numeric value for Business Seats");
                return;
            }
            this.numBusinessSeats = Int32.Parse(numBusinessSeatsInput);

            // validate for economy seats
            string numEconomySeatsInput = numberOfEconomySeatsInput.Text;
            if (numEconomySeatsInput == null || numEconomySeatsInput.Length == 0)
            {
                MessageBox.Show("Please enter atleast one economy seat");
                return;
            }
            if (_regex.IsMatch(numEconomySeatsInput))
            {
                MessageBox.Show("Please enter numeric value for Economy Seats");
                return;
            }
            this.numEconomySeats = Int32.Parse(numEconomySeatsInput);
            if (this.numEconomySeats != this.economySeatsIds.Count || this.numBusinessSeats != this.businessSeatsIds.Count)
            {
                MessageBox.Show("Kindly select particular seats by clicking 'Seat Layout' button");
                return;
            }

            // Validate for Economy Price
            string economyPriceInput = this.economyClassPrice.Text;
            if (economyPriceInput == null || economyPriceInput.Length == 0)
            {
                MessageBox.Show("Please enter the price for economy seat");
                return;
            }
            if (_regex.IsMatch(economyPriceInput))
            {
                MessageBox.Show("Please enter numeric value for Economy Price");
                return;
            }
            float economyPrice = float.Parse(economyPriceInput);


            // Validate for business price
            string businessPriceInput = this.businessClassPrice.Text;
            if (businessPriceInput == null || businessPriceInput.Length == 0)
            {
                MessageBox.Show("Please enter the price for economy seat");
                return;
            }
            if (_regex.IsMatch(businessPriceInput))
            {
                MessageBox.Show("Please enter numeric value for Economy Price");
                return;
            }
            float businessPrice = float.Parse(businessPriceInput);

            // For Date Fields
            string departureDateValue = this.departureDateInput.SelectedDate.GetValueOrDefault().ToShortDateString();
            string arrivalDateValue = this.arrivalDateInput.SelectedDate.GetValueOrDefault().ToShortDateString();
            if (departureDateValue == null && arrivalDateValue == null)
            {
                MessageBox.Show("Please enter the date values");
                return;
            }

            DateTime departureDate = DateTime.Parse(departureDateValue);
            DateTime arrivalDate = DateTime.Parse(arrivalDateValue);
            // For Arrival and Departure ComboBox
            string selectedDepartureLocationValue = (string) departureLocationInput.SelectedItem;
            string selectedArrivalLocationValue = (string)arrivalLocationInput.SelectedItem;
            if (selectedArrivalLocationValue == null && selectedDepartureLocationValue == null)
            {
                MessageBox.Show("Please fill the departure and arrival airports");
                return;
            }

            // verify that departure location and arrival location are not the same
            if (selectedArrivalLocationValue == selectedDepartureLocationValue)
            {
                MessageBox.Show("Please select different departure and arrival locations");
                return;
            }

            // verify that time fields are not empty
            string departTimeString = this.departureTimeInput.Text;
            string arrivalTimeString = this.arrivalTimeInput.Text;
            if (departTimeString == null || arrivalTimeString == null || departTimeString.Length == 0 || arrivalTimeString.Length == 0)
            {
                MessageBox.Show("Please fill the departure and arrival time");
                return;
            }

            // verify that arrival time > departure time     

            // case 1: dep date > arrival date then return false
            if (DateTime.Compare(departureDate, arrivalDate) > 0)
            {
                MessageBox.Show("Please verify that arrival date is after departure date");
                return;
            }
            // case 2: dep date == arrival date then return false if arrival time <= departure time  
            else if (DateTime.Compare(departureDate, arrivalDate) == 0)
            {
                int t1 = Int32.Parse(departTimeString.Replace(":", ""));
                int t2 = Int32.Parse(arrivalTimeString.Replace(":", ""));
                int comp = t1 == t2 ? 0 : (t1 < t2 ? 1 : -1);
                if (comp != 1)
                {
                    MessageBox.Show("Please make arrival time larger than departure time for the same date");
                    return;
                }
            }

            Regex reg = new Regex("([01]?[0-9]|2[0-3]):[0-5][0-9]");
            if (!reg.IsMatch (departTimeString) || !reg.IsMatch (arrivalTimeString))
            {
                MessageBox.Show("Please enter time in hh:mm format. For eg: 2:30, 17:30 etc");
                return;
            }
            TimeSpan departTime = TimeSpan.Parse(departTimeString);
            TimeSpan arrTime = TimeSpan.Parse (arrivalTimeString);

            try
            {
                // TODO: Start these operations in a different background thread
                // TODO: isLoading = true
                int assignedScheduleId = client.AddFlightSchedule(flightName, flightNumber, departureDate, departTime, selectedDepartureLocationValue,
                    arrivalDate, arrTime, selectedArrivalLocationValue);
                client.addScheduleClassPrice(assignedScheduleId, economyPrice, businessPrice);
                client.addSeatAvailability(assignedScheduleId, economySeatsIds.ToArray(), businessSeatsIds.ToArray());
                // TODO: isLoading = false
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error with WCF Service. Please try again");
                return;
                // TODO: Add to Log file    
            }
            MessageBox.Show("Flight Booked Successfully");
            refreshView();
        }

        private void CheckFlightSchedule(object sender, RoutedEventArgs e)
        {
            AdminFlightScheduleView adminFlightScheduleView = new AdminFlightScheduleView();
            adminFlightScheduleView.Show();
            this.Close();
        }

        private void backButtonClicked(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            loginView.Show();
            this.Close();
        }

        private void AdminLogout(object sender, RoutedEventArgs e)
        {

        }

        public void refreshView ()
        {
            // reset dep date
            this.departureDateInput.SelectedDate = null;
            this.departureDateInput.DisplayDate = DateTime.Today;

            // reset arrival date
            this.departureDateInput.SelectedDate = null;
            this.departureDateInput.DisplayDate = DateTime.Today;

            // reset flight name, flight number, departure time & arrival time combobox 
            this.flightNameInput.Text = "";
            this.flightNumberInput.Text = "";
            this.departureTimeInput.Text = "";
            this.arrivalTimeInput.Text = "";
            this.businessClassPrice.Text = "";
            this.economyClassPrice.Text = "";

            // Clear selected seats 
            this.economySeatsIds.Clear();
            this.businessSeatsIds.Clear();   
        }

        public AdminView()
        {
            InitializeComponent();
            try
            {
                client = new Intel_AirlineReservationService.Intel_AirlineReservationServiceClient();
                // isLoading = True
                var locations = client.GetAllLocations().ToList();
                this.allLocations = new ObservableCollection<string>();
                this.economySeatsIds = new ObservableCollection<int>();
                this.businessSeatsIds = new ObservableCollection<int>();
                this.numBusinessSeats = 0;
                this.numEconomySeats = 0;
                foreach (var location in locations)
                {
                    this.allLocations.Add(location);
                }
                departureLocationInput.ItemsSource = this.allLocations;
                arrivalLocationInput.ItemsSource = this.allLocations;
                // isLoading = false
            } catch (Exception ex)
            {
                MessageBox.Show("Internal Error. Please try again later");
            }
        }       
    }
}
