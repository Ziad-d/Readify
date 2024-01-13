using CodingWiki_DataAccess.Data;
using CodingWiki_Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodingWiki_Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicitionDbContext context;

        public CategoryController(ApplicitionDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            List<Category> objList = context.Categories.ToList();
            return View(objList);
        }

        public IActionResult Upsert(int? id)
        {
            Category obj = new();

            if(id == null || id == 0)
                return View(obj);

            obj = context.Categories.FirstOrDefault(u => u.CategoryId == id);

            if(obj == null)
                return NotFound();

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Category obj)
        {
            if(ModelState.IsValid)
            {
                if(obj.CategoryId == 0)
                {
                    // create
                    await context.Categories.AddAsync(obj);
                }
                else
                {
                    context.Categories.Update(obj);
                }

                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            Category obj = new();

            obj = context.Categories.FirstOrDefault(u => u.CategoryId == id);

            if (obj == null)
                return NotFound();

            context.Categories.Remove(obj);

            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public ActionResult CreateMultiple2()
        {
            for(int i = 1; i <= 2; i++)
            {
                context.Add(new Category { CategoryName = Guid.NewGuid().ToString() });
            }
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CreateMultiple5()
        {
            List<Category> categories = new List<Category>();

            for (int i = 1; i <= 5; i++)
            {
                // instead of adding to the db
                categories.Add(new Category { CategoryName = Guid.NewGuid().ToString() });
            }
            // built in funciton for bulk insert
            context.Categories.AddRange(categories);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult RemoveMultiple2()
        {
            List<Category> categories = context.Categories.OrderByDescending(c => c.CategoryId).Take(2).ToList();
            context.Categories.RemoveRange(categories);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult RemoveMultiple5()
        {
            List<Category> categories = context.Categories.OrderByDescending(c => c.CategoryId).Take(5).ToList();
            context.Categories.RemoveRange(categories);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
