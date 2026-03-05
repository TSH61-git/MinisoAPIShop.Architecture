using DTOs;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject;
using Xunit;

public class ProductRepositoryIntegrationTests : IDisposable
{
    private readonly DatabaseFixture _fixture;
    private readonly MyWebApiShopContext _context;
    private readonly ProductRepository _repository;

    public ProductRepositoryIntegrationTests()
    {
        _fixture = new DatabaseFixture();
        _context = _fixture.Context;
        _repository = new ProductRepository(_context);
    }

    public void Dispose()
    {
        _fixture.Dispose();
    }

    [Fact]
    public async Task GetProducts_ProductsExist_ReturnsAllProducts()
    {
        // Arrange
        var product1 = new Product { ProductName = "Product1", Price = 10 };
        var product2 = new Product { ProductName = "Product2", Price = 20 };
        _context.Products.AddRange(product1, product2);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetProducts(1,10,new ProductSearchParams { });

        // Assert
        Assert.Equal(2, result.Items.Count);
        Assert.Contains(result.Items, p => p.ProductName == "Product1");
        Assert.Contains(result.Items, p => p.ProductName == "Product2");
    }

    [Fact]
    public async Task GetProducts_NoProducts_ReturnsEmptyList()
    {
        // Act
        var result = await _repository.GetProducts(1,10,new ProductSearchParams { });

        // Assert
        Assert.Empty(result.Items);
    }

}
