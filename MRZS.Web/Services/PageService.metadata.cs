using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace MRZS.Web.Models
{
    // The MetadataTypeAttribute identifies PageMetadata as the class
    // that carries additional metadata for the Page class.
    [MetadataType(typeof (PageMetadata))]
    public partial class Page
    {
        // This class allows you to attach custom attributes to properties
        // of the Page class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }

        #region Nested type: PageMetadata

        internal sealed class PageMetadata
        {
            // Metadata classes are not meant to be instantiated.
            private PageMetadata()
            {
            }

            public EntityCollection<MultimediaContent> MultimediaContents { get; set; }

            public string PageContent { get; set; }

            public int PageId { get; set; }

            public string PagePath { get; set; }

            public Section Section { get; set; }

            public int SectionId { get; set; }

            public int? PageOrder { get; set; }

            public bool? IsLastPage { get; set; }

            public bool IsDeleted { get; set; }
        }

        #endregion
    }
}