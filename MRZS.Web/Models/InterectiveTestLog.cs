using System.Linq;
using System.Runtime.Serialization;

namespace MRZS.Web.Models
{
    public partial class InterectiveTestLog
    {
        [DataMember]
        public string SectionName
        {
            get
            {
                if (!InterectiveTestReference.IsLoaded)
                    InterectiveTestReference.Load();
                if (!InterectiveTest.Sections.IsLoaded)
                    InterectiveTest.Sections.Load();
                Section sec = InterectiveTest.Sections.SingleOrDefault(s => s.SectionId == SectionId);
                if (sec == null)
                    return "";
                return sec.Name;
            }
        }

        [DataMember]
        public string BookName
        {
            get
            {
                if (!InterectiveTestReference.IsLoaded)
                    InterectiveTestReference.Load();
                if (!InterectiveTest.Sections.IsLoaded)
                    InterectiveTest.Sections.Load();
                Section sec = InterectiveTest.Sections.SingleOrDefault(s => s.SectionId == SectionId);
                if (sec == null)
                    return "";
                if (!sec.BookReference.IsLoaded)
                    sec.BookReference.Load();
                return sec.Book.Name;
            }
        }
    }
}