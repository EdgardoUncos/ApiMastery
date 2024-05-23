using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ClienteAPI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClienteAPI.Controllers
{
    public class OrdersController : Controller
    {
        private readonly string urlClients;
        private readonly string urlOrders;
        private readonly string urlProducts;
        private HttpClient httpClient = new HttpClient();

        public OrdersController(IConfiguration confi)
        {
            urlClients = confi["EndPoint:UrlClients"];
            urlOrders = confi["EndPoint:UrlOrders"];
            urlProducts = confi["EndPoint:UrlProducts"];
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = JsonConvert.DeserializeObject<List<Order>>(await httpClient.GetStringAsync(urlOrders));

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> NewOrder(int id)
        {
            Order order = new Order();
            var urlID = urlClients + "/" + id;
            var client = JsonConvert.DeserializeObject<Client>(await httpClient.GetStringAsync(urlID));

            order.ClientId = client.Id;
            order.OrderDate = DateTime.Now;

            OrderVM orderVM = new OrderVM();
            orderVM.Client = client;
            orderVM.Order = order;

            var products = JsonConvert.DeserializeObject<List<Product>>(await httpClient.GetStringAsync(urlProducts));

            ViewBag.Products = products.ConvertAll(p =>
            {
                return new SelectListItem()
                {
                    Text = p.Name,
                    Value = p.Id.ToString(),
                    Selected = true
                };
            });

            return View(orderVM);
        }

        [HttpPost]
        public async Task<IActionResult> NewOrder([FromBody]Order order)
        {
            order.Id = 0;
            order.DeliveryDate = DateTime.Now;
            order.OrderDate = DateTime.Now;

            var result = await httpClient.PostAsJsonAsync(urlOrders, order);

            if (result.IsSuccessStatusCode)
            {
                return View("Ok");

            }
            return View(order);
            

        }

            [HttpPost]
        public async Task<IActionResult> GetPrice([FromBody] int id)
        {
            var urlID = urlProducts + "/" + id;
            var product = JsonConvert.DeserializeObject<Product>(await httpClient.GetStringAsync(urlID));

            return Ok(product.Price);
        }
    }
}
