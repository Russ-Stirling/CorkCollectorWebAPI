﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client.Document;

namespace CorkCollector.Utility
{
    class Program
    {
        static void Main(string[] args)
        {
            var store = new DocumentStore
            {
                ConnectionStringName = "RavenDB/CorkCollector"
            };

            
        }
    }
}
