using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace genmed_api.Controllers
{
    public class Fallback : Controller
    {
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Index.html"), "text/HTML");
        }
    }
}