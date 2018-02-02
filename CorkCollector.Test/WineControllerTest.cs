using CorkCollector.Web.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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

            Review testReview = new Review()
            {
                Rating = 4,
                Text = "This is a test review",
                UserId = "1-A",
                UserName = "TestReviewer"
            };

            var response = _wineController.Post(testReview, "wines/1-A");

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var wine = _wineController.Get("wines/1-A");

            var review = wine.Reviews.FirstOrDefault(x => x.UserId == "1-A");

            Assert.Equal(review.UserId, testReview.UserId);
            Assert.Equal(review.UserName, testReview.UserName);
            Assert.Equal(review.Rating, testReview.Rating);
            Assert.Equal(review.Text, testReview.Text);
        }
    }
}
