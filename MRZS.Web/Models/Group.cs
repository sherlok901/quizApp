using System.Linq;
using System.Runtime.Serialization;

namespace MRZS.Web.Models
{
    public partial class Group
    {
        [DataMember]
        public int StudentCount
        {
            get
            { 
                if (!Users.IsLoaded)
                    Users.Load();
                return Users.Count();
            } 
        }
    }
}