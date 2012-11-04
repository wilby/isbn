using System;
namespace ISBN.Web.Code
{
    public interface IISBNWeb
    {
        ISBN.Web.Models.BookModel GetBook(string isbn);
    }
}
