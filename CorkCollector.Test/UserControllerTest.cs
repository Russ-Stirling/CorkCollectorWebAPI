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
    public class UserControllerTest: TestCaseBase
    {
        private readonly UserController _userController;

        public UserControllerTest() : base()
        {
            _userController = new UserController(RavenStore);
        }
        
        [Fact]
        public void UsersGetAll()
        {

            var users = _userController.Get();

            Assert.NotEmpty(users);
        }
        [Fact]
        public void UserGetOne()
        {

            var user = _userController.Get("UserProfiles/1-A");

            Assert.NotNull(user);
        }
        [Fact]
        public void UserGetOneDoesntExist()
        {

            var user = _userController.Get("wesaklghp9a8y78eorqu");

            Assert.Null(user);
        }

        [Fact]
        public void UserAddFriend()
        {

            string friendId = "UserProfiles/2-A";

            var response = _userController.Post("UserProfiles/1-A", friendId);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var user = _userController.Get("UserProfiles/1-A");

            var friend = user.Friends.FirstOrDefault(x => x == friendId);

            Assert.NotNull(friend);
        }

        [Fact]
        public void UserAddNew()
        {

            Guid userId = Guid.NewGuid();
            string newUsername = string.Format("Test{0}", userId);


            UserProfile testUser = new UserProfile()
            {
                Friends = new List<string>(),
                _id = userId,
                CellarBottles = new List<CellarBottle>(),
                CheckIns = new List<string>(),
                Email = string.Format("{0}@gmail.com",newUsername),
                Password = "test",
                PersonalComments = new List<PersonalComment>(),
                Tastings = new List<string>(),
                Username = newUsername

            };

            var response = _userController.Post(testUser);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var user = _userController.Get().FirstOrDefault(x=> x.Username == newUsername);

            Assert.NotNull(user);

        }
    }
}
