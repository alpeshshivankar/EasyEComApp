using ECom.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Newtonsoft.Json;


namespace ECom.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:5001/api/v1/Product");
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(); 
                var ProdResponse = JsonConvert.DeserializeObject(content);
                
                List<Models.Product> products = new List<Models.Product>();
                foreach (var item in products)
                {
                    Models.Product prod = new Models.Product();
                    prod.Id= item.Id;
                    prod.ProductName= item.ProductName;
                    prod.UnitPrice= item.UnitPrice;
                    products.Add(prod);
                }
            
                return View(products);
            }
            else
            {
                // Handle error response, throw an exception, or return a default value
                throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
            }
            

        }
    }
}
