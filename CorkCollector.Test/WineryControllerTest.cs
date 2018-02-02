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
        [Fact]
        public void WineryGetAll()
        {
            WineryController wineryController = new WineryController(RavenStore);
            var wineries = wineryController.Get();

            Assert.NotEmpty(wineries);
        }
        [Fact]
        public void WineryGetOne()
        {
            WineryController wineryController = new WineryController(RavenStore);
            var winery = wineryController.Get("wineries/1-A");

            Assert.NotNull(winery);
        }
        [Fact]
        public void WineryGetOneDoesntExist()
        {
            WineryController wineryController = new WineryController(RavenStore);

            var winery = wineryController.Get("wesaklghp9a8y78eorqu");

            Assert.Null(winery);
        }

        [Fact]
        public void WineryPostReview()
        {
            WineryController wineryController = new WineryController(RavenStore);

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
    }
}
