using Castle.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class DatabaseFixture : IDisposable
    {
        public MyWebApiShopContext Context { get; private set; }

        public DatabaseFixture()
        {

            // Set up the test database connection and initialize the context
            var options = new DbContextOptionsBuilder<MyWebApiShopContext>()

                .UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True; TrustServerCertificate=True")
                .Options;
            Context = new MyWebApiShopContext(options);
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            // Clean up the test database after all tests are completed
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
