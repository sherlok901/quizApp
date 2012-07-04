using System.ComponentModel.DataAnnotations;

namespace MRZS.Web.Models
{
    // The MetadataTypeAttribute identifies UserSectionMetadata as the class
    // that carries additional metadata for the UserSection class.
    [MetadataType(typeof (UserSectionMetadata))]
    public partial class UserSection
    {
        // This class allows you to attach custom attributes to properties
        // of the UserSection class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }

        #region Nested type: UserSectionMetadata

        internal sealed class UserSectionMetadata
        {
            // Metadata classes are not meant to be instantiated.
            private UserSectionMetadata()
            {
            }

            public bool? IsPassed { get; set; }

            public Section Section { get; set; }

            public int SectionId { get; set; }

            public User User { get; set; }

            public int UserId { get; set; }

            public int UserSectionId { get; set; }
        }

        #endregion
    }
}