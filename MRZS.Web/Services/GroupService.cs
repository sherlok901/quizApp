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
    public class GroupService : LinqToEntitiesDomainService<MRZSEntities>
    {
        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Groups' query.
        public IQueryable<Group> GetGroups()
        {
            return ObjectContext.Groups.Include("Users").Where(g => g.IsDeleted == null || g.IsDeleted == false);
        }

        public void InsertGroup(Group group)
        {
            if ((group.EntityState != EntityState.Detached))
            {
                ObjectContext.ObjectStateManager.ChangeObjectState(group, EntityState.Added);
            }
            else
            {
                ObjectContext.Groups.AddObject(group);                
            }
        }

        public void UpdateGroup(Group currentGroup)
        {
            ObjectContext.Groups.AttachAsModified(currentGroup, ChangeSet.GetOriginal(currentGroup));
        }

        public void DeleteGroup(Group group)
        {
            if ((group.EntityState == EntityState.Detached))
            {
                ObjectContext.Groups.Attach(group);
            }
            ObjectContext.Groups.DeleteObject(group);
        }
    }
}