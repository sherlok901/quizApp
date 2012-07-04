using MRZS.Models;
using MRZS.Web.Services;
using System.ServiceModel.DomainServices.Client;
using MRZS.Web.Models;
using System.Linq;
using System.Windows.Input;
using System.Collections.Generic;

namespace MRZS.Views.Admin.Popups
{
    public class MoveQuestionViewModel : BaseViewModel
    {
        private SectionContext sectionContext;
        private QuestionContext questionContextFrom;
        private QuestionContext questionContextTo;
        private QuizContext quizContext;
        private ICommand moveQuestionsCommand;
        private int _sectionId;
        private Section sectionTo;
        private Question questionToMove;

        public List<Question> FromQuestionSet
        {
            get;
            private set;
        }
        public List<Question> ToQuestionSet
        {
            get;
            private set;
        }
        public Question QuestionToMove
        {
            get
            {
                return questionToMove;
            }
            set
            {
                questionToMove = value;
            }
        }
        public Section SectionFrom { get; private set; }
        public Section SectionTo
        {
            get
            {
                return sectionTo;
            }
            set
            {
                if (sectionTo == value)
                    return;
                sectionTo = value;
                loadToQuestionSet(value);
            }
        }

        private void loadToQuestionSet(Section value)
        {
            var questionSetToLoad = questionContextTo.Load(questionContextTo.GetQuestionsForSectionsQuery(value.SectionId));
            questionSetToLoad.Completed += new System.EventHandler(questionSetToLoad_Completed);
        }

        void questionSetToLoad_Completed(object sender, System.EventArgs e)
        {
            var loadResult = sender as LoadOperation<Question>;
            ToQuestionSet = loadResult.Entities.ToList();
            RaisePropertyChanged("ToQuestionSet");
        }

        public EntitySet<Section> Sections
        {
            get
            {
                return sectionContext.Sections;
            }
        }
        public ICommand MoveQuestionsCommand
        {
            get { return moveQuestionsCommand; }
            set { moveQuestionsCommand = value; }
        }

        public MoveQuestionViewModel(int sectionId)
        {
            _sectionId = sectionId;
            questionContextTo = new QuestionContext();
            sectionContext = new SectionContext();
            questionContextFrom = new QuestionContext();
            quizContext = new QuizContext();
            var sectionLoad = sectionContext.Load(sectionContext.GetSectionsQuery());
            sectionLoad.Completed += new System.EventHandler(sectionLoad_Completed);
            loadFromQuestionSet();
            MoveQuestionsCommand = new DelegateCommand<Question>(OnMoveCommand, CanMoveCommand);
        }

        private void questionLoad_Completed(object sender, System.EventArgs e)
        {
            var questionLoadResult = sender as LoadOperation<Question>;
            FromQuestionSet = questionLoadResult.Entities.ToList();
            RaisePropertyChanged("FromQuestionSet");
        }

        private void sectionLoad_Completed(object sender, System.EventArgs e)
        {
            var sectionLoadResult = sender as LoadOperation<Section>;
            SectionFrom = sectionLoadResult.Entities.FirstOrDefault(s => s.SectionId == _sectionId);
            RaisePropertyChanged("SectionFrom");
            RaisePropertyChanged("Sections");
        }

        public bool CanMoveCommand(Question question)
        {
            return question != null && SectionTo != null;
        }

        public void OnMoveCommand(Question question)
        {
            if (SectionTo == null)
                return;
            var quizLoad = quizContext.Load(quizContext.GetQuizsForSectionQuery(SectionTo.SectionId));
            quizLoad.Completed += new System.EventHandler(quizLoad_Completed);
        }

        private void quizLoad_Completed(object sender, System.EventArgs e)
        {
            var quizLoadResult = sender as LoadOperation<Quiz>;
            Quiz quiz = quizLoadResult.Entities.FirstOrDefault();
            if (quiz == null)
                return;
            FromQuestionSet.Remove(questionToMove);
            questionToMove.QuizId = quiz.QuizId;
            var saveQuestionMove = questionContextFrom.SubmitChanges();
            saveQuestionMove.Completed += new System.EventHandler(saveQuestionMove_Completed);
            ToQuestionSet.Add(questionToMove);
        }

        void saveQuestionMove_Completed(object sender, System.EventArgs e)
        {
            loadFromQuestionSet();
            loadToQuestionSet(SectionTo);
        }

        private void loadFromQuestionSet()
        {
            var questionLoad = questionContextFrom.Load(questionContextFrom.GetQuestionsForSectionsQuery(_sectionId));
            questionLoad.Completed += new System.EventHandler(questionLoad_Completed);
        }
    }
}
