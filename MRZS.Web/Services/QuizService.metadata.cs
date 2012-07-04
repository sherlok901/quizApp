using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace MRZS.Web.Models
{
    // The MetadataTypeAttribute identifies QuizMetadata as the class
    // that carries additional metadata for the Quiz class.
    [MetadataType(typeof (QuizMetadata))]
    public partial class Quiz
    {
        // This class allows you to attach custom attributes to properties
        // of the Quiz class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }

        #region Nested type: QuizMetadata

        internal sealed class QuizMetadata
        {
            // Metadata classes are not meant to be instantiated.
            private QuizMetadata()
            {
            }

            public int? SimpleDifficulty { get; set; }

            public int? MediumDifficulty { get; set; }

            public int? HardDifficulty { get; set; }

            public EntityCollection<Question> Questions { get; set; }

            public int QuizId { get; set; }

            public EntityCollection<QuizResult> QuizResults { get; set; }

            public Section Section { get; set; }

            public int? SectionId { get; set; }

            public bool IsDeleted { get; set; }
        }

        #endregion
    }
}