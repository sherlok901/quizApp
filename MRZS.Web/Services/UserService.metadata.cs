using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace MRZS.Web.Models
{
    // The MetadataTypeAttribute identifies UserMetadata as the class
    // that carries additional metadata for the User class.
    [MetadataType(typeof (UserMetadata))]
    public partial class User
    {
        // This class allows you to attach custom attributes to properties
        // of the User class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }

        #region Nested type: UserMetadata

        internal sealed class UserMetadata
        {
            // Metadata classes are not meant to be instantiated.
            private UserMetadata()
            {
            }

            public Guid aspnet_UserId { get; set; }

            public aspnet_Users aspnet_Users { get; set; }

            public Group Group { get; set; }

            public int? GroupId { get; set; }

            public EntityCollection<QuizResult> QuizResults { get; set; }

            public int UserId { get; set; }

            public EntityCollection<UserSection> UserSections { get; set; }

            public bool IsDeleted { get; set; }
        }

        #endregion
    }
}