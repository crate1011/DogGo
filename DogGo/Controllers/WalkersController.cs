using DogGo.Models;
using DogGo.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace DogGo.Controllers
{
    public class WalkersController : Controller
    {
        // GET: WalkersController
        // GET: Walkers
        public ActionResult Index()
        {
            int userId = GetCurrentUserId();
            Owner owner = _ownerRepo.GetOwnerById(userId);
            List<Walker> walkers = new List<Walker>();
            if (owner != null)
            {
                walkers = _walkerRepo.GetAllWalkers().Where(w => w.NeighborhoodId == owner.NeighborhoodId).ToList();
            }
            else
            {
                walkers = _walkerRepo.GetAllWalkers();
            }

            return View(walkers);
        }

        // GET: WalkersController/Details/5
        // GET: Walkers/Details/5
        public ActionResult Details(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);

            if (walker == null)
            {
                return NotFound();
            }

            return View(walker);
        }

        // GET: WalkersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalkersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalkersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalkersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id != null)
            {
                return int.Parse(id);
            }
            else
            {
                return 0;
            }
            
        }

        private readonly IWalkerRepository _walkerRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
       

        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        private readonly IOwnerRepository _ownerRepo;
        private readonly IDogRepository _dogRepo;
        private readonly INeighborhoodRepository _neighborhoodRepo;

        public WalkersController(
            IOwnerRepository ownerRepository,
            IDogRepository dogRepository,
            IWalkerRepository walkerRepository,
            INeighborhoodRepository neighborhoodRepository)
        {
            _ownerRepo = ownerRepository;
            _dogRepo = dogRepository;
            _walkerRepo = walkerRepository;
            _neighborhoodRepo = neighborhoodRepository;
        }
    }
}
