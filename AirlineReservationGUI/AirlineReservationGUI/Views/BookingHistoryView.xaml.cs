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
    /// Interaction logic for BookingHistoryView.xaml
    /// </summary>
    public partial class BookingHistoryView : Window
    {
        // Instance variables needed
        public Intel_AirlineReservationService.Intel_AirlineReservationServiceClient client;
        public ObservableCollection<Intel_AirlineReservationService.BookingHistory> bookingHistories;
        int passengerId;
        // TODO: Add Loading Screen
        public BookingHistoryView(int passengerId)
        {
            InitializeComponent();
            this.passengerId = passengerId;
            bookingHistories = new ObservableCollection<Intel_AirlineReservationService.BookingHistory>();
            this.BookingData.ItemsSource = this.bookingHistories;
            this.defaultPlaceholder.Visibility = Visibility.Collapsed;
            try
            {
                client = new Intel_AirlineReservationService.Intel_AirlineReservationServiceClient();
                List<Intel_AirlineReservationService.BookingHistory> bks = client.GetBookingHistory(this.passengerId).ToList<Intel_AirlineReservationService.BookingHistory>();
                foreach (var obj in bks)
                {
                    bookingHistories.Add(obj);
                }
                if (bookingHistories.Count () > 0)
                {
                    this.defaultPlaceholder.Visibility = Visibility.Collapsed;
                    this.BookingData.Visibility = Visibility.Visible;
                }
                else
                {
                    this.defaultPlaceholder.Visibility = Visibility.Visible;
                    this.BookingData.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Internal Error. Please try again later");
                return;
            }
        }
        private void NavigateToAvailableFlightsView(object sender, RoutedEventArgs e)
        {
            AvailableFlightsView availableFlightsView = new AvailableFlightsView(this.passengerId);
            availableFlightsView.Show();
            this.Close();
        }
    }
}
