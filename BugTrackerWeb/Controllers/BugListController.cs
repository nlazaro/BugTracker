using BugTrackerWeb.Data;
using BugTrackerWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BugTrackerWeb.Controllers
{
    public class BugListController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BugListController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<BugList> objBugList = _db.BugLists;
            return View(objBugList);
        }

        //♣[Authorize]
        public IActionResult Create()
        {
            return View();
        }

        //[Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BugList obj)
        {
            if (ModelState.IsValid)
            {
               _db.BugLists.Add(obj);
               _db.SaveChanges();
               TempData["success"] = "Created successfully";
               return RedirectToAction("Index");
            }
            return View(obj);
        }

        //[Authorize]
        //EDIT
        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }

            var catergoryFromDb = _db.BugLists.Find(id);
            if( catergoryFromDb == null)
            {
                return NotFound();
            }

            return View(catergoryFromDb);
        }

        //[Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BugList obj)
        {
            if (ModelState.IsValid)
            {
                _db.BugLists.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //[Authorize]
        //Delete 
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var catergoryFromDb = _db.BugLists.Find(id);
            if (catergoryFromDb == null)
            {
                return NotFound();
            }

            return View(catergoryFromDb);
        }
        //[Authorize]
        //Delete
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.BugLists.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.BugLists.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Deleted successfully";
            return RedirectToAction("Index");
        }

        //Search
        public async Task<IActionResult> SearchResults(string phrase)
        {
            return View("Index", await _db.BugLists.Where(b => b.Description.Contains(phrase) || b.Name.Contains(phrase)).ToListAsync());
        }

    }


}