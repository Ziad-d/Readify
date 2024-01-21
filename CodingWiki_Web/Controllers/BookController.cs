using CodingWiki_DataAccess.Data;
using CodingWiki_Model.Models;
using CodingWiki_Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CodingWiki_Web.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicitionDbContext context;

        public BookController(ApplicitionDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            List<Book> objList = context.Books.Include(u => u.Publisher).ToList();
            foreach (var obj in objList)
            {
                //// less efficient
                //obj.Publisher = context.Publishers.Find(obj.PublisherId);

                //// explicit loading
                //// Reference as there is only one publisher for the book(obj)
                //// Collection if there is more than one
                //context.Entry(obj).Reference(u => u.Publisher).Load();
            }
            return View(objList);
        }

        public IActionResult Upsert(int? id)
        {
            BookVM obj = new BookVM();

            obj.PublisherList = context.Publishers.Select(i => new SelectListItem
            {
                Text = i.PublisherName,
                Value = i.PublisherId.ToString()
            });

            if (id == null || id == 0)
                return View(obj);

            obj.Book = context.Books.FirstOrDefault(u => u.BookId == id);

            if (obj == null)
                return NotFound();

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(BookVM obj)
        {
               if(obj.Book.BookId == 0)
                {
                    await context.Books.AddAsync(obj.Book);
                }
                else
                {
                    context.Books.Update(obj.Book);
                }

                await context.SaveChangesAsync();
                return RedirectToAction("Index");
        }

        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            BookDetail obj = new BookDetail();

            obj = context.BookDetails.Include(u => u.Book).FirstOrDefault(u => u.BookId == id);

            if (obj == null)
                return NotFound();

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Details(BookDetail obj)
        {
            if (obj.BookDetail_Id == 0)
            {
                await context.BookDetails.AddAsync(obj);
            }
            else
            {
                context.BookDetails.Update(obj);
            }

            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            Book obj = new();

            obj = context.Books.FirstOrDefault(u => u.BookId == id);

            if(obj == null)
                return NotFound();

            context.Books.Remove(obj);

            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
