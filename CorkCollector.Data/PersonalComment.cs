using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorkCollector.Data
{
    public class PersonalComment
    {
        public string Text { get; set; }
        public Guid WineId { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}
