
namespace MRZS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // Атрибут MetadataTypeAttribute идентифицирует TestingImageMetadata как класс,
    // который содержит дополнительные метаданные для класса TestingImage.
    [MetadataTypeAttribute(typeof(TestingImage.TestingImageMetadata))]
    public partial class TestingImage
    {

        // Этот класс позволяет добавлять настраиваемые атрибуты к свойствам
        // класса TestingImage.
        //
        // Например, далее свойство Xyz помечено как
        // обязательное и указан формат допустимых значений:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class TestingImageMetadata
        {

            // Классы метаданных не предназначены для создания экземпляров.
            private TestingImageMetadata()
            {
            }

            public int id { get; set; }

            public string imgPath { get; set; }
        }
    }
}
