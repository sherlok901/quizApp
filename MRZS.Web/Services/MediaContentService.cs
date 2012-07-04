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
    public class MediaContentService : LinqToEntitiesDomainService<MRZSEntities>
    {
        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'MultimediaContents' query.
        public IQueryable<MultimediaContent> GetMultimediaContents()
        {
            return ObjectContext.MultimediaContents;
        }

        public IQueryable<MultimediaContent> GetMultimediaContentsForPage(int pageId)
        {
            return
                ObjectContext.MultimediaContents.Where(
                    m => m.PageId == pageId && (m.IsDeleted == false || m.IsDeleted == null));
        }

        public void InsertMultimediaContent(MultimediaContent multimediaContent)
        {
            if ((multimediaContent.EntityState != EntityState.Detached))
            {
                ObjectContext.ObjectStateManager.ChangeObjectState(multimediaContent, EntityState.Added);
            }
            else
            {
                ObjectContext.MultimediaContents.AddObject(multimediaContent);
            }
        }

        public void UpdateMultimediaContent(MultimediaContent currentMultimediaContent)
        {
            ObjectContext.MultimediaContents.AttachAsModified(currentMultimediaContent,
                                                              ChangeSet.GetOriginal(currentMultimediaContent));
        }

        public void DeleteMultimediaContent(MultimediaContent multimediaContent)
        {
            if ((multimediaContent.EntityState == EntityState.Detached))
            {
                ObjectContext.MultimediaContents.Attach(multimediaContent);
            }
            ObjectContext.MultimediaContents.DeleteObject(multimediaContent);
        }
    }
}