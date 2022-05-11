using ClosingDay.Repository.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ClosingDayApp.Test
{
    [TestClass]
    public class AddBusinessDaysUnitTests
    {
        private static DateTime friday = DateTime.Parse("2022-05-13");
        private static DateTime monday = DateTime.Parse("2022-05-16");
        private static DateTime tuesday = DateTime.Parse("2022-05-17");

        [TestMethod]
        public void AddTwoDaysIfFriday()
        {
            var twoBusinessDays = friday.AddBusinessDays(2);

            Assert.AreEqual(tuesday, twoBusinessDays);
        }

        [TestMethod]
        public void AddOneDayIfFriday()
        {
            var oneBusinessDay = friday.AddBusinessDays(1);

            Assert.AreEqual(monday, oneBusinessDay);
        }
    }
}
