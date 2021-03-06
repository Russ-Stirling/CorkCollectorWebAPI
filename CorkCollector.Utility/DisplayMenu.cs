﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CorkCollector.Data;
using CsvHelper;
using Raven.Client.Documents;
using System.Security.Cryptography.X509Certificates;

namespace CorkCollector.Utility
{
    class DisplayMenu
    {
        public DisplayMenu()
        {
            
        }

        public bool run()
        {
            bool done = false;
            string subMenu = GetMainMenu();
            string operation = string.Empty;
            switch (subMenu)
            {
                case "1":
                    operation = GetBatchDataMenu();
                    break;
            }
            switch (string.Format("{0}-{1}", subMenu, operation))
            {
                default:
                    done = CheckIfDone();
                    break;
            }
            return done;
        }

        private bool CheckIfDone()
        {
            string[] options = new string[2] { "1", "2" };
            bool correctInput = false;
            string choice = null;

            while (!correctInput)
            {
                Console.WriteLine("1. Return to Main Menu");
                Console.WriteLine("2. Exit Application");

                choice = Console.ReadLine();

                if (options.Contains(choice))
                    correctInput = true;
                else
                    Console.WriteLine("Invalid input. Please select one of the menu options.");
            }

            return choice == "1" ? true : false;

        }

        private string GetMainMenu()
        {
            string[] options = new string[1] { "1" };
            bool correctInput = false;
            string choice = null;

            while (!correctInput)
            {
                Console.WriteLine("1. Batch Data Operations");

                choice = Console.ReadLine();

                if (options.Contains(choice))
                    correctInput = true;
                else
                    Console.WriteLine("Invalid input. Please select one of the menu options.");
            }

            return choice;
        }

        private string GetBatchDataMenu()
        {
            string[] options = new string[3] { "1", "2", "3" };
            Console.WriteLine("1. Import winery data");
            Console.WriteLine("2. Import wine data");
            Console.WriteLine("3. Delete all reviews");
            bool correctInput = false;
            string userSelection = string.Empty;

            while (!correctInput)
            {
                userSelection = Console.ReadLine();

                if (options.Contains(userSelection))
                    correctInput = true;
                else
                    Console.WriteLine("Invalid input. Please select one of the menu options.");
            }

            if (userSelection == "3")
            {
                RemoveReviews();
            }

            else
            {
                Console.WriteLine("Input file path to csv with data");
                bool pathValid = false;
                string path = string.Empty;

                while (!pathValid)
                {
                    path = Console.ReadLine();

                    if (File.Exists(path))
                        pathValid = true;
                    else
                    {
                        Console.WriteLine("File does not exist, please use a valid file");
                    }
                }

                if (userSelection.Equals("1"))
                    AddWineries(path);
                if (userSelection.Equals("2"))
                    AddWines(path);
            }
            return string.Empty;
        }

        private void RemoveReviews()
        {
            var Cert = new X509Certificate2();
            Cert.Import("D:\\CorkCollector\\DBServer\\CorkCollectorTest.pfx", "Cork123", X509KeyStorageFlags.DefaultKeySet);
            var store = new DocumentStore
            {
                Database = "CorkCollector",
                Urls = new string[] { "https://a.corkcollector.dbs.local.ravendb.net:8080" },
                Certificate = Cert

            };

            store.Initialize();

            var x = store.OpenSession();

            List<Winery> wineries = x.Query<Winery>().ToList();
            foreach (var winery in wineries)
            {
                winery.Reviews = new List<Review>();
            }
            List<Wine> wines = x.Query<Wine>().ToList();
            foreach (var wine in wines)
            {
                wine.Reviews = new List<Review>();
            }
            x.SaveChanges();
        }

        private void AddWineries(string path)
        {
            List<Winery> records = new List<Winery>();

            using (TextReader fileReader = File.OpenText(path))
            {
                var csv = new CsvReader(fileReader);
                records = csv.GetRecords<Winery>().ToList();
            }

            var Cert = new X509Certificate2();
            Cert.Import("D:\\CorkCollector\\DBServer\\CorkCollectorTest.pfx", "Cork123", X509KeyStorageFlags.DefaultKeySet);
            var store = new DocumentStore
            {
                Database = "CorkCollector",
                Urls = new string[] { "https://a.corkcollector.dbs.local.ravendb.net:8080" },
                Certificate = Cert

            };

            store.Initialize();

            var x = store.OpenSession();

            foreach (var record in records)
            {
                string theID = Guid.NewGuid().ToString();
                record.Reviews = new List<Review>();
                record.WineryId = string.Format("wineries/{0}", theID);
                x.Store(record, string.Format("wineries/{0}", theID));
            }

            x.SaveChanges();
        }

        private void AddWines(string path)
        {
            List<Wine> records = new List<Wine>();

            using (TextReader fileReader = File.OpenText(path))
            {
                var csv = new CsvReader(fileReader);
                records = csv.GetRecords<Wine>().ToList();
            }

            var Cert = new X509Certificate2();
            Cert.Import("D:\\CorkCollector\\DBServer\\CorkCollectorTest.pfx", "Cork123", X509KeyStorageFlags.DefaultKeySet);
            var store = new DocumentStore
            {
                Database = "CorkCollector",
                Urls = new string[] { "https://a.corkcollector.dbs.local.ravendb.net:8080" },
                Certificate = Cert

            };

            store.Initialize();

            var x = store.OpenSession();

            foreach (var record in records)
            {
                string theID = Guid.NewGuid().ToString();
                record.Reviews = new List<Review>();
                record.WineId = string.Format("wines/{0}", theID);
                x.Store(record, string.Format("wines/{0}", theID));
            }

            x.SaveChanges();
        }
    }
}
