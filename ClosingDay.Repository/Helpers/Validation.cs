using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClosingDay.Repository.Helpers
{
    public static class Validation
    {
        public static DateTime GetDateUserInfoFromInput(string userInput)
        {
            var success = DateTime.TryParse(userInput, out DateTime userDate);

            if (success)
            {
                return userDate;
            }
            else
            {
                throw new FormatException("Could not parse user input");
            }
        }
    }
}
