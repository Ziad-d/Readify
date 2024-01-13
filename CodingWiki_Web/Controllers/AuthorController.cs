using CodingWiki_DataAccess.Data;
using CodingWiki_Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodingWiki_Web.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ApplicitionDbContext context;

        public AuthorController(ApplicitionDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            List<Author> objList = context.Authors.ToList();
            return View(objList);
        }

        public IActionResult Upsert(int? id)
        {
            Author obj = new Author();

            if (id == null || id == 0)
                return View(obj);

            obj = context.Authors.FirstOrDefault(u => u.AuthorId == id);

            if (obj == null)
                return NotFound();

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Author obj)
        {
            if(ModelState.IsValid)
            {
                if(obj.AuthorId == 0)
                {
                    await context.Authors.AddAsync(obj);
                }
                else
                {
                    context.Authors.Update(obj);
                }

                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            Author obj = new();

            obj = context.Authors.FirstOrDefault(u => u.AuthorId == id);

            if(obj == null)
                return NotFound();

            context.Authors.Remove(obj);

            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
