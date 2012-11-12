
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


    // Атрибут MetadataTypeAttribute идентифицирует TestAnswerMetadata как класс,
    // который содержит дополнительные метаданные для класса TestAnswer.
    [MetadataTypeAttribute(typeof(TestAnswer.TestAnswerMetadata))]
    public partial class TestAnswer
    {

        // Этот класс позволяет добавлять настраиваемые атрибуты к свойствам
        // класса TestAnswer.
        //
        // Например, далее свойство Xyz помечено как
        // обязательное и указан формат допустимых значений:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class TestAnswerMetadata
        {

            // Классы метаданных не предназначены для создания экземпляров.
            private TestAnswerMetadata()
            {
            }

            public string answer { get; set; }

            public int id { get; set; }

            public Nullable<bool> IsTrue { get; set; }

            public EntityCollection<QuestionAnswer> QuestionAnswers { get; set; }

            public Nullable<int> questionID { get; set; }

            public TestQuestion TestQuestion { get; set; }
        }
    }
}
