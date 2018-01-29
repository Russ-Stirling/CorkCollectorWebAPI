using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CorkCollector.Data;
using CorkCollector.Web.API.Controllers;
using Raven.Client.Documents;
using Xunit;

namespace CorkCollector.Test
{
    public class TestCaseBase
    {
        private readonly X509Certificate2 Cert;
        private readonly DocumentStore RavenStore;

        public TestCaseBase()
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Cert = new X509Certificate2();

            //TODO: move pfx location and password to app config
            Cert.Import("D:\\CorkCollector\\DBServer\\CorkCollectorTest.pfx", "Cork123", X509KeyStorageFlags.DefaultKeySet);

            RavenStore = new DocumentStore
            {
                Database = "CorkCollector",
                Urls = new string[] { "https://a.corkcollector.dbs.local.ravendb.net:8080" },
                Certificate = Cert
            };

            RavenStore.Initialize();

            
        }
        [Fact]
        public void WineryGetAll()
        {
            WineryController wineryController = new WineryController()
            {
                ravenStore = RavenStore
            };
            var wineries = wineryController.Get();

            Assert.NotEmpty(wineries);
        }
        [Fact]
        public void WineryGetOne()
        {
            WineryController wineryController = new WineryController()
            {
                ravenStore = RavenStore
            };
            var winery = wineryController.Get("wineries/1-A");

            Assert.NotNull(winery);
        }
        [Fact]
        public void WineryGetOneDoesntExist()
        {
            WineryController wineryController = new WineryController()
            {
                ravenStore = RavenStore
            };

            var winery = wineryController.Get("wesaklghp9a8y78eorqu");

            Assert.Null(winery);
        }

        [Fact]
        public void WineryPostReview()
        {
            WineryController wineryController = new WineryController()
            {
                ravenStore = RavenStore
            };

            Review testReview = new Review()
            {
                Rating = 4,
                Text = "This is a test review",
                UserId = "1-A",
                UserName = "TestReviewer"
            };

            var response = wineryController.Post(testReview, "wineries/1-A");

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var winery = wineryController.Get("wineries/1-A");

            var review = winery.Reviews.FirstOrDefault(x => x.UserId == "1-A");

            Assert.Equal(review.UserId, testReview.UserId);
            Assert.Equal(review.UserName, testReview.UserName);
            Assert.Equal(review.Rating, testReview.Rating);
            Assert.Equal(review.Text, testReview.Text);
        }

        [Fact]
        public void WineGetAll()
        {
            WineController wineController = new WineController()
            {
                ravenStore = RavenStore
            };

            var wines  = wineController.Get();

            Assert.NotEmpty(wines);
        }
        [Fact]
        public void WineGetOne()
        {
            WineController wineController = new WineController()
            {
                ravenStore = RavenStore
            };

            var wine = wineController.Get("wines/1-A");

            Assert.NotNull(wine);
        }
        [Fact]
        public void WineGetOneDoesntExist()
        {
            WineController wineController = new WineController()
            {
                ravenStore = RavenStore
            };

            var wine = wineController.Get("wesaklghp9a8y78eorqu");

            Assert.Null(wine);
        }

        [Fact]
        public void WinePostReview()
        {
            WineController wineController = new WineController()
            {
                ravenStore = RavenStore
            };

            Review testReview = new Review()
            {
                Rating = 4,
                Text = "This is a test review",
                UserId = "1-A",
                UserName = "TestReviewer"
            };

            var response = wineController.Post(testReview, "wines/1-A");
            
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var wine = wineController.Get("wines/1-A");

            var review = wine.Reviews.FirstOrDefault(x => x.UserId == "1-A");

            Assert.Equal(review.UserId, testReview.UserId);
            Assert.Equal(review.UserName, testReview.UserName);
            Assert.Equal(review.Rating, testReview.Rating);
            Assert.Equal(review.Text, testReview.Text);
        }
    }

}
