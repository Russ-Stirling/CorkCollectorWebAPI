using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorkCollector.Data
{
    public class EditPersonalCommentSubmitModel
    {
        public string UserId { get; set; }
        public string WineId { get; set; }
        public string NewComment { get; set; }
    }
}
