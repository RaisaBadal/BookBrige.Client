using BookBridge.Client.Models;
using BookBridge.Server.DecerializerDtos;
using BookBridge.Server.ModelInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookBridge.Client.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookModelService ser;

        public BookController(IBookModelService ser)
        {
            this.ser = ser;
        }
        public async Task<ActionResult> Index()
        {
            var res = await ser.GetAllBooksAsync();
            if (res is null)
            {
                return View(new List<BookData>());
            }

            return View(res);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(long id)
        {
            var rs=await ser.GetBookDataById(id);
            var res= await ser.GetBookModel();
            res.PublishedDate = rs.PublishedDate;
            res.AvailableCopies = rs.AvailableCopies;
            res.CoverImageUrl = rs.CoverImageUrl;
            res.Description = rs.Description;
            res.Title = rs.Title;
            res.TotalCopies=rs.TotalCopies;
            res.Id = rs.Id;
            return View(res);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(BookData book)
        {
            try
            {
               var res= await ser.UpdateBooks(book);
                if(res)
                {
                    RedirectToAction("Index");
                }
                return BadRequest("Update failed");
            }
            catch (Exception)
            {
                return BadRequest(book);
            }

        }

        [HttpGet]
        public async Task<ActionResult> Delete(long id)
        {
            var res = await ser.DeleteBookByID(id);
            if(res == true)
            {
                RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
           var mod=await ser.GetBookModel();
            return View(mod);
        }

        [HttpPost]
        public async Task<ActionResult> Create(BookData book)
        {
            await Console.Out.WriteLineAsync( (book.CoverImageUrl==null).ToString());
            var res = await ser.InsertBook(book);
            if (res)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest("monacemebi ara validuria");
            }
        }

        [HttpGet]
        public async Task<ActionResult> Details(long id)
        {
           var res=await ser.GetBookModelDetails(id);
            if(res is null)
            {
                return NotFound();
            }
            return View(res);
        }
    }
}
