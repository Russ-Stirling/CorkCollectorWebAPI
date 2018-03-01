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
        public List<string> Friends { get; set; }
        public List<string> CheckIns { get; set; }
        public List<string> Tastings { get; set; }
        public List<PersonalComment> PersonalComments { get; set; }
        public List<CellarBottle> CellarBottles { get; set; }
        
        

    }
}
