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
            List<Book> objList = context.Books.Include(u => u.Publisher)
                .Include(u => u.BookAuthorMap)
                .ThenInclude(u => u.Author).ToList();
            foreach (var obj in objList)
            {
                //// less efficient
                //obj.Publisher = context.Publishers.Find(obj.PublisherId);

                //// explicit loading
                //// Reference as there is only one publisher for the book(obj)
                //// Collection if there is more than one
                //context.Entry(obj).Reference(u => u.Publisher).Load();
                //context.Entry(obj).Collection(u => u.BookAuthorMap).Load();
                //foreach(var bookAuth in obj.BookAuthorMap)
                //{
                //context.Entry(bookAuth).Reference(u => u.Author).Load();
                //}
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
            if (obj.Book.BookId == 0)
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

            if (obj == null)
                return NotFound();

            context.Books.Remove(obj);

            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult ManageAuthors(int id)
        {
            BookAuthorVM obj = new()
            {
                BookAuthorList = context.BookAuthorMaps.Include(u => u.Author).Include(u => u.Book)
                    .Where(u => u.BookId == id).ToList(),
                BookAuthor = new()
                {
                    BookId = id
                },
                Book = context.Books.FirstOrDefault(u => u.BookId == id)
            };

            List<int> tempListOfAssignedAuthor = obj.BookAuthorList.Select(u => u.AuthorId).ToList();

            //NOT IN clause
            //get all the authors whos id is not in tempListOfAssignedAuthors
            var tempList = context.Authors.Where(u => !tempListOfAssignedAuthor.Contains(u.AuthorId)).ToList();
            obj.AuthorList = tempList.Select(i => new SelectListItem
            {
                Text = i.FullName,
                Value = i.AuthorId.ToString()
            });

            return View(obj);
        }

        [HttpPost]
        public IActionResult ManageAuthors(BookAuthorVM bookAuthorVM)
        {
            if(bookAuthorVM.BookAuthor.BookId != 0 && bookAuthorVM.BookAuthor.AuthorId !=0)
            {
                context.BookAuthorMaps.Add(bookAuthorVM.BookAuthor);
                context.SaveChanges();
            }
            return RedirectToAction("ManageAuthors", new
            {
                @id = bookAuthorVM.BookAuthor.BookId
            });
        }
        
        [HttpDelete]
        public IActionResult RemoveAuthors(int authorId, BookAuthorVM bookAuthorVM)
        {
            int bookId = bookAuthorVM.Book.BookId;
            BookAuthorMap bookAuthorMap = context.BookAuthorMaps.FirstOrDefault(
                u => u.BookId == bookId && u.AuthorId == authorId);

            context.BookAuthorMaps.Remove(bookAuthorMap);
            context.SaveChanges();

            return RedirectToAction("ManageAuthors", new
            {
                @id = bookId
            });
        }
    }
}
