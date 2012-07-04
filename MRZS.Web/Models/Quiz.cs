using System;
using System.Runtime.Serialization;

namespace MRZS.Web.Models
{
    public partial class Quiz
    {
        [DataMember]
        public int QuestionsCount
        {
            get
            {
                try
                {
                    //TODO: QuestionsCount
                    if (!Questions.IsLoaded)
                        Questions.Load();

                    return Questions.Count;
                }
                catch (Exception ex)
                {
                    ;
                }
                finally
                {
                }
                return 0;
            }
        }
    }
}