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
    /// Interaction logic for ConfirmationDetails_SeatSelectionView.xaml
    /// </summary>
    public partial class ConfirmationDetails_SeatSelectionView : Window
    {
        Intel_AirlineReservationService.Intel_AirlineReservationServiceClient client;
        AvailableFlightInformation availableFlightInformation;
        HashSet<int> availableSeatsSet;
        Dictionary<int, Button> integerToButtonMap;
        bool hasSelectedSeat;
        int selectedSeatId;
        string nameOnTicket;
        // TODO: Optimize this
        public void assignColourToSeats ()
        {
            for (int i =1; i<=8;i+=1)
            {
                Button b = this.integerToButtonMap[i];
                if (this.availableSeatsSet.Contains(i))
                    b.Background = new SolidColorBrush(Colors.Green);                
                else
                    b.Background = new SolidColorBrush(Colors.Red);
            }
        }
 
        public void reloadData (HashSet<int> availableSeatsSet)
        {
            try
            {
                this.client = new Intel_AirlineReservationService.Intel_AirlineReservationServiceClient();
                List<int> availableSeats = this.client.GetAvailableSeats(this.availableFlightInformation.scheduleId, this.availableFlightInformation.classType).ToList<int>();
                if (availableSeats.Count == 0)
                {
                    MessageBox.Show("Sorry, no seats available for this flight. Taking you to the home page");
                    AvailableFlightsView availableFlightsView = new AvailableFlightsView(this.availableFlightInformation.passengerId);
                    availableFlightsView.Show();
                    this.Close();
                }
                foreach (int seatId in availableSeats)
                    availableSeatsSet.Add(seatId);
                assignColourToSeats();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Internal Error. Please try again later");
                return;
            }
        }

        public void initializeDictionary (Dictionary<int, Button> integerToButtonMap)
        {
            integerToButtonMap.Add(1, this.seat1);
            integerToButtonMap.Add(2, this.seat2);
            integerToButtonMap.Add(3, this.seat3);
            integerToButtonMap.Add(4, this.seat4);
            integerToButtonMap.Add(5, this.seat5);
            integerToButtonMap.Add(6, this.seat6);
            integerToButtonMap.Add(7, this.seat7);
            integerToButtonMap.Add(8, this.seat8);
        }

        public ConfirmationDetails_SeatSelectionView(AvailableFlightInformation availableFlightInformation, string nameOnTicket)
        {
        
            InitializeComponent();
            // TODO: isLoading = true
            this.nameOnTicket = nameOnTicket;
            this.hasSelectedSeat = false;
            this.selectedSeatId = -1;
            this.integerToButtonMap = new Dictionary<int, Button>();
            initializeDictionary(integerToButtonMap);
            this.availableFlightInformation = availableFlightInformation;
            this.client = new Intel_AirlineReservationService.Intel_AirlineReservationServiceClient();
            this.availableSeatsSet = new HashSet<int>();
            reloadData(this.availableSeatsSet);
            // TODO: isLoading = false
        }
        private void Confirm(object sender, RoutedEventArgs e)
        {
            if (!this.hasSelectedSeat)
            {
                MessageBox.Show("Please select your seat first");
                return;
            }
            ConfirmationPage_FinalConfirmation confirmationPage_FinalConfirmation = new ConfirmationPage_FinalConfirmation
                (availableFlightInformation, this.selectedSeatId, nameOnTicket);
            confirmationPage_FinalConfirmation.Show();
            this.Close();
        }
        private void RefreshPage(object sender, RoutedEventArgs e)
        {
            this.hasSelectedSeat = false;
            this.selectedSeatId = 0;
            // isLoading = true
            reloadData(this.availableSeatsSet);
            // isLoading = false
        }
        private void SeatClicked(object sender, RoutedEventArgs e)
        {
            // if user already selected a seat, then request user to deselect previous seat
            int seatId = int.Parse((sender as Button).Content.ToString());
            Button b;
            if (hasSelectedSeat)
            {
                if (this.selectedSeatId == seatId)
                {
                    b = this.integerToButtonMap[this.selectedSeatId];
                    b.Background = new SolidColorBrush(Colors.LightGray);
                    this.selectedSeatId = -1;
                    hasSelectedSeat = !hasSelectedSeat;
                }
                else {
                    MessageBox.Show("Please deselect your previous seat ");
                }
                return;
            }
            // if user clicks on a seat that is reserved
            if (!this.availableSeatsSet.Contains(seatId))
            {
                MessageBox.Show("This seat was reserved. Please select a different seat ");
                return;
            }
            b = integerToButtonMap[seatId];
            b.Background = new SolidColorBrush(Colors.Yellow);
            this.hasSelectedSeat = true;
            this.selectedSeatId = seatId;
        //    MessageBox.Show("You Selected seat with seat ID " + this.selectedSeatId.ToString());
        }
    }
}
