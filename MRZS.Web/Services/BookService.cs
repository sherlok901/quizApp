using System.Data;
using System.Linq;
using System.ServiceModel.DomainServices.EntityFramework;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices.Server;
using MRZS.Web.Models;

namespace MRZS.Web.Services
{
    // Implements application logic using the MRZSEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess]
    public class BookService : LinqToEntitiesDomainService<MRZSEntities>
    {
        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Books' query.
        public IQueryable<Book> GetBooks()
        {
            return ObjectContext.Books.Include("Sections").Where(b => b.IsDeleted == null || b.IsDeleted == false);
        }

        [Query(IsComposable = false)]
        public Book GetBookById(int bookId)
        {
            return
                ObjectContext.Books.Include("Sections").Where(b => b.IsDeleted == null || b.IsDeleted == false).
                    SingleOrDefault(b => b.BookId == bookId);
            ;
        }

        public void InsertBook(Book book)
        {
            if ((book.EntityState != EntityState.Detached))
            {
                ObjectContext.ObjectStateManager.ChangeObjectState(book, EntityState.Added);
            }
            else
            {
                ObjectContext.Books.AddObject(book);
            }
        }

        public void UpdateBook(Book currentBook)
        {
            ObjectContext.Books.AttachAsModified(currentBook, ChangeSet.GetOriginal(currentBook));
        }

        public void DeleteBook(Book book)
        {
            if ((book.EntityState == EntityState.Detached))
            {
                ObjectContext.Books.Attach(book);
            }
            ObjectContext.Books.DeleteObject(book);
        }
    }
}