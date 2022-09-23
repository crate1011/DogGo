using DogGo.Models;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace DogGo.Controllers
{
    public class DogsController : Controller
    {
        // GET: DogControllers
        [Authorize]
        public ActionResult Index()
        {
            int ownerId = GetCurrentUserId();

            List<Dog> dogs = _dogRepo.GetDogsByOwnerId(ownerId);

            return View(dogs);
        }

        // GET: DogsController/Details/5
        public ActionResult Details(int id)
        {
            Dog dog = _dogRepo.GetDogById(id);

            if (dog == null)
            {
                return NotFound();
            }

            return View(dog);
        }

        // GET: OwnersController/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Owners/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dog dog)
        {
            try
            {
                // update the dogs OwnerId to the current user's Id
                dog.OwnerId = GetCurrentUserId();

                _dogRepo.AddDog(dog);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        // GET: Owners/Edit/5
        public ActionResult Edit(int id)
        {
            int ownerId = GetCurrentUserId();

            Dog dog = _dogRepo.GetDogById(id);

            if (dog == null || dog.OwnerId != ownerId)
            {
                return NotFound();
            }

            return View(dog);
        }

        // POST: Owners/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Dog dog)
        {
            try
            {
                _dogRepo.UpdateDog(dog);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        // GET: Owners/Delete/5
        public ActionResult Delete(int id)
        {
            int ownerId = GetCurrentUserId();

            Dog dog = _dogRepo.GetDogById(id);

            if (dog == null || dog.OwnerId != ownerId)
            {
                return NotFound();
            }

            return View(dog);
        }

        // POST: OwnersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Dog dog)
        {
            try
            {
                _dogRepo.DeleteDog(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
        private readonly IDogRepository _dogRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public DogsController(IDogRepository dogRepository)
        {
            _dogRepo = dogRepository;
        }
    }
}
