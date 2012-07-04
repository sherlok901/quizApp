using System;
using System.ComponentModel.DataAnnotations;

namespace MRZS.Web.Models
{
    // The MetadataTypeAttribute identifies ResultAnswerMetadata as the class
    // that carries additional metadata for the ResultAnswer class.
    [MetadataType(typeof (ResultAnswerMetadata))]
    public partial class ResultAnswer
    {
        // This class allows you to attach custom attributes to properties
        // of the ResultAnswer class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }

        #region Nested type: ResultAnswerMetadata

        internal sealed class ResultAnswerMetadata
        {
            // Metadata classes are not meant to be instantiated.
            private ResultAnswerMetadata()
            {
            }

            public Answer Answer { get; set; }

            public int? AnswerId { get; set; }

            public DateTime? AnswerTime { get; set; }

            public Question Question { get; set; }

            public int QuestionId { get; set; }

            public int ResultAnswerId { get; set; }

            public int QuizResultId { get; set; }
        }

        #endregion
    }
}