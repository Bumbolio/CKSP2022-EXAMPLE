using ClosingDay.Repository.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClosingDay.Repository.Models
{
    public class Calendar {
        public Calendar(DateTime closingDay, double allowedSnowDays) {
            ClosingDay = closingDay;
            AllowedSnowDays = allowedSnowDays;
            SnowDays = new List<SnowDay>();
        }
        public DateTime ClosingDay { get; set; }
        public List<SnowDay> SnowDays { get; set; }
        public double AllowedSnowDays { get; set; }

        public DateTime GetFinalClosingDay() {
            double snowDays = 0;


            //using linq to sum snow days
            snowDays = SnowDays.Sum(snowDay => { 
                return snowDay.IsFullDay ? 1 : 0.5;
            });

            double snowDaysDiff = snowDays - AllowedSnowDays;

            if (snowDaysDiff > 0)
            {
                return ClosingDay.AddBusinessDays((int)snowDaysDiff);
            }
            else 
            {
                return ClosingDay;
            }
        }
    }
}
