using ApiMastery.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ClienteAPI.Models;

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
            urlProducts = confi["EndPoint:UrlClients"];
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

            return View(orderVM);
        }
    }
}
