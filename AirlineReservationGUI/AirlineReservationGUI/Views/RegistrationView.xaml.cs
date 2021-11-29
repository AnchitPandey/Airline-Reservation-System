using AirlineReservationGUI.Helpers;
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
    /// Interaction logic for RegistrationView.xaml
    /// </summary>
    /// 
    public partial class RegistrationView : Window
    {
        
        public RegistrationView()
        {
            InitializeComponent();
        }

   
        public bool Verify (string firstName, string lastName, string password, string dateOfBirth, string email)
        {
            if (dateOfBirth == null)
            {
                MessageBox.Show("Please fill the date field");
                return false;
            }
            if (firstName.Length == 0 || lastName.Length == 0 || password.Length == 0 || email.Length == 0 || dateOfBirth.Length == 0)
            {
                MessageBox.Show("please fill all the text fields");
                return false;
            }
        
            String pattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
+ "@"
+ @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(email);
            if (!match.Success)
            {
                MessageBox.Show("Please enter a valid email");
                return false;
            }
            
            try
            {
                Intel_AirlineReservationService.Intel_AirlineReservationServiceClient client =
                new Intel_AirlineReservationService.Intel_AirlineReservationServiceClient();

                // TODO: IsLoading = True
                int successCode = client.RegisterUser(email, firstName, lastName, password, dateOfBirth);
                // TODO: IsLoading = False
                if (successCode == -1)
                {
                    MessageBox.Show("This Email id was registered before. Please use a different one !");
                    return false;
                }
                else if (successCode ==-2)
                {
                    MessageBox.Show("Error in WCF Service. Please try again later");
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Unable to connect to the Service. Please Check your connection");
                return false;
                // TODO: Add Error to Log file
            }
            return true;
        }

        /// <summary>
        /// verifies user input and registers user on successful verification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterUser(object sender, RoutedEventArgs e)
        { 
            string firstName = firstNameInput.Text.ToString();
            string lastName = lastNameInput.Text.ToString();
            string email = emailInput.Text.ToString().ToLower();
            string password = passwordInput.Password.ToString();
            string dateOfBirth = dateOfBirthInput.SelectedDate.GetValueOrDefault().ToShortDateString();

            // Verify Date
            if (!InputValidation.verifyInputDate(dateOfBirthInput))
            {
                MessageBox.Show("Please provide appropriate date");
                return;
            }

            bool verified = Verify(firstName, lastName, password, dateOfBirth, email);
            if (verified)
            {         
                MessageBox.Show("Registration Successfull !");
                this.Close();
            }     
        }
    }
}