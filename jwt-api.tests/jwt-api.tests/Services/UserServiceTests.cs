using Xunit;
using jwtApi.Services;
using jwtApi.Entities;
using Moq;

namespace jwt_api.Tests
{
    public class UserServiceTests
    {
        private User goodUser;
        private User badUser;

        public UserServiceTests()
        {
            goodUser = new User()
            {
                Id = 1,
                FirstName = "local",
                LastName = "only",
                Username = "lo",
                Password = "lo1",
                Token = ""
            };
            badUser = new User()
            {
                Id = 2,
                FirstName = "bad",
                LastName = "userdata",
                Username = "baduser",
                Password = "0000",
                Token = ""
            };
        }
        [Fact]
        public void CanAuthenticate()
        {
            var validUserResponse = new User()
            {
                FirstName = goodUser.FirstName,
                LastName = goodUser.LastName,
                Username = goodUser.Username,
                Password = null,
                Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIiLCJuYmYiOjE1ODY2MjE0NDAsImV4cCI6MTU4NzIyNjI0MCwiaWF0IjoxNTg2NjIxNDQwfQ.COFB8xRMveTHKAiT_xB6x520Qo3xWPilY4RxqeTx-hY"
            };

            var userService = new Mock<IUserService>();
            userService.Setup(x => x.Authenticate(goodUser.Username, goodUser.Password)).Returns(validUserResponse);

            var authedUser = userService.Object.Authenticate(goodUser.Username, goodUser.Password);
            Assert.True(authedUser.Password == null);
            Assert.True(authedUser.Token.Length > 0);
        }

        [Fact]
        public void RejectBadUserNameOrPassword()
        {
            var badResponse = new User();
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.Authenticate(badUser.Username, badUser.Password)).Returns(badResponse);

            var unauthedUser = userService.Object.Authenticate(badUser.Username, badUser.Password);
            Assert.True(unauthedUser.Password == null);
            Assert.True(unauthedUser.Token == null);
        }
    }
}
