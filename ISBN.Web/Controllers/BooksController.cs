using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISBN.Web.Code;
using StructureMap;
using System.Text;
using ISBN.Web.Models;

namespace ISBN.Web.Controllers
{
    public class BooksController : Controller
    {
        private IRepository _repo;        
        private IISBNWeb _bookInfo;

        public BooksController()
        {         
            _bookInfo = ObjectFactory.GetInstance<IISBNWeb>();
            _repo = ObjectFactory.GetInstance<IRepository>();
        }

        public ActionResult Index()
        {
            var books = _repo.GetAllBooks().OrderBy(x => x.Author.ToLower());            
            return View(books);
        }

        public RedirectToRouteResult Delete(string id)
        {
            _repo.DeleteBook(id);
            _repo.Save();
            return RedirectToAction("Index");
        }

        public RedirectToRouteResult Create(string id)
        {
            var book = _bookInfo.GetBook(id);
            if (book == null)
            {
                ViewBag.Error = "That ISBN could not be found.";
                ViewBag.ISBN = id;
            }
            else
            {
                _repo.AddBook(book);
                _repo.Save();
            }
            return RedirectToAction("Index");

        }

        [Authorize(Roles = "Admins")]
        public String Export()
        {
            var books = _repo.GetAllBooks();
            double totalTitleLength = Convert.ToDouble(LongestBookLength(books)) * 4;
            ViewBag.FixedWidth = totalTitleLength;
            StringBuilder builder = new StringBuilder();
            builder.Append("<html><head >");
            builder.Append("<style type='text/css'>.fixed-width { display: inline-block; width:");
            builder.Append(totalTitleLength);            
            builder.Append("pt; }</style></head><body>");
            builder.Append(padRight("Title", Convert.ToInt32(totalTitleLength), "&nbsp;"));
            
            builder.Append("Author");
            builder.Append("<br />");
            foreach (var book in books)
            {
                builder.Append(padRight(book.Title,Convert.ToInt32(totalTitleLength), "&nbsp;"));                
                builder.Append(book.Author);
                builder.Append("<br />");
            }
            builder.Append("</body></html>");
            return builder.ToString();
        }

        private string padRight(string s, int length, string paddingChar)
        {
            return "<span class='fixed-width'>" + s + "</span>";
            while (s.Length < length)
            {
                s += paddingChar;
            }
            return s;
        }

        private int LongestBookLength(IEnumerable<BookModel> books)
        {
            return books.Max(x => x.Title.Length);
            //int longest = -1;
            //foreach (var book in books)
            //{
            //    if (book.Title.Length > longest)
            //        longest = book.Title.Length;
            //}
            //return longest;
        }
    }
}
