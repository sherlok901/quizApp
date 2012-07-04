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
    public class SectionService : LinqToEntitiesDomainService<MRZSEntities>
    {
        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Sections' query.
        public IQueryable<Section> GetSections()
        {
            return ObjectContext.Sections.Where(s => s.IsDeleted == null || s.IsDeleted == false);
        }

        public IQueryable<Section> GetCurrentStudentSectionByUserName(string UserLogin)
        {
            IQueryable<Section> allSections =
                ObjectContext.Sections.Where(section => (section.IsDeleted == null || section.IsDeleted == false)
                                                        &&
                                                        (section.Book.IsDeleted == false ||
                                                         section.Book.IsDeleted == null));

            Section[] currentSections = (from user in ObjectContext.Users
                                         join aspUser in ObjectContext.aspnet_Users on user.aspnet_UserId equals
                                             aspUser.UserId
                                         join userSection in ObjectContext.UserSections on user.UserId equals
                                             userSection.UserId
                                         join section in ObjectContext.Sections on userSection.SectionId equals
                                             section.SectionId
                                         join book in ObjectContext.Books on section.BookId equals book.BookId
                                         where
                                             aspUser.UserName == UserLogin && userSection.IsPassed == null &&
                                             (section.IsDeleted == null || section.IsDeleted == false)
                                             && (book.IsDeleted == null || book.IsDeleted == false)
                                         select section).ToArray();

            foreach (Section section in allSections)
            {
                if (currentSections.Any(s => s.SectionId == section.SectionId))
                    section.IsCurrent = true;
            }

            return allSections;
        }

        public IQueryable<Section> GetCurrentStudentSection(int UserId)
        {
            IQueryable<Section> allSections =
                ObjectContext.Sections.Where(section => (section.IsDeleted == null || section.IsDeleted == false)
                                                        &&
                                                        (section.Book.IsDeleted == false ||
                                                         section.Book.IsDeleted == null));

            Section[] currentSections = (from user in ObjectContext.Users
                                         join userSection in ObjectContext.UserSections on user.UserId equals
                                             userSection.UserId
                                         join section in ObjectContext.Sections on userSection.SectionId equals
                                             section.SectionId
                                         join book in ObjectContext.Books on section.BookId equals book.BookId
                                         where
                                             user.UserId == UserId && userSection.IsPassed == null &&
                                             (section.IsDeleted == null || section.IsDeleted == false)
                                             && (book.IsDeleted == null || book.IsDeleted == false)
                                         select section).ToArray();

            foreach (Section section in allSections)
            {
                if (currentSections.Any(s => s.SectionId == section.SectionId))
                    section.IsCurrent = true;
            }

            return allSections;
        }

        public IQueryable<Section> GetSectionsForBook(int BookId)
        {
            return ObjectContext.Sections.Where(s => s.BookId == BookId && (s.IsDeleted == null || s.IsDeleted == false));
        }

        [Query(IsComposable = false)]
        public Section GetSectionById(int sectionId)
        {
            return
                ObjectContext.Sections.SingleOrDefault(
                    s => s.SectionId == sectionId && (s.IsDeleted == null || s.IsDeleted == false));
        }

        public void InsertSection(Section section)
        {
            if ((section.EntityState != EntityState.Detached))
            {
                ObjectContext.ObjectStateManager.ChangeObjectState(section, EntityState.Added);
            }
            else
            {
                ObjectContext.Sections.AddObject(section);
            }
        }

        public void UpdateSection(Section currentSection)
        {
            ObjectContext.Sections.AttachAsModified(currentSection, ChangeSet.GetOriginal(currentSection));
        }

        public void DeleteSection(Section section)
        {
            if ((section.EntityState == EntityState.Detached))
            {
                ObjectContext.Sections.Attach(section);
            }
            ObjectContext.Sections.DeleteObject(section);
        }
    }
}