using ClienteAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClienteAPI.Controllers
{
    public class ClientsController : Controller
    {
        private readonly string urlClients;
        private HttpClient client = new HttpClient();

        public ClientsController(IConfiguration configuration)
        {
            urlClients = configuration["EndPoint:UrlClients"];
        }
        public async Task<IActionResult> Index()
        {
            var clients = JsonConvert.DeserializeObject<List<Client>>(await client.GetStringAsync(urlClients));
            return View(clients);
        }
    }
}
