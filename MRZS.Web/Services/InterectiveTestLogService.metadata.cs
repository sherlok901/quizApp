using System;
using System.ComponentModel.DataAnnotations;

namespace MRZS.Web.Models
{
    // The MetadataTypeAttribute identifies InterectiveTestLogMetadata as the class
    // that carries additional metadata for the InterectiveTestLog class.
    [MetadataType(typeof (InterectiveTestLogMetadata))]
    public partial class InterectiveTestLog
    {
        // This class allows you to attach custom attributes to properties
        // of the InterectiveTestLog class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }

        #region Nested type: InterectiveTestLogMetadata

        internal sealed class InterectiveTestLogMetadata
        {
            // Metadata classes are not meant to be instantiated.
            private InterectiveTestLogMetadata()
            {
            }

            public int Attempts { get; set; }

            public string BookName { get; set; }

            public DateTime? EndTime { get; set; }

            public InterectiveTest InterectiveTest { get; set; }

            public int InterectiveTestId { get; set; }

            public int InterectiveTestLogId { get; set; }

            public string SectionName { get; set; }

            public DateTime StartTime { get; set; }

            public int UserId { get; set; }
        }

        #endregion
    }
}