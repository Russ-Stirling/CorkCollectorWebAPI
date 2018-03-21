using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public void WineryPostCheckin()
        {
            CheckInSubmitModel checkin = new CheckInSubmitModel()
            {
                Latitude = -73,
                Longitude = 43,
                UserId = "UserProfiles/917a96d2-0a28-4c2b-bb1c-ca7bad5d8a38",
                WineryId = "wineries/2c7b2ef5-ffb2-408c-b8a1-eb44553cd7d2"
            };

            var response = _wineryController.Post(checkin);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        }


        [Fact]
        public void WineryPostReview()
        {

            ReviewSubmitModel testReview = new ReviewSubmitModel()
            {
                SubjectId = "wineries/2c7b2ef5-ffb2-408c-b8a1-eb44553cd7d2",
                Rating = 3,
                Text = "This is a medium test review for me",
                UserId = "4-A",
                UserName = "TestReviewer4"
            };

            var response = _wineryController.Post(testReview);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var winery = _wineryController.Get("wineries/2c7b2ef5-ffb2-408c-b8a1-eb44553cd7d2");

            var review = winery.Reviews.FirstOrDefault(x => x.UserId == "4-A");

            Assert.Equal(review.UserId, testReview.UserId);
            Assert.Equal(review.UserName, testReview.UserName);
            Assert.Equal(review.Rating, testReview.Rating);
            Assert.Equal(review.Text, testReview.Text);
        }
    }
}
