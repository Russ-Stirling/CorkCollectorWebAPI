using CorkCollector.Web.API.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using CorkCollector.Data;
using Xunit;

namespace CorkCollector.Test
{
    public class WineControllerTest : TestCaseBase
    {
        private readonly WineController _wineController;

        public WineControllerTest() : base()
        {
            _wineController = new WineController(RavenStore);
        }
        [Fact]
        public void WineGetAll()
        {

            var wines = _wineController.Get();

            Assert.NotEmpty(wines);
        }
        [Fact]
        public void WineGetOne()
        {

            var wine = _wineController.Get("wines/1-A");

            Assert.NotNull(wine);
        }
        [Fact]
        public void WineGetOneDoesntExist()
        {

            var wine = _wineController.Get("wesaklghp9a8y78eorqu");

            Assert.Null(wine);
        }

        [Fact]
        public void WinePostReview()
        {

            ReviewSubmitModel testReview = new ReviewSubmitModel()
            {

                Rating = 4,
                Text = "This is a test review",
                UserId = "1-A",
                UserName = "TestReviewer"
            };

            var response = _wineController.Post(testReview);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var wine = _wineController.Get("wines/1-A");

            var review = wine.Reviews.FirstOrDefault(x => x.UserId == "1-A");

            Assert.Equal(review.UserId, testReview.UserId);
            Assert.Equal(review.UserName, testReview.UserName);
            Assert.Equal(review.Rating, testReview.Rating);
            Assert.Equal(review.Text, testReview.Text);
        }

        [Fact]
        public void WineryPostWine()
        {

            Wine testWine = new Wine()
            {
                Reviews = new List<Review>(),
                WineId = string.Empty,
                BottlingYear = 1977,
                OnTastingMenu = true,
                WineName = "Shiraz",
                WinePrice = 30.75,
                WineryId = "wineries/1-A",
                WineType = "Red"
            };

            var response = _wineController.Post(testWine);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var wine = _wineController.Get("wines/1-A");

            Assert.NotNull(wine);
        }
    }
}
