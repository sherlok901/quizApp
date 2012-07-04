using System.ComponentModel.DataAnnotations;

namespace MRZS.Web.Models
{
    // The MetadataTypeAttribute identifies RepairmanMetadata as the class
    // that carries additional metadata for the Repairman class.
    [MetadataType(typeof (RepairmanMetadata))]
    public partial class Repairman
    {
        // This class allows you to attach custom attributes to properties
        // of the Repairman class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }

        #region Nested type: RepairmanMetadata

        internal sealed class RepairmanMetadata
        {
            // Metadata classes are not meant to be instantiated.
            private RepairmanMetadata()
            {
            }

            public Defect Defect { get; set; }

            public int DefectId { get; set; }

            public bool IsCorrect { get; set; }

            public int RepeirmenId { get; set; }

            public string Text { get; set; }
        }

        #endregion
    }
}