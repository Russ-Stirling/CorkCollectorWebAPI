using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CorkCollector.Data;
using CorkCollector.Web.API.Controllers;
using Xunit;

namespace CorkCollector.Test
{
    public class WineryControllerTest: TestCaseBase
    {
        private readonly WineryController _wineryController;

        public WineryControllerTest() : base()
        {
            _wineryController = new WineryController(RavenStore);
        }
        [Fact]
        public void WineryGetAll()
        {
            var wineries = _wineryController.Get();

            Assert.NotEmpty(wineries);
        }
        [Fact]
        public void WineryGetOne()
        {
            var winery = _wineryController.Get("wineries/1-A");

            Assert.NotNull(winery);
        }
        [Fact]
        public void WineryGetOneDoesntExist()
        {

            var winery = _wineryController.Get("wesaklghp9a8y78eorqu");

            Assert.Null(winery);
        }

        [Fact]
        public void WineryPostWinery()
        {

            Winery testWinery = new Winery()
            {
                HasMenu = true,
                TastingPrice = 50,
                Rating = 4,
                Address = "290 John St E, Niagara-on-the-Lake, ON L0S 1J0",
                CheckInRadius = 250,
                HoursOfOperation = new string[] {"9-20", "9-20", "9-20", "9-20", "9-20", "11-19", "11-19"},
                PhoneNumber = "519-872-1556",
                WineryName = "Peller Estates Winery and Restaurant",
                Latitude = 43.238662,
                Longitude = -79.067035,
                Reviews = new List<Review>()
            };

            var response = _wineryController.Post(testWinery);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var winery = _wineryController.Get("wineries/1-A");

            Assert.NotNull(winery);
        }


        [Fact]
        public void WineryPostReview()
        {

            Review testReview = new Review()
            {
                Rating = 4,
                Text = "This is a test review",
                UserId = "1-A",
                UserName = "TestReviewer"
            };

            var response = _wineryController.Post(testReview, "wineries/1-A");

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var winery = _wineryController.Get("wineries/1-A");

            var review = winery.Reviews.FirstOrDefault(x => x.UserId == "1-A");

            Assert.Equal(review.UserId, testReview.UserId);
            Assert.Equal(review.UserName, testReview.UserName);
            Assert.Equal(review.Rating, testReview.Rating);
            Assert.Equal(review.Text, testReview.Text);
        }
    }
}
