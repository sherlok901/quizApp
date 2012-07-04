using System.Configuration;

namespace MRZS.Web.Models
{
    public static class Preferences
    {
        public static int QuestionsCount
        {
            get
            {
                int questionsCount = 0;
                if (int.TryParse(ConfigurationManager.AppSettings["QuestionForStudent"], out questionsCount))
                    return questionsCount;
                return 15;
            }
        }
    }
}