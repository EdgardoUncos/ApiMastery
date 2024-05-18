using ClienteAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace ClienteAPI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly string urlProducts;
        private HttpClient client = new HttpClient();
       
        public ProductsController(IConfiguration configuration)
        {
            urlProducts = configuration["EndPoint:UrlProducts"];
        }
        public async Task<IActionResult> Index()
        {
            var products = JsonConvert.DeserializeObject<List<Product>>(await client.GetStringAsync(urlProducts));

            return View(products);
        }
    }
}
