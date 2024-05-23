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
            // Obtener el token de la sesion
            var token = HttpContext.Session.GetString("token");

            // Verificar si el token esta presente
            if (string.IsNullOrEmpty(token))
            {
                // Si no hay token, retornar la vista NotAuthorizer
                return View("NotAuthorized");
            }

            // COnfigurar la autorizacion en el cliente HTTP
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

            try
            {
                // Hacer la solicitud para obtener los productos
                var products = JsonConvert.DeserializeObject<List<Product>>(await client.GetStringAsync(urlProducts));
                return View(products);

            }
            catch(HttpRequestException)
            {
                // En caso de error en la solicitud, retornar la vista Not AUthorized
                return View("NotAuthorized");
            }
            

        }
    }
}
