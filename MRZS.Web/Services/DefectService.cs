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
    public class DefectService : LinqToEntitiesDomainService<MRZSEntities>
    {
        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Defects' query.
        public IQueryable<Defect> GetDefects()
        {
            return ObjectContext.Defects;
        }

        public IQueryable<Defect> GetDefectsForInterectiveTest(int interectiveTestId)
        {
            return ObjectContext.Defects.Where(d => d.InterectiveTestId == interectiveTestId);
        }

        public void InsertDefect(Defect defect)
        {
            if ((defect.EntityState != EntityState.Detached))
            {
                ObjectContext.ObjectStateManager.ChangeObjectState(defect, EntityState.Added);
            }
            else
            {
                ObjectContext.Defects.AddObject(defect);
            }
        }

        public void UpdateDefect(Defect currentDefect)
        {
            ObjectContext.Defects.AttachAsModified(currentDefect, ChangeSet.GetOriginal(currentDefect));
        }

        public void DeleteDefect(Defect defect)
        {
            if ((defect.EntityState == EntityState.Detached))
            {
                ObjectContext.Defects.Attach(defect);
            }
            ObjectContext.Defects.DeleteObject(defect);
        }
    }
}