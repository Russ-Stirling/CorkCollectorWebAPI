using System;

namespace CorkCollector.Data
{
    public class CheckIn
    {
        public string WineryId { get; set; }
        public string WineryName { get; set; }
        public DateTimeOffset VisitTime { get; set; }
    }
}
