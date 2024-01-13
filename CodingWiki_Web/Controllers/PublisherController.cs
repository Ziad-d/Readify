using CodingWiki_DataAccess.Data;
using CodingWiki_Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodingWiki_Web.Controllers
{
    public class PublisherController : Controller
    {
        private readonly ApplicitionDbContext context;

        public PublisherController(ApplicitionDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            List<Publisher> objList = context.Publishers.ToList();
            return View(objList);
        }

        public IActionResult Upsert(int? id)
        {
            Publisher obj = new Publisher();

            if(id == null || id == 0)
                return View(obj);

            obj = context.Publishers.FirstOrDefault(u => u.PublisherId == id);

            if(obj == null)
                return NotFound();

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Publisher obj)
        {
            if(ModelState.IsValid)
            {
                if(obj.PublisherId == 0)
                {
                    await context.Publishers.AddAsync(obj);
                }
                else
                {
                    context.Publishers.Update(obj);
                }
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            Publisher obj = new();

            obj = context.Publishers.FirstOrDefault(u => u.PublisherId == id);

            if(obj == null)
                return NotFound();

            context.Publishers.Remove(obj);

            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
