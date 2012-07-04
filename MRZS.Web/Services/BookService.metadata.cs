using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace MRZS.Web.Models
{
    // The MetadataTypeAttribute identifies BookMetadata as the class
    // that carries additional metadata for the Book class.
    [MetadataType(typeof (BookMetadata))]
    public partial class Book
    {
        // This class allows you to attach custom attributes to properties
        // of the Book class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }

        #region Nested type: BookMetadata

        internal sealed class BookMetadata
        {
            // Metadata classes are not meant to be instantiated.
            private BookMetadata()
            {
            }

            public int BookId { get; set; }

            public byte[] Name { get; set; }

            public EntityCollection<Section> Sections { get; set; }

            public bool IsDeleted { get; set; }
        }

        #endregion
    }
}