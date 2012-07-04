using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace MRZS.Web.Models
{
    // The MetadataTypeAttribute identifies SectionMetadata as the class
    // that carries additional metadata for the Section class.
    [MetadataType(typeof (SectionMetadata))]
    public partial class Section
    {
        // This class allows you to attach custom attributes to properties
        // of the Section class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }

        #region Nested type: SectionMetadata

        internal sealed class SectionMetadata
        {
            // Metadata classes are not meant to be instantiated.
            private SectionMetadata()
            {
            }

            public Book Book { get; set; }

            public int BookId { get; set; }

            public string Name { get; set; }

            public EntityCollection<Page> Pages { get; set; }

            public EntityCollection<Quiz> Quizs { get; set; }

            public int SectionId { get; set; }

            public EntityCollection<UserSection> UserSections { get; set; }

            public string BookName { get; set; }

            public bool IsCurrent { get; set; }
        }

        #endregion
    }
}