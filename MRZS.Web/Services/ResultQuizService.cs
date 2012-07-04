using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel.DomainServices.EntityFramework;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices.Server;
using MRZS.Web.Models;

namespace MRZS.Web.Services
{
    // Implements application logic using the MRZSEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess]
    public class ResultQuizService : LinqToEntitiesDomainService<MRZSEntities>
    {
        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'QuizResults' query.
        public IQueryable<QuizResult> GetQuizResults()
        {
            return ObjectContext.QuizResults;
        }

        public IQueryable<QuestionLog> GetStudentQuestionLog(int resultQuizId)
        {
            return from quizResult in ObjectContext.QuizResults
                   join question in ObjectContext.Questions on quizResult.QuizId equals question.QuizId
                   join resultAnswer in ObjectContext.ResultAnswers on question.QuestionId equals
                       resultAnswer.QuestionId
                   join answer in ObjectContext.Answers on resultAnswer.AnswerId equals answer.AnswerId
                   where quizResult.QuizResultId == resultQuizId
                         && answer.QuestionId == question.QuestionId
                         && resultAnswer.QuizResultId == resultQuizId
                   select new QuestionLog
                              {
                                  Id = question.QuestionId,
                                  QuestionText = question.Text,
                                  IsCorrect = answer.IsCorrect
                              };
        }

        [Query(IsComposable = false)]
        public QuizLogDTO GetLastQuizResultsForUser(int userId)
        {
            return GetAllQuizResultsForUser(userId).OrderBy(r => r.StartTime).ToArray().LastOrDefault(t => t.QuizId > 0);
        }

        public IQueryable<QuizResult> GetQuizResultsForUser(int userId)
        {
            return from user in ObjectContext.Users
                   join userSection in ObjectContext.UserSections on user.UserId equals userSection.UserId
                   join section in ObjectContext.Sections on userSection.SectionId equals section.SectionId
                   join quiz in ObjectContext.Quizs on section.SectionId equals quiz.SectionId
                   join quizResult in ObjectContext.QuizResults on quiz.QuizId equals quizResult.QuizId
                   where
                       user.UserId == userId && (quiz.IsDeleted == false || quiz.IsDeleted == null) &&
                       quizResult.UserId == userId
                   select quizResult;
        }

        public IQueryable<QuizLogDTO> GetAllQuizResultsForUser(int userId)
        {
            QuizResult[] quizes = (from user in ObjectContext.Users
                                   join userSection in ObjectContext.UserSections on user.UserId equals
                                       userSection.UserId
                                   join section in ObjectContext.Sections on userSection.SectionId equals
                                       section.SectionId
                                   join book in ObjectContext.Books on section.BookId equals book.BookId
                                   join quiz in ObjectContext.Quizs on section.SectionId equals quiz.SectionId
                                   join quizResult in ObjectContext.QuizResults on
                                       new {quizId = quiz.QuizId, userId} equals
                                       new {quizId = quizResult.QuizId, userId = quizResult.UserId}
                                   where
                                       user.UserId == userId && (quiz.IsDeleted == false || quiz.IsDeleted == null) &&
                                       quizResult.UserId == userId
                                   select quizResult).ToArray();

            List<QuizLogDTO> quizResults = (from res in quizes
                                            select new QuizLogDTO
                                                       {
                                                           Id = Guid.NewGuid(),
                                                           QuizId = res.QuizId,
                                                           EntityId = res.QuizResultId,
                                                           BookName = res.BookName,
                                                           SectionName = res.SectionName,
                                                           DurationString = res.DurationString,
                                                           StartTime = res.StartTime,
                                                           CorrectAnswersString = res.CorrectAnswers.ToString("F2")
                                                       }
                                           ).ToList();

            InterectiveTestLog[] interectiveTests = (from section in ObjectContext.Sections
                                                     join book in ObjectContext.Books on section.BookId equals
                                                         book.BookId
                                                     join user in ObjectContext.Users on userId equals user.UserId
                                                     join userSection in ObjectContext.UserSections on
                                                         new {user.UserId, section.SectionId} equals
                                                         new {userSection.UserId, userSection.SectionId}
                                                     join interectiveTestResult in ObjectContext.InterectiveTestLogs on
                                                         new {sectionId = (int?) section.SectionId, userId} equals
                                                         new
                                                             {
                                                                 sectionId = interectiveTestResult.SectionId,
                                                                 userId = interectiveTestResult.UserId
                                                             }
                                                     where user.UserId == userId
                                                     group interectiveTestResult by interectiveTestResult
                                                     into res
                                                     select res.Key).ToArray();

            quizResults.AddRange((from test in interectiveTests
                                  select new QuizLogDTO
                                             {
                                                 Id = Guid.NewGuid(),
                                                 BookName = test.BookName,
                                                 SectionName = test.SectionName,
                                                 DurationString =
                                                     test.EndTime.HasValue
                                                         ? new TimeSpan(test.EndTime.Value.Ticks - test.StartTime.Ticks >
                                                                        0
                                                                            ? test.EndTime.Value.Ticks -
                                                                              test.StartTime.Ticks
                                                                            : 0).Seconds.ToString()
                                                         : "0",
                                                 StartTime = test.StartTime,
                                                 CorrectAnswersString = test.Attempts.ToString()
                                             }).ToArray());
            return quizResults.OrderByDescending(q => q.StartTime).AsQueryable();
        }

        public IQueryable<QuizResult> GetQuizResultById(int quizResultId)
        {
            return ObjectContext.QuizResults.Where(qr => qr.QuizResultId == quizResultId);
        }

        [Invoke]
        public decimal GetStudentScore(int quizResultId)
        {
            return ObjectContext.QuizResults.Where(qr => qr.QuizResultId == quizResultId).First().CorrectAnswers;
        }

        public IQueryable<QuizResult> GetQuizResultByQuizId(int quizId)
        {
            return ObjectContext.QuizResults.Where(qr => qr.QuizId == quizId);
        }

        public void InsertQuizResult(QuizResult quizResult)
        {
            if ((quizResult.EntityState != EntityState.Detached))
            {
                ObjectContext.ObjectStateManager.ChangeObjectState(quizResult, EntityState.Added);
            }
            else
            {
                ObjectContext.QuizResults.AddObject(quizResult);
            }
        }

        public void UpdateQuizResult(QuizResult currentQuizResult)
        {
            ObjectContext.QuizResults.AttachAsModified(currentQuizResult, ChangeSet.GetOriginal(currentQuizResult));
        }

        public void DeleteQuizResult(QuizResult quizResult)
        {
            if ((quizResult.EntityState == EntityState.Detached))
            {
                ObjectContext.QuizResults.Attach(quizResult);
            }
            ObjectContext.QuizResults.DeleteObject(quizResult);
        }
    }

    public class QuizLogDTOComparer : IEqualityComparer<QuizLogDTO>
    {
        #region IEqualityComparer<QuizLogDTO> Members

        public bool Equals(QuizLogDTO x, QuizLogDTO y)
        {
            return x.BookName == y.BookName
                   && x.StartTime == y.StartTime
                   && x.QuizId == y.QuizId
                   && x.SectionName == y.SectionName
                   && x.DurationString == y.DurationString;
        }

        public int GetHashCode(QuizLogDTO obj)
        {
            return obj.StartTime.GetHashCode() ^ obj.QuizId.GetHashCode() ^
                   obj.SectionName.GetHashCode() ^ obj.DurationString.GetHashCode();
        }

        #endregion
    }
}