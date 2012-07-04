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
    public class ResultAnswerService : LinqToEntitiesDomainService<MRZSEntities>
    {
        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ResultAnswers' query.
        public IQueryable<ResultAnswer> GetResultAnswers()
        {
            return ObjectContext.ResultAnswers;
        }

        public IQueryable<ResultAnswer> GetResultAnswersForQuizResult(int quizResultId)
        {
            return from resultAnswer in ObjectContext.ResultAnswers
                   where resultAnswer.QuizResultId == quizResultId
                   select resultAnswer;
        }

        public void InsertResultAnswer(ResultAnswer resultAnswer)
        {
            if ((resultAnswer.EntityState != EntityState.Detached))
            {
                ObjectContext.ObjectStateManager.ChangeObjectState(resultAnswer, EntityState.Added);
            }
            else
            {
                ObjectContext.ResultAnswers.AddObject(resultAnswer);
            }
        }

        public void UpdateResultAnswer(ResultAnswer currentResultAnswer)
        {
            ObjectContext.ResultAnswers.AttachAsModified(currentResultAnswer, ChangeSet.GetOriginal(currentResultAnswer));
        }

        public void DeleteResultAnswer(ResultAnswer resultAnswer)
        {
            if ((resultAnswer.EntityState == EntityState.Detached))
            {
                ObjectContext.ResultAnswers.Attach(resultAnswer);
            }
            ObjectContext.ResultAnswers.DeleteObject(resultAnswer);
        }
    }
}