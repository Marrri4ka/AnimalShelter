using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Models;

namespace AnimalShelter.Controllers
{
  public class AnimalController : Controller
  {

    [HttpGet("/pet/new")]
    public ActionResult NewPet()
    {

      return View();
    }

    [HttpPost("/pet")]
    public ActionResult ListIndex(string petName, string petSex, string petType)
    {
      Pet newPet = new Pet(petName,petSex,petType);
      List<Pet> allPets = Pet.GetAll();
      return View(allPets);
    }


}
}
