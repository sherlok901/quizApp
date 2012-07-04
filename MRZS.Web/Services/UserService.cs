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
    public class UserService : LinqToEntitiesDomainService<MRZSEntities>
    {
        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Users' query.
        public IQueryable<Models.User> GetUsers()
        {
            return ObjectContext.Users.Include("aspnet_Users").Where(u => u.IsDeleted == false || u.IsDeleted == null);
                //.Include("aspnet_Roles");
        }

        public IQueryable<Models.User> GetStudents()
        {
            return ObjectContext.Users.Include("aspnet_Users").Include("Group").
                Where(u => u.GroupId != null && (u.IsDeleted == false || u.IsDeleted == null));
        }

        public IQueryable<Models.User> GetStudentsFroGroup(int groupId)
        {
            return
                ObjectContext.Users.Include("aspnet_Users").Include("Group").Where(
                    u => u.GroupId != null && (u.IsDeleted == false || u.IsDeleted == null) && u.GroupId == groupId);
        }

        [Query(IsComposable = false)]
        public Models.User GetUserById(int userId)
        {
            Models.User user =
                ObjectContext.Users.Include("aspnet_Users").ToArray().SingleOrDefault(
                    u => u.UserId == userId && (u.IsDeleted == false || u.IsDeleted == null));
            return user;
        }

        [Query(IsComposable = false)]
        public Models.User GetUserByAspUserName(string aspUser_Name)
        {
            return (from user in ObjectContext.Users
                    join aspUser in ObjectContext.aspnet_Users on user.aspnet_UserId equals aspUser.UserId
                    where aspUser.UserName == aspUser_Name && (user.IsDeleted == false || user.IsDeleted == null)
                    select user).FirstOrDefault();
        }

        public void InsertUser(Models.User user)
        {
            if ((user.EntityState != EntityState.Detached))
            {
                ObjectContext.ObjectStateManager.ChangeObjectState(user, EntityState.Added);
            }
            else
            {
                ObjectContext.Users.AddObject(user);
            }
        }

        public void UpdateUser(Models.User currentUser)
        {
            ObjectContext.Users.AttachAsModified(currentUser, ChangeSet.GetOriginal(currentUser));
        }

        public void DeleteUser(Models.User user)
        {
            if ((user.EntityState == EntityState.Detached))
            {
                ObjectContext.Users.Attach(user);
            }
            ObjectContext.Users.DeleteObject(user);
        }
    }
}