using System.ServiceModel.DomainServices.Server.ApplicationServices;

namespace MRZS.Web
{
    /// <summary>
    /// Class containing information about the authenticated user.
    /// </summary>
    public partial class User : UserBase
    {
        //// NOTE: Profile properties can be added for use in Silverlight application.
        //// To enable profiles, edit the appropriate section of web.config file.
        ////
        //// public string MyProfileProperty { get; set; }

        /// <summary>
        /// Gets and sets the friendly name of the user.
        /// </summary>
        public string FriendlyName { get; set; }

        //[DataMemberAttribute()]
        //public string FirstName { get; set; }

        //[DataMemberAttribute()]
        //public string Lastname { get; set; }
    }
}