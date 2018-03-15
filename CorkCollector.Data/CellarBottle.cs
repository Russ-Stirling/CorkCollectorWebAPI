using System;

namespace CorkCollector.Data
{
    public class CellarBottle
    {
        public CellarBottle()
        {

        }

        public CellarBottle(Wine bottle, string wineryName, int bottleCount=1)
        {
            WineId = bottle.WineId;
            WineName = bottle.WineName;
            WineryName = wineryName;
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

        public void IncreaseBottleCount(int incrAmnt)
        {
            BottleCount+= incrAmnt;
        }

        public void FinishBottle()
        {
            BottleCount--;
        }
        public string WineId { get; set; }
        public string WineName { get; set; }
        public string WineType { get; set; }
        public string WineryName { get; set; }
        public int WineYear { get; set; }
        public int BottleCount { get; set; }
        public DateTimeOffset PurchaseDate { get; set; }
        public bool Finished { get; set; }
        public string PersonalComment { get; set; }
    }
}
