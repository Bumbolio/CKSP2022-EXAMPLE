using ClosingDay.Repository.Models;
using ClosingDay.Repository.Helpers;
using System;
using System.Text.RegularExpressions;

namespace ClosingDayApp
{
    partial class Program
    {
        static void Main(string[] args)
        {
            var calendar = new Calendar(DateTime.Parse("2022-05-26"), 5);
            string userInput;


            do
            {
                Console.WriteLine("Please enter a snow day (yyyy-mm-dd), or type exit to calculate the closing day");
                userInput = Console.ReadLine();

                DateTime date = new DateTime();
                bool isDateValid = true;

                try
                {
                    date = Validation.GetDateUserInfoFromInput(userInput);
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Please enter a valid date in yyyy-mm-dd format");
                    isDateValid = false;
                }

                bool isWeekDay = false;

                if (userInput != "exit")
                {
                    date = DateTime.Parse(userInput);
                    isWeekDay = !(date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday);
                }

                if (!isWeekDay)
                {
                    Console.WriteLine("Please enter a week day");
                }

                if (userInput != "exit" && isDateValid && isWeekDay)
                {
                    

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
    }
}
