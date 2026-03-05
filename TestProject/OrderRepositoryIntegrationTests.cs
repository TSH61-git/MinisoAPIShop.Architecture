using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository;

namespace TestProject
{
    public class OrderRepositoryIntegrationTests : IDisposable
    {
        private readonly DatabaseFixture _fixture;
        private readonly MyWebApiShopContext _context;
        private readonly OrderRepository _repository;

        public OrderRepositoryIntegrationTests()
        {
            _fixture = new DatabaseFixture();
            _context = _fixture.Context;
            _repository = new OrderRepository(_context);
        }

        public void Dispose()
        {
            _fixture.Dispose();
        }

        [Fact]
        public async Task AddOrder_ShouldPersistOrder()
        {
            // Arrange
            var user = new User {FirstName = "TestUser", Email = "testuser@example.com", Password = "12341234"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var order = new Order
            {
                OrderItems = new List<OrderItem>(),
                OrderSum = 250,
                UserId = user.UserId
            };

            // Act
            var result = await _repository.AddOrder(order);
            var dbOrder = await _context.Orders.FindAsync(order.OrderId);

            // Assert
            Assert.NotNull(dbOrder);
            Assert.Equal(order.OrderId, dbOrder.OrderId);
            Assert.Equal(order.OrderSum, dbOrder.OrderSum);
            Assert.Equal(order.UserId, dbOrder.UserId);
            Assert.Equal(order, result);
        }

        [Fact]
        public async Task AddMultipleOrders_ShouldPersistAll()
        {
            // Arrange
            var user1 = new User
            {
                FirstName = "TestUser",
                Email = "test1@example.com",
                Password = "12341234"
            };
            var user2 = new User
            {
                FirstName = "anotherUser",
                Email = "test2@example.com",
                Password = "12123456"
            };
            _context.Users.Add(user1);
            _context.Users.Add(user2);
            await _context.SaveChangesAsync();

            var order1 = new Order { OrderItems = new List<OrderItem>(), OrderSum = 100, UserId = user1.UserId };
            var order2 = new Order { OrderItems = new List<OrderItem>(), OrderSum = 150, UserId = user2.UserId };

            // Act
            await _repository.AddOrder(order1);
            await _repository.AddOrder(order2);

            var allOrders = await _context.Orders.ToListAsync();

            // Assert
            Assert.Contains(allOrders, o => o.OrderId == user1.UserId && o.OrderSum == 100);
            Assert.Contains(allOrders, o => o.OrderId == user2.UserId && o.OrderSum == 150);
            Assert.Equal(2, allOrders.Count);
        }
    }
}
