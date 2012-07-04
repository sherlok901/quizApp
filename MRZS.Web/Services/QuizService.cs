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
    public class QuizService : LinqToEntitiesDomainService<MRZSEntities>
    {
        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Quizs' query.
        public IQueryable<Quiz> GetQuizs()
        {
            return ObjectContext.Quizs;
        }

        public IQueryable<Quiz> GetQuizsById(int quizId)
        {
            return ObjectContext.Quizs.Where(q => q.QuizId == quizId && (q.IsDeleted == null || q.IsDeleted == false));
        }

        public IQueryable<Quiz> GetQuizsForSection(int sectionId)
        {
            return
                ObjectContext.Quizs.Where(q => q.SectionId == sectionId && (q.IsDeleted == null || q.IsDeleted == false));
        }

        public void InsertQuiz(Quiz quiz)
        {
            if ((quiz.EntityState != EntityState.Detached))
            {
                ObjectContext.ObjectStateManager.ChangeObjectState(quiz, EntityState.Added);
            }
            else
            {
                ObjectContext.Quizs.AddObject(quiz);
            }
        }

        public void UpdateQuiz(Quiz currentQuiz)
        {
            ObjectContext.Quizs.AttachAsModified(currentQuiz, ChangeSet.GetOriginal(currentQuiz));
        }

        public void DeleteQuiz(Quiz quiz)
        {
            if ((quiz.EntityState == EntityState.Detached))
            {
                ObjectContext.Quizs.Attach(quiz);
            }
            //this.ObjectContext.Questions.Where(q=>q.qui
            ObjectContext.Quizs.DeleteObject(quiz);
        }
    }
}