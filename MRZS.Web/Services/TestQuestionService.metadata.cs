
namespace MRZS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // Атрибут MetadataTypeAttribute идентифицирует TestQuestionMetadata как класс,
    // который содержит дополнительные метаданные для класса TestQuestion.
    [MetadataTypeAttribute(typeof(TestQuestion.TestQuestionMetadata))]
    public partial class TestQuestion
    {

        // Этот класс позволяет добавлять настраиваемые атрибуты к свойствам
        // класса TestQuestion.
        //
        // Например, далее свойство Xyz помечено как
        // обязательное и указан формат допустимых значений:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class TestQuestionMetadata
        {

            // Классы метаданных не предназначены для создания экземпляров.
            private TestQuestionMetadata()
            {
            }

            public int id { get; set; }

            public string question { get; set; }

            public EntityCollection<QuestionAnswer> QuestionAnswers { get; set; }

            public EntityCollection<TestAnswer> TestAnswers { get; set; }
        }
    }
}
