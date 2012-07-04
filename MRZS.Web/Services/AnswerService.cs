using System.Data;
using System.Linq;
using System.ServiceModel.DomainServices.EntityFramework;
using System.ServiceModel.DomainServices.Hosting;
using MRZS.Web.Helpers;
using MRZS.Web.Models;

namespace MRZS.Web.Services
{
    // Implements application logic using the MRZSEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess]
    public class AnswerService : LinqToEntitiesDomainService<MRZSEntities>
    {
        public IQueryable<Answer> GetAnswers()
        {
            return ObjectContext.Answers;
        }

        public IQueryable<Answer> GetAnswersForQuestion(int QuestionId)
        {
            return
                ObjectContext.Answers.Where(
                    a => a.QuestionId == QuestionId && (a.IsDeleted == null || a.IsDeleted == false))
                    .Randomize().OfType<Answer>().AsQueryable<Answer>();
        }

        public void InsertAnswer(Answer answer)
        {
            if ((answer.EntityState != EntityState.Detached))
            {
                ObjectContext.ObjectStateManager.ChangeObjectState(answer, EntityState.Added);
            }
            else
            {
                ObjectContext.Answers.AddObject(answer);
            }
        }

        public void UpdateAnswer(Answer currentAnswer)
        {
            ObjectContext.Answers.AttachAsModified(currentAnswer, ChangeSet.GetOriginal(currentAnswer));
        }

        public void DeleteAnswer(Answer answer)
        {
            if ((answer.EntityState == EntityState.Detached))
            {
                ObjectContext.Answers.Attach(answer);
            }
            ObjectContext.Answers.DeleteObject(answer);
        }
    }
}