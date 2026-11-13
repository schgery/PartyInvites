using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PartyInvites.Data;
using PartyInvites.Models;

namespace PartyInvites.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;

        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public ViewResult RsvpForm()
        {
            return View();
        }
        [HttpPost]
        public ViewResult RsvpForm(GuestResponse guestResponse)
        {
            if (ModelState.IsValid)
            {
                //Repository.AddResponse(guestResponse);
                _db.GuestResponses.Add(guestResponse);
                _db.SaveChanges();
                return View("Thanks", guestResponse);
            }
            else
            {
                return View();
            }
        }
        public ViewResult ListResponses()
        {
            //return View(Repository.Responses.Where(r => r.WillAttend == true));
            return View(_db.GuestResponses.Where(r => r.WillAttend == true).ToList());
        }

        public IActionResult Details(int id)
        {
            var guestResponse = _db.GuestResponses.FirstOrDefault(r => r.Id == id);
            if (guestResponse == null)
            {
                return NotFound();
            }
            return View(guestResponse);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var guestResponse = _db.GuestResponses.FirstOrDefault(r => r.Id == id);
            if (guestResponse == null)
            {
                return NotFound();
            }
            return View(guestResponse);
        }
        [HttpPost]
        public IActionResult Edit(GuestResponse guestResponse)
        {
            if (ModelState.IsValid)
            {
                _db.GuestResponses.Update(guestResponse);
                _db.SaveChanges();
                return RedirectToAction("ListResponses");
            }
            return View(guestResponse);
        }
        public IActionResult Delete(int id)
        {
            var guestResponse = _db.GuestResponses.FirstOrDefault(r => r.Id == id);
            if (guestResponse == null)
            {
                return NotFound();
            }
            _db.GuestResponses.Remove(guestResponse);
            _db.SaveChanges();
            return RedirectToAction("ListResponses");
        }
    }
}
