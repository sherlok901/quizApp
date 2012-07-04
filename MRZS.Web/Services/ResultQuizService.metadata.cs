using System;
using System.ComponentModel.DataAnnotations;

namespace MRZS.Web.Models
{
    // The MetadataTypeAttribute identifies QuizResultMetadata as the class
    // that carries additional metadata for the QuizResult class.
    [MetadataType(typeof (QuizResultMetadata))]
    public partial class QuizResult
    {
        // This class allows you to attach custom attributes to properties
        // of the QuizResult class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }

        #region Nested type: QuizResultMetadata

        internal sealed class QuizResultMetadata
        {
            // Metadata classes are not meant to be instantiated.
            private QuizResultMetadata()
            {
            }

            public DateTime? EndTime { get; set; }

            public Quiz Quiz { get; set; }

            public int QuizId { get; set; }

            public int QuizResultId { get; set; }

            public DateTime StartTime { get; set; }

            public User User { get; set; }

            public int UserId { get; set; }

            public decimal CorrectAnswers { get; set; }

            public string DurationString { get; set; }

            public string BookName { get; set; }
        }

        #endregion
    }
}