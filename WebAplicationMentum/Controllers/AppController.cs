using Microsoft.AspNetCore.Mvc;

namespace WebAplicationMentum.Controllers
{
    public class AppController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Lista clientes";
            return View();
        }
        public IActionResult NewCustomer()
        {
            ViewData["Title"] = "Nuevo Cliente";
            return View();
        }

        public IActionResult CustomersContacts()
        {
            ViewData["Title"] = "Contactos de clientes";
            return View();
        }
        public IActionResult NewContact()
        {
            ViewData["Title"] = "Contactos de clientes";
            return View();
        }
    }
}
