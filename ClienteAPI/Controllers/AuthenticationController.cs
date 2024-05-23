using ClienteAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace ClienteAPI.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly string UrlLogin;
        private IConfiguration confi;

        public AuthenticationController(IConfiguration confi)
        {
            this.confi = confi;
            UrlLogin = confi["EndPoint:UrlLogin"];
        }
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequestDto login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

            using (var cliente = new HttpClient())
            {
                var respuesta = await cliente.PostAsJsonAsync(UrlLogin, login);

                if (respuesta.IsSuccessStatusCode)
                {
                    // leer el contenido de la respuesta
                    var contenido = JsonConvert.DeserializeObject<AuthResult>(await respuesta.Content.ReadAsStringAsync());
                    var token = contenido?.Token;

                    HttpContext.Session.SetString("email", login.Email);
                    HttpContext.Session.SetString("token", token);

                    return RedirectToAction("Index","Products");
                }
                else
                {
                    return View(login);
                }
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("token");
            HttpContext.Session.Remove("email");
            return RedirectToAction("Login", "Authentication");
        }
    }

    public class AuthResult
    {
        public string Token { get; set; }
        public bool Result { get; set; }
        public List<string> Errors { get; set; }
    }
}
