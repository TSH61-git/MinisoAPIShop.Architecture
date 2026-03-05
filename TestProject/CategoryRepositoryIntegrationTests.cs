using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository;

namespace TestProject
{
    public class CategoryRepositoryIntegrationTests : IDisposable
    {

        private readonly DatabaseFixture _fixture;
        private readonly MyWebApiShopContext _context;
        private readonly CategoryRepository _repository;

        public CategoryRepositoryIntegrationTests()
        {
            _fixture = new DatabaseFixture();
            _context = _fixture.Context;
            _repository = new CategoryRepository(_context);
        }

        public void Dispose()
        {
            _fixture.Dispose();
        }

        [Fact]
        public async Task GetCategories_ReturnsAllCategories()
        {
            // Arrange
            var category1 = new Category { CategoryName = "Category 1" };
            var category2 = new Category { CategoryName = "Category 2" };

            _context.Categories.AddRange(category1, category2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetCategories();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.CategoryName == "Category 1");
            Assert.Contains(result, c => c.CategoryName == "Category 2");
        }

        [Fact]
        public async Task GetCategories_EmptyTable_ReturnsEmptyList()
        {
            // Arrange
            _context.Categories.RemoveRange(_context.Categories);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetCategories();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
