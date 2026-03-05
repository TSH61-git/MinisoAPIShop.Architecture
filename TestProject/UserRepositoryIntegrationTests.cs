using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository;
using TestProject;
using Xunit;



public class UserRepositoryIntegrationTests : IDisposable
{
    private readonly DatabaseFixture _fixture;
    private readonly MyWebApiShopContext _context;
    private readonly UserRepository _repository;

    public UserRepositoryIntegrationTests()
    {
        _fixture = new DatabaseFixture();
        _context = _fixture.Context;
        _repository = new UserRepository(_context);
    }

    public void Dispose()
    {
        _fixture.Dispose();
    }

    [Fact]
    public async Task GetUserById_UserExists_ReturnsUser()
    {
        // Arrange
        var user = new User {Email = "user@test.com", Password = "password1" };


        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetUserById(user.UserId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("user@test.com", result.Email);
    }

    [Fact]
    public async Task GetUserById_UserNotExists_ReturnsNull()
    {
        // Act
        var result = await _repository.GetUserById(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddUser_ValidUser_UserSavedToDatabase()
    {
        // Arrange
        var user = new User
        {
            Email = "new@test.com",
            Password = "123"
        };

        // Act
        var result = await _repository.AddUser(user);

        // Assert
        Assert.NotEqual(0, result.UserId);
        Assert.Single(_context.Users);
    }

    [Fact]
    public async Task LoginUser_ValidCredentials_ReturnsUser()
    {
        // Arrange
        var user = new User
        {
            Email = "login@test.com",
            Password = "123"
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.LoginUser(
            new User { Email = "login@test.com", Password = "123" });

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task LoginUser_WrongPassword_ReturnsNull()
    {
        // Arrange
        _context.Users.Add(new User
        {
            Email = "login@test.com",
            Password = "123"
        });
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.LoginUser(
            new User { Email = "login@test.com", Password = "wrong" });

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateUser_UserExists_UpdatesData()
    {
        // Arrange
        var user = new User
        {
            Email = "old@test.com",
            FirstName = "Old",
            Password = "123456"
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        await _repository.UpdateUser(user.UserId, new User
        {
            Email = "new@test.com",
            FirstName = "New",
            Password = "123456"
        });

        // Assert
        var updated = await _context.Users.FirstAsync();
        Assert.Equal("new@test.com", updated.Email);
        Assert.Equal("New", updated.FirstName);
    }

    [Fact]
    public async Task UpdateUser_UserNotExists_DoesNothing()
    {
        // Act
        await _repository.UpdateUser(999, new User
        {
            Email = "x@test.com"
        });

        // Assert
        Assert.Empty(_context.Users);
    }




}
