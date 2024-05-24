using Boocic.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Boocic.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class DashboardController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
