using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace MRZS.Web.Models
{
    // The MetadataTypeAttribute identifies DefectMetadata as the class
    // that carries additional metadata for the Defect class.
    [MetadataType(typeof (DefectMetadata))]
    public partial class Defect
    {
        // This class allows you to attach custom attributes to properties
        // of the Defect class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }

        #region Nested type: DefectMetadata

        internal sealed class DefectMetadata
        {
            // Metadata classes are not meant to be instantiated.
            private DefectMetadata()
            {
            }

            public int DefectId { get; set; }

            public InterectiveTest InterectiveTest { get; set; }

            public int InterectiveTestId { get; set; }

            public bool IsCorrect { get; set; }

            public EntityCollection<Repairman> Repairmen { get; set; }

            public string Text { get; set; }
        }

        #endregion
    }
}