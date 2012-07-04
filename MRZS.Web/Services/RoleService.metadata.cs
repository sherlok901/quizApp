using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace MRZS.Web.Models
{
    // The MetadataTypeAttribute identifies aspnet_RolesMetadata as the class
    // that carries additional metadata for the aspnet_Roles class.
    [MetadataType(typeof (aspnet_RolesMetadata))]
    public partial class aspnet_Roles
    {
        // This class allows you to attach custom attributes to properties
        // of the aspnet_Roles class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }

        #region Nested type: aspnet_RolesMetadata

        internal sealed class aspnet_RolesMetadata
        {
            // Metadata classes are not meant to be instantiated.
            private aspnet_RolesMetadata()
            {
            }

            public Guid ApplicationId { get; set; }

            public aspnet_Applications aspnet_Applications { get; set; }

            public EntityCollection<aspnet_Users> aspnet_Users { get; set; }

            public string Description { get; set; }

            public string LoweredRoleName { get; set; }

            public Guid RoleId { get; set; }

            public string RoleName { get; set; }
        }

        #endregion
    }
}