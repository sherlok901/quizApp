using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace MRZS.Web.Models
{
    // The MetadataTypeAttribute identifies QuestionMetadata as the class
    // that carries additional metadata for the Question class.
    [MetadataType(typeof (QuestionMetadata))]
    public partial class Question
    {
        // This class allows you to attach custom attributes to properties
        // of the Question class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }

        #region Nested type: QuestionMetadata

        internal sealed class QuestionMetadata
        {
            // Metadata classes are not meant to be instantiated.
            private QuestionMetadata()
            {
            }

            public EntityCollection<Answer> Answers { get; set; }

            public int? Difficulty { get; set; }

            public int QuestionId { get; set; }

            public Quiz Quiz { get; set; }

            public int QuizId { get; set; }

            public EntityCollection<ResultAnswer> ResultAnswers { get; set; }

            public string Text { get; set; }

            public bool IsDeleted { get; set; }
        }

        #endregion
    }
}