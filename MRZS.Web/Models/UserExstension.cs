using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MRZS.Web.Models
{
    public partial class User
    {
        [DataMember]
        public string UserLogin
        {
            get
            {
                if (aspnet_Users == null)
                    return "";

                return aspnet_Users.UserName;
            }
        }

        [DataMember]
        public DateTime? LastActivityDate
        {
            get
            {
                if (aspnet_Users == null)
                    return null;

                return aspnet_Users.LastActivityDate;
            }
        }

        [DataMember]
        public string FullName
        {
            get { return string.Format("{0} {1}", LastName, FirstName); }
        }

        [DataMember]
        public string GroupName
        {
            get
            {
                if (!GroupReference.IsLoaded)
                    GroupReference.Load();

                return Group != null ? Group.GroupName : string.Empty;
            }
        }

        [DataMember]
        public string UserRole
        {
            get
            {
                if (aspnet_Users == null)
                    return "";
                if (!aspnet_Users.aspnet_Roles.IsLoaded)
                    aspnet_Users.aspnet_Roles.Load();
                var roles = new StringBuilder();
                foreach (aspnet_Roles role in aspnet_Users.aspnet_Roles)
                {
                    if (roles.Length != 0)
                        roles.Append(", ");
                    roles.Append(role.RoleName);
                }
                return roles.ToString();
            }
        }

        [DataMember]
        public string CurrentSection
        {
            get
            {
                if (!UserSections.IsLoaded)
                    UserSections.Load();
                UserSection section = UserSections.FirstOrDefault(s => s.IsPassed != true);
                if (section == null)
                    return string.Empty;
                section.SectionReference.Load();
                return section.Section.Name;
            }
        }

        [DataMember]
        public string CurrentBook
        {
            get
            {
                if (!UserSections.IsLoaded)
                    UserSections.Load();
                UserSection section = UserSections.FirstOrDefault(s => s.IsPassed != true);
                if (section == null)
                    return string.Empty;
                section.SectionReference.Load();
                if (!section.Section.BookReference.IsLoaded)
                    section.Section.BookReference.Load();
                return section.Section.Book.Name;
            }
        }
    }
}