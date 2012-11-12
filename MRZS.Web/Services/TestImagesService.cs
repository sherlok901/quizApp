
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
    public class TestImagesService : LinqToEntitiesDomainService<MRZSEntities>
    {

        // TODO:
        // рассмотрите возможность сокращения результатов метода запроса.  Если необходим дополнительный ввод,
        // то в этот метод можно добавить параметры или создать дополнительные методы выполнения запроса с другими именами.
        // Для поддержки разбиения на страницы добавьте упорядочение в запрос "TestingImages".
        public IQueryable<TestingImage> GetTestingImages()
        {
            return this.ObjectContext.TestingImages;
        }

        public void InsertTestingImage(TestingImage testingImage)
        {
            if ((testingImage.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(testingImage, EntityState.Added);
            }
            else
            {
                this.ObjectContext.TestingImages.AddObject(testingImage);
            }
        }

        public void UpdateTestingImage(TestingImage currentTestingImage)
        {
            this.ObjectContext.TestingImages.AttachAsModified(currentTestingImage, this.ChangeSet.GetOriginal(currentTestingImage));
        }

        public void DeleteTestingImage(TestingImage testingImage)
        {
            if ((testingImage.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(testingImage, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.TestingImages.Attach(testingImage);
                this.ObjectContext.TestingImages.DeleteObject(testingImage);
            }
        }
    }
}


