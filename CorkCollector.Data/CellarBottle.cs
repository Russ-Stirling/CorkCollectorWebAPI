using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorkCollector.Data
{
    public class CellarBottle
    {
        public CellarBottle()
        {

        }

        public CellarBottle(Wine bottle, int bottleCount=1)
        {
            WineId = bottle.WineId;
            WineName = bottle.WineName;
            WineType = bottle.WineType;
            WineYear = bottle.BottlingYear;
            BottleCount = bottleCount;
            PurchaseDate = DateTimeOffset.Now;
            Finished = false;
        }

        public void IncreaseBottleCount()
        {
            BottleCount++;
        }

        public void FinishBottle()
        {
            BottleCount--;
        }
        public string WineId { get; set; }
        public string WineName { get; set; }
        public string WineType { get; set; }
        public int WineYear { get; set; }
        public int BottleCount { get; set; }
        public DateTimeOffset PurchaseDate { get; set; }
        public bool Finished { get; set; }
    }
}
