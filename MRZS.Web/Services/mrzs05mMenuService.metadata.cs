
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


    // Атрибут MetadataTypeAttribute идентифицирует BooleanValMetadata как класс,
    // который содержит дополнительные метаданные для класса BooleanVal.
    [MetadataTypeAttribute(typeof(BooleanVal.BooleanValMetadata))]
    public partial class BooleanVal
    {

        // Этот класс позволяет добавлять настраиваемые атрибуты к свойствам
        // класса BooleanVal.
        //
        // Например, далее свойство Xyz помечено как
        // обязательное и указан формат допустимых значений:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class BooleanValMetadata
        {

            // Классы метаданных не предназначены для создания экземпляров.
            private BooleanValMetadata()
            {
            }

            public int id { get; set; }

            public EntityCollection<mrzs05mMenu> mrzs05mMenu { get; set; }

            public string val { get; set; }
        }
    }

    // Атрибут MetadataTypeAttribute идентифицирует BooleanVal2Metadata как класс,
    // который содержит дополнительные метаданные для класса BooleanVal2.
    [MetadataTypeAttribute(typeof(BooleanVal2.BooleanVal2Metadata))]
    public partial class BooleanVal2
    {

        // Этот класс позволяет добавлять настраиваемые атрибуты к свойствам
        // класса BooleanVal2.
        //
        // Например, далее свойство Xyz помечено как
        // обязательное и указан формат допустимых значений:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class BooleanVal2Metadata
        {

            // Классы метаданных не предназначены для создания экземпляров.
            private BooleanVal2Metadata()
            {
            }

            public int id { get; set; }

            public EntityCollection<mrzs05mMenu> mrzs05mMenu { get; set; }

            public string val { get; set; }
        }
    }

    // Атрибут MetadataTypeAttribute идентифицирует BooleanVal3Metadata как класс,
    // который содержит дополнительные метаданные для класса BooleanVal3.
    [MetadataTypeAttribute(typeof(BooleanVal3.BooleanVal3Metadata))]
    public partial class BooleanVal3
    {

        // Этот класс позволяет добавлять настраиваемые атрибуты к свойствам
        // класса BooleanVal3.
        //
        // Например, далее свойство Xyz помечено как
        // обязательное и указан формат допустимых значений:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class BooleanVal3Metadata
        {

            // Классы метаданных не предназначены для создания экземпляров.
            private BooleanVal3Metadata()
            {
            }

            public string boolVal { get; set; }

            public int id { get; set; }

            public EntityCollection<mrzs05mMenu> mrzs05mMenu { get; set; }
        }
    }

    // Атрибут MetadataTypeAttribute идентифицирует kindSignalDCMetadata как класс,
    // который содержит дополнительные метаданные для класса kindSignalDC.
    [MetadataTypeAttribute(typeof(kindSignalDC.kindSignalDCMetadata))]
    public partial class kindSignalDC
    {

        // Этот класс позволяет добавлять настраиваемые атрибуты к свойствам
        // класса kindSignalDC.
        //
        // Например, далее свойство Xyz помечено как
        // обязательное и указан формат допустимых значений:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class kindSignalDCMetadata
        {

            // Классы метаданных не предназначены для создания экземпляров.
            private kindSignalDCMetadata()
            {
            }

            public int id { get; set; }

            public string kindSignal { get; set; }

            public EntityCollection<mrzs05mMenu> mrzs05mMenu { get; set; }
        }
    }

    // Атрибут MetadataTypeAttribute идентифицирует mrzs05mMenuMetadata как класс,
    // который содержит дополнительные метаданные для класса mrzs05mMenu.
    [MetadataTypeAttribute(typeof(mrzs05mMenu.mrzs05mMenuMetadata))]
    public partial class mrzs05mMenu
    {

        // Этот класс позволяет добавлять настраиваемые атрибуты к свойствам
        // класса mrzs05mMenu.
        //
        // Например, далее свойство Xyz помечено как
        // обязательное и указан формат допустимых значений:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class mrzs05mMenuMetadata
        {

            // Классы метаданных не предназначены для создания экземпляров.
            private mrzs05mMenuMetadata()
            {
            }

            public BooleanVal BooleanVal { get; set; }

            public BooleanVal2 BooleanVal2 { get; set; }

            public Nullable<int> BooleanVal2ID { get; set; }

            public BooleanVal3 BooleanVal3 { get; set; }

            public Nullable<int> BooleanVal3ID { get; set; }

            public Nullable<int> BooleanValID { get; set; }

            public int id { get; set; }

            public kindSignalDC kindSignalDC { get; set; }

            public Nullable<int> kindSignalDCid { get; set; }

            public string menuElement { get; set; }

            public mrzsInOutOption mrzsInOutOption { get; set; }

            public Nullable<int> mrzsInOutOptionsID { get; set; }

            public mtzVal mtzVal { get; set; }

            public Nullable<int> mtzValID { get; set; }

            public Nullable<int> parentID { get; set; }

            public Nullable<int> passwordCheckType { get; set; }

            public passwordCheckType passwordCheckType1 { get; set; }

            public typeFuncDC typeFuncDC { get; set; }

            public Nullable<int> typeFuncDCid { get; set; }

            public typeSignalDC typeSignalDC { get; set; }

            public Nullable<int> typeSignalDCid { get; set; }

            public string unitValue { get; set; }

            public string value { get; set; }
        }
    }

    // Атрибут MetadataTypeAttribute идентифицирует mrzsInOutOptionMetadata как класс,
    // который содержит дополнительные метаданные для класса mrzsInOutOption.
    [MetadataTypeAttribute(typeof(mrzsInOutOption.mrzsInOutOptionMetadata))]
    public partial class mrzsInOutOption
    {

        // Этот класс позволяет добавлять настраиваемые атрибуты к свойствам
        // класса mrzsInOutOption.
        //
        // Например, далее свойство Xyz помечено как
        // обязательное и указан формат допустимых значений:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class mrzsInOutOptionMetadata
        {

            // Классы метаданных не предназначены для создания экземпляров.
            private mrzsInOutOptionMetadata()
            {
            }

            public int id { get; set; }

            public EntityCollection<mrzs05mMenu> mrzs05mMenu { get; set; }

            public string optionsName { get; set; }
        }
    }

    // Атрибут MetadataTypeAttribute идентифицирует mtzValMetadata как класс,
    // который содержит дополнительные метаданные для класса mtzVal.
    [MetadataTypeAttribute(typeof(mtzVal.mtzValMetadata))]
    public partial class mtzVal
    {

        // Этот класс позволяет добавлять настраиваемые атрибуты к свойствам
        // класса mtzVal.
        //
        // Например, далее свойство Xyz помечено как
        // обязательное и указан формат допустимых значений:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class mtzValMetadata
        {

            // Классы метаданных не предназначены для создания экземпляров.
            private mtzValMetadata()
            {
            }

            public int id { get; set; }

            public EntityCollection<mrzs05mMenu> mrzs05mMenu { get; set; }

            public string mtzVals { get; set; }
        }
    }

    // Атрибут MetadataTypeAttribute идентифицирует passwordCheckTypeMetadata как класс,
    // который содержит дополнительные метаданные для класса passwordCheckType.
    [MetadataTypeAttribute(typeof(passwordCheckType.passwordCheckTypeMetadata))]
    public partial class passwordCheckType
    {

        // Этот класс позволяет добавлять настраиваемые атрибуты к свойствам
        // класса passwordCheckType.
        //
        // Например, далее свойство Xyz помечено как
        // обязательное и указан формат допустимых значений:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class passwordCheckTypeMetadata
        {

            // Классы метаданных не предназначены для создания экземпляров.
            private passwordCheckTypeMetadata()
            {
            }

            public int id { get; set; }

            public EntityCollection<mrzs05mMenu> mrzs05mMenu { get; set; }

            public string passwordType { get; set; }
        }
    }

    // Атрибут MetadataTypeAttribute идентифицирует typeFuncDCMetadata как класс,
    // который содержит дополнительные метаданные для класса typeFuncDC.
    [MetadataTypeAttribute(typeof(typeFuncDC.typeFuncDCMetadata))]
    public partial class typeFuncDC
    {

        // Этот класс позволяет добавлять настраиваемые атрибуты к свойствам
        // класса typeFuncDC.
        //
        // Например, далее свойство Xyz помечено как
        // обязательное и указан формат допустимых значений:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class typeFuncDCMetadata
        {

            // Классы метаданных не предназначены для создания экземпляров.
            private typeFuncDCMetadata()
            {
            }

            public int @int { get; set; }

            public EntityCollection<mrzs05mMenu> mrzs05mMenu { get; set; }

            public string typeFunction { get; set; }
        }
    }

    // Атрибут MetadataTypeAttribute идентифицирует typeSignalDCMetadata как класс,
    // который содержит дополнительные метаданные для класса typeSignalDC.
    [MetadataTypeAttribute(typeof(typeSignalDC.typeSignalDCMetadata))]
    public partial class typeSignalDC
    {

        // Этот класс позволяет добавлять настраиваемые атрибуты к свойствам
        // класса typeSignalDC.
        //
        // Например, далее свойство Xyz помечено как
        // обязательное и указан формат допустимых значений:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class typeSignalDCMetadata
        {

            // Классы метаданных не предназначены для создания экземпляров.
            private typeSignalDCMetadata()
            {
            }

            public int id { get; set; }

            public EntityCollection<mrzs05mMenu> mrzs05mMenu { get; set; }

            public string typeSignal { get; set; }
        }
    }
}
