
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
    public class mrzs05mMenuService : LinqToEntitiesDomainService<MRZSEntities>
    {

        // TODO:
        // рассмотрите возможность сокращения результатов метода запроса.  Если необходим дополнительный ввод,
        // то в этот метод можно добавить параметры или создать дополнительные методы выполнения запроса с другими именами.
        // Для поддержки разбиения на страницы добавьте упорядочение в запрос "BooleanVals".
        public IQueryable<BooleanVal> GetBooleanVals()
        {
            return this.ObjectContext.BooleanVals;
        }

        public void InsertBooleanVal(BooleanVal booleanVal)
        {
            if ((booleanVal.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(booleanVal, EntityState.Added);
            }
            else
            {
                this.ObjectContext.BooleanVals.AddObject(booleanVal);
            }
        }

        public void UpdateBooleanVal(BooleanVal currentBooleanVal)
        {
            this.ObjectContext.BooleanVals.AttachAsModified(currentBooleanVal, this.ChangeSet.GetOriginal(currentBooleanVal));
        }

        public void DeleteBooleanVal(BooleanVal booleanVal)
        {
            if ((booleanVal.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(booleanVal, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.BooleanVals.Attach(booleanVal);
                this.ObjectContext.BooleanVals.DeleteObject(booleanVal);
            }
        }

        // TODO:
        // рассмотрите возможность сокращения результатов метода запроса.  Если необходим дополнительный ввод,
        // то в этот метод можно добавить параметры или создать дополнительные методы выполнения запроса с другими именами.
        // Для поддержки разбиения на страницы добавьте упорядочение в запрос "BooleanVal2".
        public IQueryable<BooleanVal2> GetBooleanVal2()
        {
            return this.ObjectContext.BooleanVal2;
        }

        public void InsertBooleanVal2(BooleanVal2 booleanVal2)
        {
            if ((booleanVal2.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(booleanVal2, EntityState.Added);
            }
            else
            {
                this.ObjectContext.BooleanVal2.AddObject(booleanVal2);
            }
        }

        public void UpdateBooleanVal2(BooleanVal2 currentBooleanVal2)
        {
            this.ObjectContext.BooleanVal2.AttachAsModified(currentBooleanVal2, this.ChangeSet.GetOriginal(currentBooleanVal2));
        }

        public void DeleteBooleanVal2(BooleanVal2 booleanVal2)
        {
            if ((booleanVal2.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(booleanVal2, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.BooleanVal2.Attach(booleanVal2);
                this.ObjectContext.BooleanVal2.DeleteObject(booleanVal2);
            }
        }

        // TODO:
        // рассмотрите возможность сокращения результатов метода запроса.  Если необходим дополнительный ввод,
        // то в этот метод можно добавить параметры или создать дополнительные методы выполнения запроса с другими именами.
        // Для поддержки разбиения на страницы добавьте упорядочение в запрос "BooleanVal3".
        public IQueryable<BooleanVal3> GetBooleanVal3()
        {
            return this.ObjectContext.BooleanVal3;
        }

        public void InsertBooleanVal3(BooleanVal3 booleanVal3)
        {
            if ((booleanVal3.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(booleanVal3, EntityState.Added);
            }
            else
            {
                this.ObjectContext.BooleanVal3.AddObject(booleanVal3);
            }
        }

        public void UpdateBooleanVal3(BooleanVal3 currentBooleanVal3)
        {
            this.ObjectContext.BooleanVal3.AttachAsModified(currentBooleanVal3, this.ChangeSet.GetOriginal(currentBooleanVal3));
        }

        public void DeleteBooleanVal3(BooleanVal3 booleanVal3)
        {
            if ((booleanVal3.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(booleanVal3, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.BooleanVal3.Attach(booleanVal3);
                this.ObjectContext.BooleanVal3.DeleteObject(booleanVal3);
            }
        }

        // TODO:
        // рассмотрите возможность сокращения результатов метода запроса.  Если необходим дополнительный ввод,
        // то в этот метод можно добавить параметры или создать дополнительные методы выполнения запроса с другими именами.
        // Для поддержки разбиения на страницы добавьте упорядочение в запрос "kindSignalDCs".
        public IQueryable<kindSignalDC> GetKindSignalDCs()
        {
            return this.ObjectContext.kindSignalDCs;
        }

        public void InsertKindSignalDC(kindSignalDC kindSignalDC)
        {
            if ((kindSignalDC.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(kindSignalDC, EntityState.Added);
            }
            else
            {
                this.ObjectContext.kindSignalDCs.AddObject(kindSignalDC);
            }
        }

        public void UpdateKindSignalDC(kindSignalDC currentkindSignalDC)
        {
            this.ObjectContext.kindSignalDCs.AttachAsModified(currentkindSignalDC, this.ChangeSet.GetOriginal(currentkindSignalDC));
        }

        public void DeleteKindSignalDC(kindSignalDC kindSignalDC)
        {
            if ((kindSignalDC.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(kindSignalDC, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.kindSignalDCs.Attach(kindSignalDC);
                this.ObjectContext.kindSignalDCs.DeleteObject(kindSignalDC);
            }
        }

        // TODO:
        // рассмотрите возможность сокращения результатов метода запроса.  Если необходим дополнительный ввод,
        // то в этот метод можно добавить параметры или создать дополнительные методы выполнения запроса с другими именами.
        // Для поддержки разбиения на страницы добавьте упорядочение в запрос "mrzs05mMenu".
        public IQueryable<mrzs05mMenu> GetMrzs05mMenu()
        {
            return this.ObjectContext.mrzs05mMenu;
        }

        public void InsertMrzs05mMenu(mrzs05mMenu mrzs05mMenu)
        {
            if ((mrzs05mMenu.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mrzs05mMenu, EntityState.Added);
            }
            else
            {
                this.ObjectContext.mrzs05mMenu.AddObject(mrzs05mMenu);
            }
        }

        public void UpdateMrzs05mMenu(mrzs05mMenu currentmrzs05mMenu)
        {
            this.ObjectContext.mrzs05mMenu.AttachAsModified(currentmrzs05mMenu, this.ChangeSet.GetOriginal(currentmrzs05mMenu));
        }

        public void DeleteMrzs05mMenu(mrzs05mMenu mrzs05mMenu)
        {
            if ((mrzs05mMenu.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mrzs05mMenu, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.mrzs05mMenu.Attach(mrzs05mMenu);
                this.ObjectContext.mrzs05mMenu.DeleteObject(mrzs05mMenu);
            }
        }

        // TODO:
        // рассмотрите возможность сокращения результатов метода запроса.  Если необходим дополнительный ввод,
        // то в этот метод можно добавить параметры или создать дополнительные методы выполнения запроса с другими именами.
        // Для поддержки разбиения на страницы добавьте упорядочение в запрос "mrzsInOutOptions".
        public IQueryable<mrzsInOutOption> GetMrzsInOutOptions()
        {
            return this.ObjectContext.mrzsInOutOptions;
        }

        public void InsertMrzsInOutOption(mrzsInOutOption mrzsInOutOption)
        {
            if ((mrzsInOutOption.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mrzsInOutOption, EntityState.Added);
            }
            else
            {
                this.ObjectContext.mrzsInOutOptions.AddObject(mrzsInOutOption);
            }
        }

        public void UpdateMrzsInOutOption(mrzsInOutOption currentmrzsInOutOption)
        {
            this.ObjectContext.mrzsInOutOptions.AttachAsModified(currentmrzsInOutOption, this.ChangeSet.GetOriginal(currentmrzsInOutOption));
        }

        public void DeleteMrzsInOutOption(mrzsInOutOption mrzsInOutOption)
        {
            if ((mrzsInOutOption.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mrzsInOutOption, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.mrzsInOutOptions.Attach(mrzsInOutOption);
                this.ObjectContext.mrzsInOutOptions.DeleteObject(mrzsInOutOption);
            }
        }

        // TODO:
        // рассмотрите возможность сокращения результатов метода запроса.  Если необходим дополнительный ввод,
        // то в этот метод можно добавить параметры или создать дополнительные методы выполнения запроса с другими именами.
        // Для поддержки разбиения на страницы добавьте упорядочение в запрос "mtzVals".
        public IQueryable<mtzVal> GetMtzVals()
        {
            return this.ObjectContext.mtzVals;
        }

        public void InsertMtzVal(mtzVal mtzVal)
        {
            if ((mtzVal.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mtzVal, EntityState.Added);
            }
            else
            {
                this.ObjectContext.mtzVals.AddObject(mtzVal);
            }
        }

        public void UpdateMtzVal(mtzVal currentmtzVal)
        {
            this.ObjectContext.mtzVals.AttachAsModified(currentmtzVal, this.ChangeSet.GetOriginal(currentmtzVal));
        }

        public void DeleteMtzVal(mtzVal mtzVal)
        {
            if ((mtzVal.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mtzVal, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.mtzVals.Attach(mtzVal);
                this.ObjectContext.mtzVals.DeleteObject(mtzVal);
            }
        }

        // TODO:
        // рассмотрите возможность сокращения результатов метода запроса.  Если необходим дополнительный ввод,
        // то в этот метод можно добавить параметры или создать дополнительные методы выполнения запроса с другими именами.
        // Для поддержки разбиения на страницы добавьте упорядочение в запрос "passwordCheckTypes".
        public IQueryable<passwordCheckType> GetPasswordCheckTypes()
        {
            return this.ObjectContext.passwordCheckTypes;
        }

        public void InsertPasswordCheckType(passwordCheckType passwordCheckType)
        {
            if ((passwordCheckType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(passwordCheckType, EntityState.Added);
            }
            else
            {
                this.ObjectContext.passwordCheckTypes.AddObject(passwordCheckType);
            }
        }

        public void UpdatePasswordCheckType(passwordCheckType currentpasswordCheckType)
        {
            this.ObjectContext.passwordCheckTypes.AttachAsModified(currentpasswordCheckType, this.ChangeSet.GetOriginal(currentpasswordCheckType));
        }

        public void DeletePasswordCheckType(passwordCheckType passwordCheckType)
        {
            if ((passwordCheckType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(passwordCheckType, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.passwordCheckTypes.Attach(passwordCheckType);
                this.ObjectContext.passwordCheckTypes.DeleteObject(passwordCheckType);
            }
        }

        // TODO:
        // рассмотрите возможность сокращения результатов метода запроса.  Если необходим дополнительный ввод,
        // то в этот метод можно добавить параметры или создать дополнительные методы выполнения запроса с другими именами.
        // Для поддержки разбиения на страницы добавьте упорядочение в запрос "typeFuncDCs".

        public IQueryable<typeFuncDC> GetTypeFuncDCs()
        {
            return this.ObjectContext.typeFuncDCs;
        }

        public void InsertTypeFuncDC(typeFuncDC typeFuncDC)
        {
            if ((typeFuncDC.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(typeFuncDC, EntityState.Added);
            }
            else
            {
                this.ObjectContext.typeFuncDCs.AddObject(typeFuncDC);
            }
        }

        public void UpdateTypeFuncDC(typeFuncDC currenttypeFuncDC)
        {
            this.ObjectContext.typeFuncDCs.AttachAsModified(currenttypeFuncDC, this.ChangeSet.GetOriginal(currenttypeFuncDC));
        }

        public void DeleteTypeFuncDC(typeFuncDC typeFuncDC)
        {
            if ((typeFuncDC.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(typeFuncDC, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.typeFuncDCs.Attach(typeFuncDC);
                this.ObjectContext.typeFuncDCs.DeleteObject(typeFuncDC);
            }
        }

        // TODO:
        // рассмотрите возможность сокращения результатов метода запроса.  Если необходим дополнительный ввод,
        // то в этот метод можно добавить параметры или создать дополнительные методы выполнения запроса с другими именами.
        // Для поддержки разбиения на страницы добавьте упорядочение в запрос "typeSignalDCs".
        public IQueryable<typeSignalDC> GetTypeSignalDCs()
        {
            return this.ObjectContext.typeSignalDCs;
        }

        public void InsertTypeSignalDC(typeSignalDC typeSignalDC)
        {
            if ((typeSignalDC.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(typeSignalDC, EntityState.Added);
            }
            else
            {
                this.ObjectContext.typeSignalDCs.AddObject(typeSignalDC);
            }
        }

        public void UpdateTypeSignalDC(typeSignalDC currenttypeSignalDC)
        {
            this.ObjectContext.typeSignalDCs.AttachAsModified(currenttypeSignalDC, this.ChangeSet.GetOriginal(currenttypeSignalDC));
        }

        public void DeleteTypeSignalDC(typeSignalDC typeSignalDC)
        {
            if ((typeSignalDC.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(typeSignalDC, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.typeSignalDCs.Attach(typeSignalDC);
                this.ObjectContext.typeSignalDCs.DeleteObject(typeSignalDC);
            }
        }
    }
}


