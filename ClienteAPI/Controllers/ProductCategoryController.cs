using ClienteAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClienteAPI.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly string urlCategory;
        private HttpClient client = new HttpClient();

        public ProductCategoryController(IConfiguration configuration)
        {
            urlCategory = configuration["EndPoint:UrlProductCategory"];
        }
        public async Task<IActionResult> Index()
        {
            var productCategory = JsonConvert.DeserializeObject<List<ProductCategory>>(await client.GetStringAsync(urlCategory));
            return View(productCategory);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCategory category)
        {
            var responseMessage = await client.PostAsJsonAsync(urlCategory, category);
            responseMessage.EnsureSuccessStatusCode();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var urlID = urlCategory + "/" + id;
            await client.DeleteAsync(urlID);
            return RedirectToAction("Index");
        }
    }
}
