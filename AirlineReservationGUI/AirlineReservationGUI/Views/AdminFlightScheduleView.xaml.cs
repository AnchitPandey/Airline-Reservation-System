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
    /// Interaction logic for AdminFlightScheduleView.xaml
    /// </summary>
    public partial class AdminFlightScheduleView : Window
    {

        ObservableCollection<Intel_AirlineReservationService.flightSchedule> flightSchedules;
        Intel_AirlineReservationService.Intel_AirlineReservationServiceClient client;
        public AdminFlightScheduleView()
        {
            InitializeComponent();
            this.flightSchedules = new ObservableCollection<Intel_AirlineReservationService.flightSchedule>();
            try
            {
                this.client = new Intel_AirlineReservationService.Intel_AirlineReservationServiceClient();
                List<Intel_AirlineReservationService.flightSchedule> tempList = client.GetAllSchedulesFlights().ToList<Intel_AirlineReservationService.flightSchedule>();
                foreach (var obj in tempList)
                {
                    Intel_AirlineReservationService.flightSchedule flightSchedule = new Intel_AirlineReservationService.flightSchedule();
                    flightSchedule.scheduleId = obj.scheduleId;
                    flightSchedule.departureDate = obj.departureDate;
                    flightSchedule.departureLocationId = obj.departureLocationId;
                    flightSchedule.arrivalDate = obj.arrivalDate;
                   //flightSchedule.arr
                }

            }
            catch (Exception ec)
            {
                MessageBox.Show("WCF Service Error. Please try again later");
                return;
            }
        }

        private void ToAdminView(object sender, RoutedEventArgs e)
        {
            AdminView adminView = new AdminView();
            adminView.Show();
            this.Close();
        }

        private void RefreshPage(object sender, RoutedEventArgs e)
        {
            this.flightSchedules.Clear();
        }
    }
}
