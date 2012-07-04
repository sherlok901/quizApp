using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace MRZS.Web.Models
{
    // The MetadataTypeAttribute identifies GroupMetadata as the class
    // that carries additional metadata for the Group class.
    [MetadataType(typeof (GroupMetadata))]
    public partial class Group
    {
        // This class allows you to attach custom attributes to properties
        // of the Group class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }

        #region Nested type: GroupMetadata

        internal sealed class GroupMetadata
        {
            // Metadata classes are not meant to be instantiated.
            private GroupMetadata()
            {
            }

            public int GroupId { get; set; }

            public string GroupName { get; set; }

            public EntityCollection<User> Users { get; set; }

            public bool IsDeleted { get; set; }
        }

        #endregion
    }
}