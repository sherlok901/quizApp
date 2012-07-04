using System.ComponentModel.DataAnnotations;

namespace MRZS.Web.Models
{
    // The MetadataTypeAttribute identifies MultimediaContentMetadata as the class
    // that carries additional metadata for the MultimediaContent class.
    [MetadataType(typeof (MultimediaContentMetadata))]
    public partial class MultimediaContent
    {
        // This class allows you to attach custom attributes to properties
        // of the MultimediaContent class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }

        #region Nested type: MultimediaContentMetadata

        internal sealed class MultimediaContentMetadata
        {
            // Metadata classes are not meant to be instantiated.
            private MultimediaContentMetadata()
            {
            }

            public int MultimediaContentId { get; set; }

            public Page Page { get; set; }

            public int? PageId { get; set; }

            public string Path { get; set; }

            public string ContentName { get; set; }

            public bool? IsDeleted { get; set; }
        }

        #endregion
    }
}