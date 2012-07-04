using System.Runtime.Serialization;

namespace MRZS.Web.Models
{
    public partial class Answer
    {
        [DataMember]
        public bool IsCorrectDebug
        {
            get
            {
#if DEBUG
                return _IsCorrect;
#endif
                return false;
            }
        }
    }
}