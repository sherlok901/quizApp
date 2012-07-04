using System;
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
    public class InterectiveTestService : LinqToEntitiesDomainService<MRZSEntities>
    {
        private readonly Random randomizer = new Random();
        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'InterectiveTests' query.
        public IQueryable<InterectiveTest> GetInterectiveTests()
        {
            return ObjectContext.InterectiveTests;
        }

        [Query(IsComposable = false)]
        public InterectiveTest GetInterectiveTestById(int interectiveTestId)
        {
            return
                ObjectContext.InterectiveTests.Include("InterectiveTestParameters").FirstOrDefault(
                    t => t.InterectiveTestId == interectiveTestId);
        }

        [Query(IsComposable = false)]
        public InterectiveTest GetInterectiveTestForSectionRandom(int sectionId)
        {
            InterectiveTest[] tests =
                (ObjectContext.InterectiveTests.Include("InterectiveTestParameters").Where(
                    t => t.Sections.Any(s => sectionId == s.SectionId))).ToArray();
            if (tests.Any())
                return tests[randomizer.Next(tests.Length)];
            else
                return null;
        }

        [Query(IsComposable = false)]
        public InterectiveTest GetInterectiveTestByIndex(int sectionId, int index)
        {
            InterectiveTest[] tests =
                (ObjectContext.InterectiveTests.Include("InterectiveTestParameters").Where(
                    t => t.Sections.Any(s => sectionId == s.SectionId))).ToArray();

            if (tests.Length > index)
                return tests[index];
            else
                return null;
        }

        public void InsertInterectiveTest(InterectiveTest interectiveTest)
        {
            if ((interectiveTest.EntityState != EntityState.Detached))
            {
                ObjectContext.ObjectStateManager.ChangeObjectState(interectiveTest, EntityState.Added);
            }
            else
            {
                ObjectContext.InterectiveTests.AddObject(interectiveTest);
            }
        }

        public void UpdateInterectiveTest(InterectiveTest currentInterectiveTest)
        {
            ObjectContext.InterectiveTests.AttachAsModified(currentInterectiveTest,
                                                            ChangeSet.GetOriginal(currentInterectiveTest));
        }

        public void DeleteInterectiveTest(InterectiveTest interectiveTest)
        {
            if ((interectiveTest.EntityState == EntityState.Detached))
            {
                ObjectContext.InterectiveTests.Attach(interectiveTest);
            }
            ObjectContext.InterectiveTests.DeleteObject(interectiveTest);
        }
    }
}