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
        [Fact]
        public void WineGetAll()
        {
            WineController wineController = new WineController(RavenStore);

            var wines = wineController.Get();

            Assert.NotEmpty(wines);
        }
        [Fact]
        public void WineGetOne()
        {
            WineController wineController = new WineController(RavenStore);

            var wine = wineController.Get("wines/1-A");

            Assert.NotNull(wine);
        }
        [Fact]
        public void WineGetOneDoesntExist()
        {
            WineController wineController = new WineController(RavenStore);

            var wine = wineController.Get("wesaklghp9a8y78eorqu");

            Assert.Null(wine);
        }

        [Fact]
        public void WinePostReview()
        {
            WineController wineController = new WineController(RavenStore);

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
