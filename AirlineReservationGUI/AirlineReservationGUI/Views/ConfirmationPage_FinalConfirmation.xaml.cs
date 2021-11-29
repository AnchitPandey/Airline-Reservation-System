using AirlineReservationGUI.Models;
using System;
using System.Collections.Generic;
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
    public partial class ConfirmationPage_FinalConfirmation : Window
    {
        public AvailableFlightInformation availableFlightInformation;
        public int selectedSeatId;
        public string nameOnTicket;
        Intel_AirlineReservationService.Intel_AirlineReservationServiceClient client;


        public ConfirmationPage_FinalConfirmation(AvailableFlightInformation availableFlightInformation, int selectedSeatId, string nameOnTicket)
        {
                InitializeComponent();
                this.availableFlightInformation = availableFlightInformation;
                this.selectedSeatId = selectedSeatId;
                this.client = new Intel_AirlineReservationService.Intel_AirlineReservationServiceClient();
                this.nameOnTicket = nameOnTicket;

            this.seatIdTextBlock.Text = this.selectedSeatId.ToString();
            this.nameOnTicketTextBlock.Text = this.nameOnTicket;
            this.departureDateTextBlock.Text = this.availableFlightInformation.departureDate.Date.ToString();
            this.departureLocationTextBlock.Text = this.availableFlightInformation.departureLocationName.ToString();
            this.departureTimeTextBlock.Text = this.availableFlightInformation.departureTime.ToString();  
        }
        private void Confirm(object sender, RoutedEventArgs e)
        {
            try
            {
                this.client = new Intel_AirlineReservationService.Intel_AirlineReservationServiceClient();
                int successCode = client.BookSeat(availableFlightInformation.scheduleId,
                    this.selectedSeatId);
                // if user was successful in entering in the db
                if (successCode == 1)
                {
                    MessageBox.Show("Booking Successful !");
           
                    // TODO: make this asynchronous
                    client.addBooking(this.availableFlightInformation.scheduleId, this.availableFlightInformation.passengerId,
                        this.selectedSeatId, this.availableFlightInformation.price , this.nameOnTicket);

                    AvailableFlightsView availableFlightsView = new AvailableFlightsView(availableFlightInformation.passengerId);
                    availableFlightsView.Show();
                    this.Close();
                }
                else if (successCode == -1)
                {
                    MessageBox.Show("This seat is not available anymore. Please try another seat");
                    ConfirmationDetails_SeatSelectionView confirmationDetails_SeatSelectionView = new ConfirmationDetails_SeatSelectionView
                        (this.availableFlightInformation, nameOnTicket);
                    confirmationDetails_SeatSelectionView.Show();
                    this.Close();     
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Internal error. Please try again later");
            }
        }

        private void backButtonClicked(object sender, RoutedEventArgs e)
        {
            AvailableFlightsView availableFlightsView = new AvailableFlightsView(this.availableFlightInformation.passengerId);
            availableFlightsView.Show();
            this.Close();
        }
    }
}