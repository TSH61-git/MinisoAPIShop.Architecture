using Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Repository;

namespace TestProject
{
    public class UserRepositoryUnitTesting
    {
        
        //2. GetUserById

        //ID קיים → מוחזר משתמש נכון

        //ID לא קיים → מוחזר null

        //ID = 0 → null


        //5. AddUser / CreateUser

        //משתמש תקין → נוסף בהצלחה

        //משתמש null → Exception

        //משתמש עם שדות חסרים → Exception

        //משתמש כפול(Email / Username) → Exception / false


        //6. UpdateUser

        //משתמש קיים → עודכן

        //משתמש לא קיים → Exception / false

        //null → Exception

        //עדכון חלקי


        //8. Login / Authenticate(אם קיים)

        //פרטי התחברות נכונים → הצלחה

        //סיסמה שגויה → null

        //משתמש לא קיים → null

        //סיסמה ריקה

        //null כקלט



        [Fact]
        public async Task GetUserById_UserExists_ReturnsUser()
        {
            // Arrange
            var user = new User { UserId = 1, Email = "user1@test.com", Password = "password1" };
            var users = new List<User> { user };

            var mockContext = new Mock<MyWebApiShopContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var repository = new UserRepository(mockContext.Object);

            // Act
            var result = await repository.GetUserById(user.UserId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task GetUserById_UserNotExists_ReturnsNull()
        {
            // Arrange
            var users = new List<User>();

            var mockContext = new Mock<MyWebApiShopContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var repository = new UserRepository(mockContext.Object);

            // Act
            var result = await repository.GetUserById(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddUser_ValidUser_AddsUser()
        {
            // Arrange
            var users = new List<User>();

            var mockContext = new Mock<MyWebApiShopContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            mockContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

            var repository = new UserRepository(mockContext.Object);

            var user = new User { Email = "new@test.com" };

            // Act
            var result = await repository.AddUser(user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task LoginUser_ValidCredentials_ReturnsUser()
        {
            // Arrange
            var user = new User
            {
                Email = "myemail@gmail.com",
                Password = "1234567"
            };

            var users = new List<User> { user };

            var mockContext = new Mock<MyWebApiShopContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var repository = new UserRepository(mockContext.Object);

            // Act
            var result = await repository.LoginUser(user);

            // Assert
            Assert.Equal(user, result);
        }


        [Fact]
        public async Task LoginUser_WrongPassword_ReturnsNull()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Email = "myemail@gmail.com", Password = "1234567" }
            };

            var mockContext = new Mock<MyWebApiShopContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var repository = new UserRepository(mockContext.Object);

            var loginUser = new User
            {
                Email = "myemail@gmail.com",
                Password = "wrong"
            };

            // Act
            var result = await repository.LoginUser(loginUser);

            // Assert
            Assert.Null(result);
        }


        [Fact]
        public async Task LoginUser_UserNotExists_ReturnsNull()
        {
            // Arrange
            var users = new List<User>();

            var mockContext = new Mock<MyWebApiShopContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var repository = new UserRepository(mockContext.Object);

            // Act
            var result = await repository.LoginUser(new User
            {
                Email = "no@email.com",
                Password = "123"
            });

            // Assert
            Assert.Null(result);
        }


        [Fact]
        public async Task UpdateUser_UserExists_UpdatesUser()
        {
            // Arrange
            var user = new User { UserId = 1, Email = "old@test.com" };
            var users = new List<User> { user };

            var mockContext = new Mock<MyWebApiShopContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            mockContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

            var repository = new UserRepository(mockContext.Object);

            var updatedUser = new User { UserId = 1, Email = "new@test.com" };

            // Act
            await repository.UpdateUser(1, updatedUser);

            // Assert
            Assert.Equal("new@test.com", user.Email);
        }


        [Fact]
        public async Task UpdateUser_UserNotExists_DoesNothing()
        {
            // Arrange
            var users = new List<User>();

            var mockContext = new Mock<MyWebApiShopContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var repository = new UserRepository(mockContext.Object);

            // Act
            await repository.UpdateUser(99, new User { Email = "x@test.com" });

            // Assert
            mockContext.Verify(x => x.SaveChangesAsync(default), Times.Never);
        }


    }
}
