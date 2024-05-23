using ClienteAPI.Models;
using MessagePack;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;

namespace ClienteAPI.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly string urlCategory;
        private HttpClient client;

        public ProductCategoryController(IConfiguration configuration)
        {
            urlCategory = configuration["EndPoint:UrlProductCategory"];
                      var client = new HttpClient();
            
        }
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
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
