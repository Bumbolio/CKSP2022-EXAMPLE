using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ClosingDayApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var regexForDate = @"^\d{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])$";
            Regex rg = new Regex(regexForDate);

            var calendar = new Calendar(DateTime.Parse("2022-05-26"), 5);
            string userInput;


            do
            {
                Console.WriteLine("Please enter a snow day (yyyy-mm-dd), or type exit to calculate the closing day");
                userInput = Console.ReadLine();
                var isDateValid = rg.Match(userInput).Success;

                if (!isDateValid)
                {
                    Console.WriteLine("Please enter a valid date in yyyy-mm-dd format");
                }

                if (userInput != "exit" && isDateValid)
                {
                    var date = DateTime.Parse(userInput);

                    Console.WriteLine("Is it a full day or half day. Enter \"full\" or \"half\"");
                    var isFullDay = Console.ReadLine() == "full";

                    var snowDay = new SnowDay
                    {
                        OccurredOn = date,
                        IsFullDay = isFullDay
                    };

                    calendar.SnowDays.Add(snowDay);
                }

            } while (userInput != "exit");

            Console.WriteLine($"The closing day is {calendar.GetFinalClosingDay()}");
        }

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

        public class SnowDay { 
            public bool IsFullDay { get; set; }
            public DateTime OccurredOn { get; set; }
            public double Days { get; set; }
        }
    }

    //https://stackoverflow.com/a/1044821
    public static class DateTimeExtensions
    {
        public static DateTime AddBusinessDays(this DateTime date, int days)
        {
            if (days < 0)
            {
                throw new ArgumentException("days cannot be negative", "days");
            }

            if (days == 0) return date;

            if (date.DayOfWeek == DayOfWeek.Saturday)
            {
                date = date.AddDays(2);
                days -= 1;
            }
            else if (date.DayOfWeek == DayOfWeek.Sunday)
            {
                date = date.AddDays(1);
                days -= 1;
            }

            date = date.AddDays(days / 5 * 7);
            int extraDays = days % 5;

            if ((int)date.DayOfWeek + extraDays > 5)
            {
                extraDays += 2;
            }

            return date.AddDays(extraDays);

        }
    }
}
