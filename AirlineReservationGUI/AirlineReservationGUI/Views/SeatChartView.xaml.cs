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
    /// Interaction logic for SeatChartView.xaml
    /// </summary>
    public partial class SeatChartView : Window
    {
        ObservableCollection<int> seatIdsToReserve;
        ObservableCollection<int> reservedSeatIds;
        int numSeatsToChoose;
        public event Action<ObservableCollection<int>> emitter;

        public bool IsClosed { get; private set; }


        // TODO: Optimize this using HashSet
        bool isSeatPresentIn (ObservableCollection<int> reservedSeatIds, int seatId)
        {
            foreach (var seat in reservedSeatIds)
            {
                if (seat == seatId)
                    return true;
            }
            return false;
        }

        public SeatChartView(int numSeatsToChoose, ObservableCollection<int> reservedSeatIds)
        {
            InitializeComponent();
            this.seatIdsToReserve = new ObservableCollection<int>();
            this.numSeatsToChoose = numSeatsToChoose;
            this.reservedSeatIds = reservedSeatIds;
            // Mark Seats Reserved With Red Colour 
            // TODO: Optimize this
            foreach (int seatId in reservedSeatIds)
            {
                if (seatId ==1)
                {
                    seat1.Background = new SolidColorBrush (Colors.Red);
                }
                else if (seatId == 2)
                {
                    seat2.Background = new SolidColorBrush(Colors.Red);
                }
                else if (seatId == 3)
                {
                    seat3.Background = new SolidColorBrush(Colors.Red);                  
                }
                else if (seatId == 4)
                {
                    seat4.Background = new SolidColorBrush(Colors.Red);
                }
                else if (seatId == 5)
                {
                    seat5.Background = new SolidColorBrush(Colors.Red);
                }
                else if (seatId == 6)
                {
                    seat6.Background = new SolidColorBrush(Colors.Red);
                }
                else if (seatId == 7)
                {
                    seat7.Background = new SolidColorBrush(Colors.Red);
                }
                else if (seatId == 8)
                {
                    seat8.Background = new SolidColorBrush(Colors.Red);
                }
            }
        }

        private void SeatClicked(object sender, RoutedEventArgs e)
        {
    
            int seatId = int.Parse((sender as Button).Content.ToString());
            bool differentClassSeat = isSeatPresentIn(reservedSeatIds, seatId);
            if (differentClassSeat)
            {
                MessageBox.Show("This Seat is reserved for different class. Please select other seat");
                return;
            }
            bool isAlreadySelected = isSeatPresentIn(seatIdsToReserve, seatId); 
            if (isAlreadySelected)
            {
                this.numSeatsToChoose += 1;
                Button b1 = sender as Button;
                b1.Background = new SolidColorBrush(Colors.Gray);
                seatIdsToReserve.Remove(seatId);
                return;
            }
            if (this.numSeatsToChoose == 0)
            {
                MessageBox.Show("You have reached the max seats to be alloted. Please deselect them to book new seats");
                return;
            }
            this.numSeatsToChoose -= 1;
            Button b = sender as Button;
            b.Background = new SolidColorBrush(Colors.Yellow);
            seatIdsToReserve.Add(seatId);
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            if (numSeatsToChoose != 0)
            {
                MessageBox.Show("Please select " + numSeatsToChoose + " more seats");
                return;
            }
            if (emitter != null)
            {
                emitter(seatIdsToReserve);
                this.Close();
            }
            else
            {
                MessageBox.Show(numSeatsToChoose.ToString());
                return;
            }
        }
    }
}
