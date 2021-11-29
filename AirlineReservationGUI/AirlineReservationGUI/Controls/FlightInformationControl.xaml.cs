using AirlineReservationGUI.Models;
using AirlineReservationGUI.Views;
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
    /// Interaction logic for FlightInformationControl.xaml
    /// </summary>
    public partial class FlightInformationControl : UserControl
    {
        public AvailableFlightInformation AvailableFlightInformation
        {
            get { return (AvailableFlightInformation)GetValue(AvailableFlightInformationProperty); }
            set { SetValue(AvailableFlightInformationProperty, value); }
        }      
        // Using a DependencyProperty as the backing store for AvailableFlightInformation. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AvailableFlightInformationProperty =
            DependencyProperty.Register("AvailableFlightInformation", typeof(AvailableFlightInformation), typeof(FlightInformationControl), new PropertyMetadata( new AvailableFlightInformation() {

                flightName = "Default Flight",
                flightNumber = "Def",
                departureLocationName = "Def.",
                arrivalLocationName = "Default",
            }, SetValue));

        private static void SetValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FlightInformationControl control = d as FlightInformationControl;
            if (control != null)
            {
                control.flightNameTextBlock.Text = (e.NewValue as AvailableFlightInformation).flightName;
                control.departureTextBlock.Text = (e.NewValue as AvailableFlightInformation).departureLocationName;
                control.arrivalTextBlock.Text = (e.NewValue as AvailableFlightInformation).arrivalLocationName;
                control.priceTextBlock.Text = (e.NewValue as AvailableFlightInformation).price.ToString();
                control.departureTimeBlock.Text = (e.NewValue as AvailableFlightInformation).departureTime.ToString();
                control.arrivalTimeBlock.Text = (e.NewValue as AvailableFlightInformation).arrivalTime.ToString();

            }
            //throw new NotImplementedException();
        }

        public FlightInformationControl()
        {
            InitializeComponent();
        }
        private void ProceedToSeatBooking(object sender, RoutedEventArgs e)
        {
            ConfirmationPage_UserDetailsView confirmationPage_UserDetailsView = new ConfirmationPage_UserDetailsView(AvailableFlightInformation);
            confirmationPage_UserDetailsView.Show();


            var parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }
    }
}