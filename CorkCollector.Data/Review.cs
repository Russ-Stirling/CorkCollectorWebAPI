﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorkCollector.Data
{
    public class Review
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }

    }
}
