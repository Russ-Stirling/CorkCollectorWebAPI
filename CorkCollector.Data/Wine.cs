using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorkCollector.Data
{
    public class Wine
    {
        public Guid _id { get; set; }
        public Guid WineryId { get; set; }
        public string WineName { get; set; }
        public string WineType { get; set; }
        public bool OnTastingMenu { get; set; }
        public double WinePrice { get; set; }
        public DateTimeOffset BottlingDate { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
