using System;
namespace ISBN.Web.Code
{
    public interface IRepository
    {
        System.Collections.Generic.IEnumerable<ISBN.Web.Models.BookModel> GetAllBooks();
        ISBN.Web.Models.BookModel GetBook(string isbn);
        void AddBook(Models.BookModel book);
        void DeleteBook(string isbn);
        void Save();
    }
}
