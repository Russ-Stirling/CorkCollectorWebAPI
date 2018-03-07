using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorkCollector.Data
{
    public class TastingListItem
    {
        public TastingListItem(Wine basedWine, string wineryName)
        {
            WineId = basedWine.WineId;
            WineryName = wineryName;
            WineName = basedWine.WineName;
            WineType = basedWine.WineType;
            OnTastingMenu = basedWine.OnTastingMenu;
            WinePrice = basedWine.WinePrice;
            BottlingYear = basedWine.BottlingYear;
            Description = basedWine.Description;
        }
        public string WineId { get; set; }
        public string WineryName { get; set; }
        public string WineName { get; set; }
        public string WineType { get; set; }
        public bool OnTastingMenu { get; set; }
        public double WinePrice { get; set; }
        public int BottlingYear { get; set; }
        public string Description { get; set; }
    }
}
