using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebPortal.Api.Test
{
    public class ProductsTest : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _factory;

        public ProductsTest(ApiTestFixture factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetApiEndpoint_ReturnSuccess()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/products");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
