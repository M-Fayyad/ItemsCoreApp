using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;
using TestCoreApp.Models;

namespace TestCoreApp.Controllers
{
	public class ItemsController : Controller
	{
		private readonly AppDbContext _db;

		public ItemsController(AppDbContext db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
			IEnumerable<Item> itemsList = _db.Items.ToList();

			return View(itemsList);
		}

        //Get
        public IActionResult New()
        {
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Item item)
        {

            if (ModelState.IsValid) // this statement for server_side_validatoin
            {
                _db.Items.Add(item);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(item);
            }
        }

		//Get
		public IActionResult Edit(int? Id)
		{
			if (Id == null || Id ==0) //Check if the ID exists or not
            {
				return NotFound();
			}

			var item = _db.Items.Find(Id); // if Id exists then return item
			if (item == null)
			{
				return NotFound();
			}
			return View(item); // sent item to Edit view
		}

		//Post
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Item item)
		{
			if (ModelState.IsValid) // this for server_side_validatoin
			{
                _db.Items.Update(item);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
			else
			{
				return View(item);
			}
		}

		//Get
		public IActionResult Delete(int? Id)
		{
			if (Id == null || Id ==0) //Check if the ID exists or not
            {
				return NotFound();
			}

			var item = _db.Items.Find(Id); // if Id exists then return item
			if (item == null)
			{
				return NotFound();
			}
			return View(item); // sent item to Delete view
		}

		//Post
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(Item item)
		{
            _db.Items.Remove(item);
            _db.SaveChanges();
            return RedirectToAction("Index");
		}
    }
}
