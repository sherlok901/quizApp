
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
    public class BooleanVal1 : LinqToEntitiesDomainService<MRZSEntities>
    {        
        [Query(IsComposable = false)]
        public BooleanVal GetBooleanValByID(int boolValID)
        {
            return
                //ObjectContext.Books.Include("Sections").Where(b => b.IsDeleted == null || b.IsDeleted == false).
                //    SingleOrDefault(b => b.BookId == bookId);
            ObjectContext.BooleanVals.Where(b => b.id == boolValID).SingleOrDefault(b => b.id == boolValID);
        }
        public IQueryable<BooleanVal> GetBooleanValByID2(int boolValID)
        {
            return this.ObjectContext.BooleanVals;
        }
    }
}


