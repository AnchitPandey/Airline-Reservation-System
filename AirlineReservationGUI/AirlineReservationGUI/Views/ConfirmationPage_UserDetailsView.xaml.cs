using AirlineReservationGUI.Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ConfirmationPage_UserDetailsView.xaml
    /// </summary>
    public partial class ConfirmationPage_UserDetailsView : Window
    {
        string firstName, lastName, country, passportNumber;
        string nameOnTicket;
        string age;

        private void backButtonClicked(object sender, RoutedEventArgs e)
        {
            AvailableFlightsView availableFlightsView = new AvailableFlightsView(this.availableFlightInformation.passengerId);
            availableFlightsView.Show();
            this.Close();
        }

        AvailableFlightInformation availableFlightInformation;
        public ConfirmationPage_UserDetailsView(AvailableFlightInformation availableFlightInformation)
        {
            InitializeComponent();
            this.nameOnTicket = "";
            this.firstName = "";
            this.lastName = "";
            this.country = "";
            this.passportNumber = "";
            this.age = "";
            this.firstNameTextBox.Text = this.firstName;
            this.lastNameTextBox.Text = this.lastName;
            this.countryTextBox.Text = this.country;
            this.passportNumberTextBox.Text = this.passportNumber;
            this.ageTextBox.Text = this.age;
            this.availableFlightInformation = availableFlightInformation;
        }

        private void NavigateToSeatSelectionPage(object sender, RoutedEventArgs e)
        {
            this.firstName = firstNameTextBox.Text;
            this.lastName = lastNameTextBox.Text;
            this.age = ageTextBox.Text;
            this.country = countryTextBox.Text;
            this.passportNumber = passportNumberTextBox.Text;

            // check for null values
            if (firstName == null || lastName == null || age == null || country == null || passportNumber == null
                || firstName.Length == 0 || lastName.Length == 0 || age.Length ==0 || country.Length ==0 || passportNumber.Length ==0) 
            {
                MessageBox.Show("Please enter all the fields");
                return;
            }

            // check age for integer
            int newAge = -1;
            Regex regex = new Regex("[0-9]");
            if (!regex.IsMatch(age))
            {
                MessageBox.Show("Please enter valid age field");
                return;
            }
                try
                {
                    this.nameOnTicket = this.firstName + " " + this.lastName;
                    newAge = int.Parse(age);
                    ConfirmationDetails_SeatSelectionView confirmationDetails_SeatSelectionView = new ConfirmationDetails_SeatSelectionView(this.availableFlightInformation, this.nameOnTicket);
                    confirmationDetails_SeatSelectionView.ShowDialog();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please enter a realistic age ! ");
                    return; 
                }
            }
        }
    }