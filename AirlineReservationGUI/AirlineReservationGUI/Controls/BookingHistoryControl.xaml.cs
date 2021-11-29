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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AirlineReservationGUI.Controls
{
    /// <summary>
    /// Interaction logic for BookingHistoryControl.xaml
    /// </summary>
    public partial class BookingHistoryControl : UserControl
    {
        public Intel_AirlineReservationService.BookingHistory Bookings
        {
            get { return (Intel_AirlineReservationService.BookingHistory)GetValue(BookingsProperty); }
            set { SetValue(BookingsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Bookings.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BookingsProperty =
            DependencyProperty.Register("Bookings", typeof(Intel_AirlineReservationService.BookingHistory), typeof(BookingHistoryControl), new PropertyMetadata(new Intel_AirlineReservationService.BookingHistory() {

                flightName = "Default Flight",
                departureDate = DateTime.Parse("04/08/2021"),
                arrivalDate = DateTime.Parse("04/09/2021"),
            }, SetValue));

        private static void SetValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            BookingHistoryControl control = d as BookingHistoryControl;
            control.bookingIdBlock.Text = (e.NewValue as Intel_AirlineReservationService.BookingHistory).bookingId.ToString();
            control.bookingDateBlock.Text = (e.NewValue as Intel_AirlineReservationService.BookingHistory).bookingDate.ToString();
            control.nameOnTicketTextBlock.Text = (e.NewValue as Intel_AirlineReservationService.BookingHistory).nameOnTicket.ToString();
            control.departureLocationNameTextBlock.Text = (e.NewValue as Intel_AirlineReservationService.BookingHistory).departureLocationName.ToString();
            control.arrivalLocationNameTextBlock.Text = (e.NewValue as Intel_AirlineReservationService.BookingHistory).arrivalLocationName.ToString();
            control.priceTextBlock.Text = (e.NewValue as Intel_AirlineReservationService.BookingHistory).flightPrice.ToString();
            control.flightNameTextBlock.Text = (e.NewValue as Intel_AirlineReservationService.BookingHistory).flightName.ToString();
            control.arrivalTimeTextBlock.Text = (e.NewValue as Intel_AirlineReservationService.BookingHistory).arrivalTime.ToString();
            control.seatIdTextBlock.Text = (e.NewValue as Intel_AirlineReservationService.BookingHistory).seatId.ToString();
            control.departureTimeTextBlock.Text = (e.NewValue as Intel_AirlineReservationService.BookingHistory).departureTime.ToString();
        }

        public BookingHistoryControl()
        {
            InitializeComponent();
        }
    }
}
