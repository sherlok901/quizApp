using System;
using System.Runtime.Serialization;

namespace MRZS.Web.Models
{
    public partial class QuizResult
    {
        [DataMember]
        public decimal CorrectAnswers
        {
            get
            {
                if (!QuizReference.IsLoaded)
                    QuizReference.Load();

                int questionCount = Preferences.QuestionsCount;
                int all = Quiz.QuestionsCount;
                int correct = 0;

                if (!ResultAnswers.IsLoaded)
                    ResultAnswers.Load();

                int answersCount = ResultAnswers.Count;

                foreach (ResultAnswer a in ResultAnswers)
                {
                    if (!a.AnswerReference.IsLoaded)
                        a.AnswerReference.Load();
                    if (a.Answer.IsCorrect)
                        correct++;
                }

                if (questionCount < all && answersCount > questionCount && all != 0)
                    return (correct/(decimal) answersCount)*100;
                else if (questionCount < all && all != 0)
                    return (correct/(decimal) questionCount)*100;

                return all != 0 ? (correct/(decimal) all)*100 : 0;
            }
        }

        [DataMember]
        public TimeSpan Duration
        {
            get
            {
                if (!EndTime.HasValue)
                    return new TimeSpan(0);

                return
                    new TimeSpan(EndTime.Value.Ticks - StartTime.Ticks > 0 ? EndTime.Value.Ticks - StartTime.Ticks : 0);
            }
        }

        [DataMember]
        public string DurationString
        {
            get { return Duration.Seconds.ToString(); }
        }

        [DataMember]
        public string SectionName
        {
            get
            {
                if (!QuizReference.IsLoaded)
                    QuizReference.Load();
                if (!Quiz.SectionReference.IsLoaded)
                    Quiz.SectionReference.Load();

                return Quiz.Section.Name;
            }
        }

        [DataMember]
        public string BookName
        {
            get
            {
                if (!QuizReference.IsLoaded)
                    QuizReference.Load();
                if (!Quiz.SectionReference.IsLoaded)
                    Quiz.SectionReference.Load();
                if (!Quiz.Section.BookReference.IsLoaded)
                    Quiz.Section.BookReference.Load();

                return Quiz.Section.Book.Name;
            }
        }
    }
}