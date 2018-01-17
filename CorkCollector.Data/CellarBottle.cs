using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorkCollector.Data
{
    public class CellarBottle
    {
        public Guid WineId { get; set; }
        public string WineName { get; set; }
        public string WineType { get; set; }
        public int WineYear { get; set; }
        public DateTimeOffset PurchaseDate { get; set; }
        public bool Finished { get; set; }
    }
}
