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
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        Intel_AirlineReservationService.Intel_AirlineReservationServiceClient client;

        public LoginView()
        {
            InitializeComponent();
        }

        private void displayRegistrationWindow(object sender, MouseButtonEventArgs e)
        {
            RegistrationView registrationView = new RegistrationView();
            registrationView.ShowDialog();
        }


        public bool Verify (string email, string password)
        {
            String pattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
+ "@"
+ @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";

            Regex regex = new Regex(pattern);
            Match match = regex.Match(email);
            return email.Length != 0 && password.Length != 0 && match.Success;   
        }

        private void PerformLogin(object sender, RoutedEventArgs e)
        {
            string email = emailInput.Text.ToString().ToLower();
            string password = passwordInput.Password.ToString();
            // Send data to WCF Service
            if (!Verify(email, password))
            {
                MessageBox.Show("Please enter all fields and verify your email address");
                return;
            }
            int passengerId = -2;
            client = new Intel_AirlineReservationService.Intel_AirlineReservationServiceClient();
            try
            {
                passengerId = client.login(email, password);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Internal Error. Please try again later");
                return;
            }

            if (passengerId == -1)
            {
                MessageBox.Show("Internal Error. Please try again later");
                return;
            }
            else if (passengerId ==-2)
            {
                MessageBox.Show("Internal Error. Please try again later");
                return;
            }
            else if (passengerId == -3)
            {
                MessageBox.Show("This email is not registered. Please register first");
                return;
            }

            // If login is successful, then go to home screen
            AvailableFlightsView availableFlightsView = new AvailableFlightsView(passengerId);
            availableFlightsView.Show();
            this.Close();
        }

     
        private void NavigateToAdminView(object sender, RoutedEventArgs e)
        {
            AdminView adminView = new AdminView();
            adminView.Show();
            this.Close();
        }
    }
}
