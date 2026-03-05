using Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Repository;


namespace TestProject
{
    public class OrderRepositoryUnitTesting
    {
        [Fact]
        public async Task AddOrder_AddsOrderSuccessfully()
        {
            // Arrange
            var users = new List<User>
            {
                new User {FirstName = "TestUser", Email = "testuser@example.com", Password= "1234" }
            };
            var orders = new List<Order>();

            var mockContext = new Mock<MyWebApiShopContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            mockContext.Setup(x => x.Orders).ReturnsDbSet(orders);

            var repository = new OrderRepository(mockContext.Object);

            var order = new Order
            {
                OrderItems = new List<OrderItem>(),
                OrderSum = 200,
                UserId = users[0].UserId
            };

            // Act
            var result = await repository.AddOrder(order);

            // Assert
            Assert.Equal(order, result);
        }

        [Fact]
        public async Task AddOrder_EmptyOrderItems_ShouldThrowException()
        {
            // Arrange
            var users = new List<User>
                {
                    new User {FirstName = "TestUser", Email = "testuser@example.com", Password = "123123" }
                };
            var orders = new List<Order>();

            var mockContext = new Mock<MyWebApiShopContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            mockContext.Setup(x => x.Orders).ReturnsDbSet(orders);

            var repository = new OrderRepository(mockContext.Object);

            var order = new Order
            {
                OrderItems = new List<OrderItem>(), // ריק
                OrderSum = 0,
                UserId = users[0].UserId
            };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await repository.AddOrder(order);
            });

        }

    }
}
