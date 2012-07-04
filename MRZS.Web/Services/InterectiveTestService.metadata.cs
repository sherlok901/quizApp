using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace MRZS.Web.Models
{
    // The MetadataTypeAttribute identifies InterectiveTestMetadata as the class
    // that carries additional metadata for the InterectiveTest class.
    [MetadataType(typeof (InterectiveTestMetadata))]
    public partial class InterectiveTest
    {
        // This class allows you to attach custom attributes to properties
        // of the InterectiveTest class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }

        #region Nested type: InterectiveTestMetadata

        internal sealed class InterectiveTestMetadata
        {
            // Metadata classes are not meant to be instantiated.
            private InterectiveTestMetadata()
            {
            }

            public EntityCollection<Defect> Defects { get; set; }

            public int InterectiveTestId { get; set; }

            public string TestName { get; set; }

            public string Parameters { get; set; }

            public string PreparetionMessage { get; set; }

            public string TitleText { get; set; }
        }

        #endregion
    }
}