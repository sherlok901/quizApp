﻿
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
    public class TestQuestionService : LinqToEntitiesDomainService<MRZSEntities>
    {

        // TODO:
        // рассмотрите возможность сокращения результатов метода запроса.  Если необходим дополнительный ввод,
        // то в этот метод можно добавить параметры или создать дополнительные методы выполнения запроса с другими именами.
        // Для поддержки разбиения на страницы добавьте упорядочение в запрос "TestQuestions".
        public IQueryable<TestQuestion> GetTestQuestions()
        {
            return this.ObjectContext.TestQuestions;
        }

        public void InsertTestQuestion(TestQuestion testQuestion)
        {
            if ((testQuestion.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(testQuestion, EntityState.Added);
            }
            else
            {
                this.ObjectContext.TestQuestions.AddObject(testQuestion);
            }
        }

        public void UpdateTestQuestion(TestQuestion currentTestQuestion)
        {
            this.ObjectContext.TestQuestions.AttachAsModified(currentTestQuestion, this.ChangeSet.GetOriginal(currentTestQuestion));
        }

        public void DeleteTestQuestion(TestQuestion testQuestion)
        {
            if ((testQuestion.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(testQuestion, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.TestQuestions.Attach(testQuestion);
                this.ObjectContext.TestQuestions.DeleteObject(testQuestion);
            }
        }
    }
}


