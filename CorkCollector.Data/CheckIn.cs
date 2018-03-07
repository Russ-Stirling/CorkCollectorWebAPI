using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorkCollector.Data
{
    public class CheckIn
    {
        public string WineryId { get; set; }
        public string WineryName { get; set; }
        public DateTimeOffset VisitTime { get; set; }
    }
}
