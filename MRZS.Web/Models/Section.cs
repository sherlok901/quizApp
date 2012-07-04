using System.Runtime.Serialization;

namespace MRZS.Web.Models
{
    public partial class Section
    {
        [DataMember]
        public string BookName
        {
            get
            {
                try
                {
                    if (SectionId != 0 && !BookReference.IsLoaded)
                        BookReference.Load();
                    return Book.Name;
                }
                catch
                {
                }
                finally
                {
                    //TODO: avoid not attached exception
                }
                return string.Empty;
            }
        }

        [DataMember]
        public bool IsCurrent { get; set; }
    }
}