using DTOs;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace WebApiShop.Controllers
{
    // ChatController.cs
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase, IChatController
    {
        private readonly HttpClient _http;
        private readonly IProductService _productService;

        public ChatController(IHttpClientFactory factory, IProductService productService)
        {
            _http = factory.CreateClient();
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ChatRequest req)
        {
            var pageResponse = await _productService.GetProducts(position: 1, skip: 50, parameters: new ProductSearchParams());

            var rawProducts = pageResponse?.Data ?? Enumerable.Empty<ProductDTO>();

            var productList = rawProducts
                .Take(50)
                .Select(p => new {
                    Name = p.ProductName,
                    Price = p.Price,     
                    Description = p.Description 
                }).ToList();

            var payload = new
            {
                message = req.Message,
                history = req.History,
                products = productList 
            };

            var res = await _http.PostAsJsonAsync("http://localhost:8001/chat", payload);

            if (!res.IsSuccessStatusCode)
                return StatusCode(500, "AI service unavailable");

            var data = await res.Content.ReadFromJsonAsync<ChatResponse>();
            return Ok(data);
        }
    }

    public record ChatRequest(
        string Message,
        List<HistoryItem> History,
        List<object> Products);

    public record HistoryItem(string Role, string Content);
    public record ChatResponse(string Reply);
}
