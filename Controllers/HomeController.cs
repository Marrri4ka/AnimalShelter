using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult IndexHome()
    {
      return View();
    }

  }
}
