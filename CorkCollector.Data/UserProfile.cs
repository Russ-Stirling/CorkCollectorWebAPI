using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorkCollector.Data
{
    public class UserProfile
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string DateJoined { get; set; }
        public List<string> Friends { get; set; }
        public List<CheckIn> CheckIns { get; set; }
        public List<string> Tastings { get; set; }
        public List<CellarBottle> CellarBottles { get; set; }
        
        

    }
}
