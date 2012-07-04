using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices.Server;
using System.Web.Profile;
using System.Web.Security;
using MRZS.Web.Models;
using MRZS.Web.Resources;

namespace MRZS.Web
{
    /// <summary>
    ///   RIA Services Domain Service that exposes methods for performing user
    ///   registrations.
    /// </summary>
    [EnableClientAccess]
    public class UserRegistrationService : DomainService
    {
        /// <summary>
        /// Role to which users will be added by default.
        /// </summary>
        public const string DefaultRole = "Registered Users";

        //// NOTE: This is a sample code to get your application started. In the production code you would 
        //// want to provide a mitigation against a denial of service attack by providing CAPTCHA 
        //// control functionality or verifying user's email address.

        /// <summary>
        /// Adds a new user with the supplied <see cref="RegistrationData"/> and <paramref name="password"/>.
        /// </summary>
        /// <param name="user">The registration information for this user.</param>
        /// <param name="password">The password for the new user.</param>
        [Invoke(HasSideEffects = true)]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public CreateUserStatus CreateUser(RegistrationData user,
                                           [Required(ErrorMessageResourceName = "ValidationErrorRequiredField",
                                               ErrorMessageResourceType = typeof (ValidationErrorResources))] [StringLength(50, MinimumLength = 1,
                                               ErrorMessageResourceName = "ValidationErrorBadPasswordLength",
                                               ErrorMessageResourceType = typeof (ValidationErrorResources))] string
                                               password)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            // Run this BEFORE creating the user to make sure roles are enabled and the default role
            // will be available
            //
            // If there are a problem with the role manager it is better to fail now than to have it
            // happening after the user is created
            if (!Roles.RoleExists(DefaultRole))
            {
                Roles.CreateRole(DefaultRole);
            }

            // NOTE: ASP.NET by default uses SQL Server Express to create the user database. 
            // CreateUser will fail if you do not have SQL Server Express installed.
            MembershipCreateStatus createStatus;
            Membership.CreateUser(user.UserName, password, user.Email, user.Question, user.Answer, true, null,
                                  out createStatus);

            if (createStatus != MembershipCreateStatus.Success)
            {
                return ConvertStatus(createStatus);
            }

            var model = new MRZSEntities();
            aspnet_Users createdUser = model.aspnet_Users.SingleOrDefault(u => u.UserName == user.UserName);
            var adUser = new Models.User();
            adUser.FirstName = user.FirstName;
            adUser.LastName = user.LastName;
            adUser.aspnet_UserId = createdUser.UserId;
            if (user.GroupId > 0)
            {
                adUser.GroupId = user.GroupId;
            }
            model.Users.AddObject(adUser);
            model.SaveChanges();

            // Assign it to the default role
            // This *can* fail but only if role management is disabled
            if (!string.IsNullOrEmpty(user.UserRole))
                Roles.AddUserToRole(user.UserName, user.UserRole);

            // Set its friendly name (profile setting)
            // This *can* fail but only if Web.config is configured incorrectly 
            ProfileBase profile = ProfileBase.Create(user.UserName, true);
            profile.SetPropertyValue("FriendlyName", user.FriendlyName);
            profile.Save();

            return CreateUserStatus.Success;
        }

        [Invoke(HasSideEffects = true)]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public void EditUser(RegistrationData user,
                             [StringLength(50, MinimumLength = 1,
                                 ErrorMessageResourceName = "ValidationErrorBadPasswordLength",
                                 ErrorMessageResourceType = typeof (ValidationErrorResources))] string password)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var model = new MRZSEntities();
            Models.User createdUser = model.Users.SingleOrDefault(u => u.UserId == user.UserId);
            createdUser.FirstName = user.FirstName;
            createdUser.LastName = user.LastName;
            if (user.GroupId > 0)
            {
                createdUser.GroupId = user.GroupId;
            }
            model.SaveChanges();
            if (!string.IsNullOrEmpty(user.UserRole))
            {
                if (Roles.IsUserInRole(user.UserName, user.UserRole))
                    return;
                string[] userRoles = Roles.GetRolesForUser(user.UserName);
                if (userRoles.Length > 0)
                    Roles.RemoveUserFromRoles(user.UserName, userRoles);
                Roles.AddUserToRole(user.UserName, user.UserRole);
            }
        }

        /// <summary>
        /// Query method that exposes the <see cref="RegistrationData"/> class to Silverlight client code.
        /// </summary>
        /// <remarks>
        /// This query method is not used and will throw <see cref="NotSupportedException"/> if called.
        /// Its primary job is to indicate the <see cref="RegistrationData"/> class should be made
        /// available to the Silverlight client.
        /// </remarks>
        /// <returns>Not applicable.</returns>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"),
         SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public IEnumerable<RegistrationData> GetUsers()
        {
            throw new NotSupportedException();
        }

        private static CreateUserStatus ConvertStatus(MembershipCreateStatus createStatus)
        {
            switch (createStatus)
            {
                case MembershipCreateStatus.Success:
                    return CreateUserStatus.Success;
                case MembershipCreateStatus.InvalidUserName:
                    return CreateUserStatus.InvalidUserName;
                case MembershipCreateStatus.InvalidPassword:
                    return CreateUserStatus.InvalidPassword;
                case MembershipCreateStatus.InvalidQuestion:
                    return CreateUserStatus.InvalidQuestion;
                case MembershipCreateStatus.InvalidAnswer:
                    return CreateUserStatus.InvalidAnswer;
                case MembershipCreateStatus.InvalidEmail:
                    return CreateUserStatus.InvalidEmail;
                case MembershipCreateStatus.DuplicateUserName:
                    return CreateUserStatus.DuplicateUserName;
                case MembershipCreateStatus.DuplicateEmail:
                    return CreateUserStatus.DuplicateEmail;
                default:
                    return CreateUserStatus.Failure;
            }
        }
    }

    /// <summary>
    /// An enumeration of the values that can be returned from <see cref="UserRegistrationService.CreateUser"/>
    /// </summary>
    public enum CreateUserStatus
    {
        Success = 0,
        InvalidUserName = 1,
        InvalidPassword = 2,
        InvalidQuestion = 3,
        InvalidAnswer = 4,
        InvalidEmail = 5,
        DuplicateUserName = 6,
        DuplicateEmail = 7,
        Failure = 8,
    }
}