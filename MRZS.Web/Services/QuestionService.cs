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
    public class QuestionService : LinqToEntitiesDomainService<MRZSEntities>
    {
        public IQueryable<Question> GetQuestions()
        {
            return ObjectContext.Questions;
        }

        public IQueryable<Question> GetQuestionsForQuiz(int quizId)
        {
            return
                ObjectContext.Questions.Where(q => q.QuizId == quizId && (q.IsDeleted == false || q.IsDeleted == null));
        }

        public IQueryable<Question> GetQuestionsForLearning(int quizId)
        {
            int questionCount = Preferences.QuestionsCount;

            return (from quiz in ObjectContext.Quizs
                    join question in ObjectContext.Questions on quiz.QuizId equals question.QuizId
                    where quiz.QuizId == quizId
                          && (question.IsDeleted == false || question.IsDeleted == null)
                    select question).Take(questionCount);
        }

        public IQueryable<Question> GetQuestionsForSections(int sectionId)
        {
            return from question in ObjectContext.Questions
                   join quiz in ObjectContext.Quizs on question.QuizId equals quiz.QuizId
                   join section in ObjectContext.Sections on quiz.SectionId equals section.SectionId
                   where section.SectionId == sectionId
                         && (question.IsDeleted == false || question.IsDeleted == null)
                   select question;
        }

        public void InsertQuestion(Question question)
        {
            if ((question.EntityState != EntityState.Detached))
            {
                ObjectContext.ObjectStateManager.ChangeObjectState(question, EntityState.Added);
            }
            else
            {
                ObjectContext.Questions.AddObject(question);
            }
        }

        public void UpdateQuestion(Question currentQuestion)
        {
            ObjectContext.Questions.AttachAsModified(currentQuestion, ChangeSet.GetOriginal(currentQuestion));
        }

        public void DeleteQuestion(Question question)
        {
            if ((question.EntityState == EntityState.Detached))
            {
                ObjectContext.Questions.Attach(question);
            }
            ObjectContext.Questions.DeleteObject(question);
        }
    }
}