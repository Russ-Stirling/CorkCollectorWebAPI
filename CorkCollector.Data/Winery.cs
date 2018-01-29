using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorkCollector.Data
{
    public class Winery
    {
        //public string _id { get; set; }
        public List<Review> Reviews { get; set; }
        public bool HasMenu { get; set; }
        public double TastingPrice { get; set; }
        public float Rating { get; set; }
        public string Address { get; set; }
        public float CheckInRadius { get; set; }
        public string[] HoursOfOperation { get; set; }
        public string PhoneNumber { get; set; }
        public string WineryName { get; set; }
    }
}
