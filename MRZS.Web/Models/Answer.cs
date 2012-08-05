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
                MRZSEntities mr = new MRZSEntities();
                
                #if DEBUG
                return _IsCorrect;
                #endif
                return false;                
            }
        }
    }
}