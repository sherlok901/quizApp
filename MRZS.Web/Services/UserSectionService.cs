using System.Data;
using System.Linq;
using System.ServiceModel.DomainServices.EntityFramework;
using System.ServiceModel.DomainServices.Hosting;
using MRZS.Web.Models;

namespace MRZS.Web.Services
{
    // Implements application logic using the MRZSEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess]
    public class UserSectionService : LinqToEntitiesDomainService<MRZSEntities>
    {
        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'UserSections' query.
        public IQueryable<UserSection> GetUserSections()
        {
            return ObjectContext.UserSections;
        }

        public void InsertUserSection(UserSection userSection)
        {
            if (!(userSection.IsPassed.HasValue && userSection.IsPassed.Value))
            {
                foreach (UserSection us in ObjectContext.UserSections.Where(i => i.UserId == userSection.UserId))
                {
                    us.IsPassed = true;
                }
            }
            if ((userSection.EntityState != EntityState.Detached))
            {
                ObjectContext.ObjectStateManager.ChangeObjectState(userSection, EntityState.Added);
            }
            else
            {
                ObjectContext.UserSections.AddObject(userSection);
            }
        }

        public void UpdateUserSection(UserSection currentUserSection)
        {
            ObjectContext.UserSections.AttachAsModified(currentUserSection, ChangeSet.GetOriginal(currentUserSection));
        }

        public void DeleteUserSection(UserSection userSection)
        {
            if ((userSection.EntityState == EntityState.Detached))
            {
                ObjectContext.UserSections.Attach(userSection);
            }
            ObjectContext.UserSections.DeleteObject(userSection);
        }
    }
}