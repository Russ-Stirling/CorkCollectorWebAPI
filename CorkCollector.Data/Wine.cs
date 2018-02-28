using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorkCollector.Data
{
    public class Wine
    {
        public string WineId { get; set; }
        public string WineryId { get; set; }
        public string WineName { get; set; }
        public string WineType { get; set; }
        public bool OnTastingMenu { get; set; }
        public double WinePrice { get; set; }
        public int BottlingYear { get; set; }
        public string Description { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
