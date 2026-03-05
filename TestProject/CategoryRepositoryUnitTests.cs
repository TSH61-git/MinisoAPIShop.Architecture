using Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Repository;

namespace TestProject
{
    public class CategoryRepositoryUnitTests
    {
        [Fact]
        public async Task GetCategories_ReturnsAllCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { CategoryName = "Category 1" },
                new Category { CategoryName = "Category 2" }
            };

            var mockContext = new Mock<MyWebApiShopContext>();
            mockContext.Setup(x => x.Categories).ReturnsDbSet(categories);

            var repository = new CategoryRepository(mockContext.Object);

            // Act
            var result = await repository.GetCategories();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.CategoryName == "Category 1");
            Assert.Contains(result, c => c.CategoryName == "Category 2");
        }

        [Fact]
        public async Task GetCategories_EmptyTable_ReturnsEmptyList()
        {
            // Arrange
            var categories = new List<Category>();
            var mockContext = new Mock<MyWebApiShopContext>();
            mockContext.Setup(x => x.Categories).ReturnsDbSet(categories);

            var repository = new CategoryRepository(mockContext.Object);

            // Act
            var result = await repository.GetCategories();

            // Assert
            Assert.Empty(result);
        }
    }
}
