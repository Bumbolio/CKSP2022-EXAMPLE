using System;

namespace ClosingDay.Repository.Models
{
    public class SnowDay { 
        public bool IsFullDay { get; set; }
        public DateTime OccurredOn { get; set; }
        public double Days { get; set; }
    }
}
