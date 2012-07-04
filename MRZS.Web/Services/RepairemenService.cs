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
    public class RepairemenService : LinqToEntitiesDomainService<MRZSEntities>
    {
        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Repairmen' query.
        public IQueryable<Repairman> GetRepairmen()
        {
            return ObjectContext.Repairmen;
        }

        public IQueryable<Repairman> GetRepairmenForDefect(int defectId)
        {
            return ObjectContext.Repairmen.Where(r => r.DefectId == defectId);
        }

        public void InsertRepairman(Repairman repairman)
        {
            if ((repairman.EntityState != EntityState.Detached))
            {
                ObjectContext.ObjectStateManager.ChangeObjectState(repairman, EntityState.Added);
            }
            else
            {
                ObjectContext.Repairmen.AddObject(repairman);
            }
        }

        public void UpdateRepairman(Repairman currentRepairman)
        {
            ObjectContext.Repairmen.AttachAsModified(currentRepairman, ChangeSet.GetOriginal(currentRepairman));
        }

        public void DeleteRepairman(Repairman repairman)
        {
            if ((repairman.EntityState == EntityState.Detached))
            {
                ObjectContext.Repairmen.Attach(repairman);
            }
            ObjectContext.Repairmen.DeleteObject(repairman);
        }
    }
}