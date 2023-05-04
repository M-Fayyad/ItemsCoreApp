using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
			IEnumerable<Item> itemsList = _db.Items.Include(cat => cat.Category).ToList();

			return View(itemsList);
		}

        //Get
        public IActionResult New()
        {
            createSelectList();
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
				TempData["SuccessData"] = "Item has been added successfully"; 
                return RedirectToAction("Index");
            }
            else
            {
                return View(item);
            }
        }

		public void createSelectList(int SelectId = 1)
		{
			////create list of categories 
			//var categories = new List<Category>
			//{
			//	new Category{Id= 0, Name = "Select category" },
			//	new Category{Id= 1, Name = "Computer" },
			//	new Category{Id= 2, Name = "Mobile" },
			//	new Category{Id= 3, Name = "tablet" }
			//};
			List<Category> categories = _db.Categories.ToList(); // this from data base
			SelectList listItems = new SelectList(categories,"Id","Name",SelectId ); //"SelectList" class to view list to choice form it.
			ViewBag.CategryList = listItems; // "ViewBag" to move data from Controller to view.
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
			createSelectList(item.CategoryId);
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
				TempData["SuccessData"] = "Item has been updated successfully";
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
			createSelectList(item.CategoryId);
			return View(item); // sent item to Delete view
		}

		//Post
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(Item item)
		{
            _db.Items.Remove(item);
            _db.SaveChanges();
            TempData["SuccessData"] = "Item has been deleted successfully";
            return RedirectToAction("Index");
		}
    }
}
