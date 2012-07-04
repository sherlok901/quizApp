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
    public class InterectiveTestLogService : LinqToEntitiesDomainService<MRZSEntities>
    {
        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'InterectiveTestLogs' query.
        public IQueryable<InterectiveTestLog> GetInterectiveTestLogs()
        {
            return ObjectContext.InterectiveTestLogs;
        }

        public IQueryable<InterectiveTestLog> GetInterectiveTestLogsForSection(int sectionId)
        {
            int testsCount = ObjectContext.InterectiveTests.Count(it => it.Sections.Any(s => s.SectionId == sectionId));
            return ObjectContext.InterectiveTestLogs.Where(it => it.SectionId == sectionId).Take(testsCount);
        }

        public void InsertInterectiveTestLog(InterectiveTestLog interectiveTestLog)
        {
            if ((interectiveTestLog.EntityState != EntityState.Detached))
            {
                ObjectContext.ObjectStateManager.ChangeObjectState(interectiveTestLog, EntityState.Added);
            }
            else
            {
                ObjectContext.InterectiveTestLogs.AddObject(interectiveTestLog);
            }
        }

        public void UpdateInterectiveTestLog(InterectiveTestLog currentInterectiveTestLog)
        {
            ObjectContext.InterectiveTestLogs.AttachAsModified(currentInterectiveTestLog,
                                                               ChangeSet.GetOriginal(currentInterectiveTestLog));
        }

        public void DeleteInterectiveTestLog(InterectiveTestLog interectiveTestLog)
        {
            if ((interectiveTestLog.EntityState == EntityState.Detached))
            {
                ObjectContext.InterectiveTestLogs.Attach(interectiveTestLog);
            }
            ObjectContext.InterectiveTestLogs.DeleteObject(interectiveTestLog);
        }
    }
}