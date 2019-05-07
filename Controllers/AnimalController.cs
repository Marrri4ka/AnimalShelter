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
    public ActionResult ListIndex(string petName, string petSex, string petType, DateTime petDate, string petBreed)
    {
      Pet newPet = new Pet(petName,petSex,petType,petDate,petBreed);
      newPet.Save();
      List<Pet> allPets = Pet.GetAll();
      return View(allPets);
    }

    [HttpPost("/sortascending")]
       public ActionResult IndexPet()
       {
         List<Pet> allPetsAscending = Pet.SortAscending();
         return View("ListIndex",allPetsAscending);
       }

       [HttpPost("/sortdescending")]
          public ActionResult IndexMyPet()
          {
            List<Pet> allPetsDescending = Pet.SortDescending();
            return View("ListIndex",allPetsDescending);
          }


          [HttpPost("/petFilterType")]
                public ActionResult IndexMyType(string filtertype)
                {
                  List<Pet> allFirlteredPets= Pet.FilterType(filtertype);
                  return View("ListIndex",allFirlteredPets);
                }

}
}
