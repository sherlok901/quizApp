using System.Data;
using System.IO;
using System.Linq;
using System.ServiceModel.DomainServices.EntityFramework;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices.Server;
using System.Text;
using MRZS.Web.Models;

namespace MRZS.Web.Services
{
    // Implements application logic using the MRZSEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess]
    public class PageService : LinqToEntitiesDomainService<MRZSEntities>
    {
        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Pages' query.
        public IQueryable<Page> GetPages()
        {
            return ObjectContext.Pages;
        }

        public IQueryable<Page> GetPagesForSection(int sectionId)
        {
            return
                ObjectContext.Pages.Where(p => p.SectionId == sectionId && (p.IsDeleted == false || p.IsDeleted == null))
                    .OrderBy(p => p.PageOrder);
        }

        [Query(IsComposable = false)]
        public Page GetPageById(int pageId)
        {
            return ObjectContext.Pages.SingleOrDefault(p => p.PageId == pageId);
        }

        [Query(IsComposable = false)]
        public Page GetNextPageForStudent(int currentPageId, string userName)
        {
            Page nextPage = null;
            if (currentPageId == -1)
            {
                nextPage = (from page in ObjectContext.Pages.Include("Section")
                            join section in ObjectContext.Sections on page.SectionId equals section.SectionId
                            join userSection in ObjectContext.UserSections on section.SectionId equals
                                userSection.SectionId
                            join user in ObjectContext.Users on userSection.UserId equals user.UserId
                            join aspUser in ObjectContext.aspnet_Users on user.aspnet_UserId equals aspUser.UserId
                            where
                                aspUser.UserName == userName && page.PageOrder == 1 &&
                                (userSection.IsPassed == false || userSection.IsPassed == null)
                            //TODO: page.PageOrder == 1
                            orderby page.PageOrder
                            select page).FirstOrDefault();
                if (nextPage != null)
                {
                    nextPage.PageContent = LoadPageMarkup(nextPage);
                    nextPage.PagePath = nextPage.PagePath;
                }
                return nextPage;
            }
            Page currentPage = ObjectContext.Pages.Single(p => p.PageId == currentPageId);

            nextPage = (from page in ObjectContext.Pages.Include("Section")
                        join section in ObjectContext.Sections on page.SectionId equals section.SectionId
                        join userSection in ObjectContext.UserSections on section.SectionId equals userSection.SectionId
                        join user in ObjectContext.Users on userSection.UserId equals user.UserId
                        join aspUser in ObjectContext.aspnet_Users on user.aspnet_UserId equals aspUser.UserId
                        where aspUser.UserName == userName
                              && page.PageOrder == currentPage.PageOrder + 1
                              && (page.IsDeleted == false || page.IsDeleted == null)
                              && (userSection.IsPassed == false || userSection.IsPassed == null)
                        select page).FirstOrDefault();

            if (nextPage != null)
            {
                nextPage.PageContent = LoadPageMarkup(nextPage);
                nextPage.PagePath = nextPage.PagePath;
            }
            return nextPage;
        }

        /// <summary>
        /// Detects the byte order mark of a file and returns
        /// an appropriate encoding for the file.
        /// </summary>
        /// <param name="srcFile"></param>
        /// <returns></returns>
        public static Encoding GetFileEncoding(string srcFile)
        {
            // *** Use Default of Encoding.Default (Ansi CodePage)
            Encoding enc = Encoding.Default;
            // *** Detect byte order mark if any - otherwise assume default
            var buffer = new byte[5];
            var file = new FileStream(srcFile, FileMode.Open);
            file.Read(buffer, 0, 5);
            file.Close();

            if (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)
                enc = Encoding.UTF8;
            else if (buffer[0] == 0xfe && buffer[1] == 0xff)
                enc = Encoding.Unicode;
            else if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff)
                enc = Encoding.UTF32;
            else if (buffer[0] == 0x2b && buffer[1] == 0x2f && buffer[2] == 0x76)
                enc = Encoding.UTF7;

            return enc;
        }

        private string LoadPageMarkup(Page page)
        {
            string markup = string.Empty;
            if (page == null)
                return markup;
            StreamReader fileStream = null;
            try
            {
                Encoding enc = GetFileEncoding(page.PagePath);
                fileStream = new StreamReader(page.PagePath, enc);
                markup = fileStream.ReadToEnd();
            }
            catch
            {
                markup = "Текущий файл недоступен. Обратитесь за помощью к администратору.";
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Close();
            }
            return markup;
        }

        [Query(IsComposable = false)]
        public Page GetPreviousPageForStudent(int currentPageId, string userName)
        {
            Page previousPage = null;
            Page currentPage = ObjectContext.Pages.Single(p => p.PageId == currentPageId);

            previousPage = (from page in ObjectContext.Pages.Include("Section")
                            join section in ObjectContext.Sections on page.SectionId equals section.SectionId
                            join userSection in ObjectContext.UserSections on section.SectionId equals
                                userSection.SectionId
                            join user in ObjectContext.Users on userSection.UserId equals user.UserId
                            join aspUser in ObjectContext.aspnet_Users on user.aspnet_UserId equals aspUser.UserId
                            where
                                aspUser.UserName == userName && page.PageOrder == currentPage.PageOrder - 1 &&
                                (userSection.IsPassed == false || userSection.IsPassed == null)
                            select page).FirstOrDefault();
            if (previousPage != null)
            {
                previousPage.PageContent = LoadPageMarkup(previousPage);
                previousPage.PagePath = previousPage.PagePath;
            }
            return previousPage;
        }

        public void InsertPage(Page page)
        {
            if ((page.EntityState != EntityState.Detached))
            {
                ObjectContext.ObjectStateManager.ChangeObjectState(page, EntityState.Added);
            }
            else
            {
                ObjectContext.Pages.AddObject(page);
            }
        }

        public void UpdatePage(Page currentPage)
        {
            ObjectContext.Pages.AttachAsModified(currentPage, ChangeSet.GetOriginal(currentPage));
        }

        public void DeletePage(Page page)
        {
            if ((page.EntityState == EntityState.Detached))
            {
                ObjectContext.Pages.Attach(page);
            }
            ObjectContext.Pages.DeleteObject(page);
        }
    }
}