using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace MRZS.Web.Models
{
    // The MetadataTypeAttribute identifies AnswerMetadata as the class
    // that carries additional metadata for the Answer class.
    [MetadataType(typeof (AnswerMetadata))]
    public partial class Answer
    {
        // This class allows you to attach custom attributes to properties
        // of the Answer class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }

        #region Nested type: AnswerMetadata

        internal sealed class AnswerMetadata
        {
            // Metadata classes are not meant to be instantiated.
            private AnswerMetadata()
            {
            }

            public int AnswerId { get; set; }

            public string AnswerText { get; set; }

            public bool IsCorrect { get; set; }

            public Question Question { get; set; }

            public int QuestionId { get; set; }

            public bool IsDeleted { get; set; }

            public EntityCollection<ResultAnswer> ResultAnswers { get; set; }
        }

        #endregion
    }
}