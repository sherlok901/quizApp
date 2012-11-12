
namespace MRZS.Web.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using MRZS.Web.Models;


    // Реализует логику приложения с использованием контекста MRZSEntities.
    // TODO: добавьте свою прикладную логику в эти или другие методы.
    // TODO: включите проверку подлинности (Windows/ASP.NET Forms) и раскомментируйте следующие строки, чтобы запретить анонимный доступ
    // Кроме того, рассмотрите возможность добавления ролей для соответствующего ограничения доступа.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public class TestAnswerService : LinqToEntitiesDomainService<MRZSEntities>
    {

        // TODO:
        // рассмотрите возможность сокращения результатов метода запроса.  Если необходим дополнительный ввод,
        // то в этот метод можно добавить параметры или создать дополнительные методы выполнения запроса с другими именами.
        // Для поддержки разбиения на страницы добавьте упорядочение в запрос "TestAnswers".
        public IQueryable<TestAnswer> GetTestAnswers()
        {
            return this.ObjectContext.TestAnswers;
        }

        public void InsertTestAnswer(TestAnswer testAnswer)
        {
            if ((testAnswer.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(testAnswer, EntityState.Added);
            }
            else
            {
                this.ObjectContext.TestAnswers.AddObject(testAnswer);
            }
        }

        public void UpdateTestAnswer(TestAnswer currentTestAnswer)
        {
            this.ObjectContext.TestAnswers.AttachAsModified(currentTestAnswer, this.ChangeSet.GetOriginal(currentTestAnswer));
        }

        public void DeleteTestAnswer(TestAnswer testAnswer)
        {
            if ((testAnswer.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(testAnswer, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.TestAnswers.Attach(testAnswer);
                this.ObjectContext.TestAnswers.DeleteObject(testAnswer);
            }
        }
    }
}


