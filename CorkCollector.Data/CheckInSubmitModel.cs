using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorkCollector.Data
{
    public class CheckInSubmitModel
    {
        public string WineryId { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }
        public string UserId { get; set; }

    }
}
