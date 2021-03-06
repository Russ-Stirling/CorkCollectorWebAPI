﻿using System.Collections.Generic;

namespace CorkCollector.Data
{
    public class Winery
    {
        public string WineryId { get; set; }
        public List<Review> Reviews { get; set; }
        public bool HasMenu { get; set; }
        public double TastingPrice { get; set; }
        public double? Rating { get; set; }
        public string Address { get; set; }
        public float CheckInRadius { get; set; }
        public string[] HoursOfOperation { get; set; }
        public string PhoneNumber { get; set; }
        public string WineryName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
