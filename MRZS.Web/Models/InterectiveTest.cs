using System.Runtime.Serialization;

namespace MRZS.Web.Models
{
    public partial class InterectiveTest
    {
        [DataMember]
        public string Parameters
        {
            get { return InterectiveTestParameters.ToCommaSeparatedList<InterectiveTestParameter>(t => t.ParameterName); }
        }
    }
}