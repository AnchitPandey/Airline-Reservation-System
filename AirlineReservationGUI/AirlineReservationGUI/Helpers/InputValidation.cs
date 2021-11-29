using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AirlineReservationGUI.Helpers
{
   public static class InputValidation
    {
        public static bool verifyInputDate (DatePicker dt)
        {
            string dateValue = dt.SelectedDate.GetValueOrDefault().ToShortDateString();
            if (dateValue == null)
                return false;
            return true;
        }
        
        public static bool verifyEmail (string emailInput)
        {
            String pattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
+ "@"
+ @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(emailInput);
            return match.Success;
        }

    }
}
