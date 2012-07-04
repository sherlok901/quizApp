using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using MRZS.Web.Models;
using MRZS.Web.Services;
using System.ServiceModel.DomainServices.Client;

namespace MRZS.Models.Statistic
{
    public class StatisticViewModel : BaseViewModel
    {
        private Group group;
        private User student;
        private UserContext studentContext;
        private GroupContext groupContaxt;
        private ResultQuizContext resultQuizContext;
        private bool isStudentsFiltered;

        public StatisticViewModel()
        {
            studentContext = new UserContext();
            var studentsLoad = studentContext.Load(studentContext.GetStudentsQuery());
            studentsLoad.Completed += new EventHandler(studentsLoad_Completed);
            groupContaxt = new GroupContext();
            groupContaxt.Load(groupContaxt.GetGroupsQuery());

            resultQuizContext = new ResultQuizContext();
        }

        void studentsLoad_Completed(object sender, EventArgs e)
        {
            RaisePropertyChanged("IsBusy");
        }

        public User SelectedStudent
        {
            get
            {
                return student;
            }
            set
            {
                student = value;
                loadResults(student);
                RaisePropertyChanged("SelectedStudent");
            }
        }

        public EntitySet<User> Students
        {
            get
            {
                return studentContext.Users;
            }
        }

        public EntitySet<Group> Groups
        {
            get
            {
                groupContaxt.Groups.Add(new Group() { GroupName = "Сбросить фильтр" });
                return groupContaxt.Groups;
            }
        }

        public Group SelectedGroup
        {
            get
            {
                return group;
            }
            set
            {
                group = value;
                filterStudentsByGroup(group);
                RaisePropertyChanged("SelectedGroup");
            }
        }

        private void filterStudentsByGroup(Group group)
        {
            if (group != null)
            {
                if (group.GroupId != 0)
                {
                    studentContext.Users.Clear();
                    studentContext.Load(studentContext.GetStudentsFroGroupQuery(group.GroupId));
                    isStudentsFiltered = true;
                }
                else if (isStudentsFiltered)
                {
                    studentContext.Load(studentContext.GetStudentsQuery());
                }
                RaisePropertyChanged("Students");
            }
        }

        private void loadResults(User student)
        {
            if (student == null)
                return;
            resultQuizContext.QuizResults.Clear();
            resultQuizContext.Load(resultQuizContext.GetQuizResultsForUserQuery(student.UserId));
            RaisePropertyChanged("QuizResults");
        }

        public EntitySet<QuizResult> QuizResults
        {
            get
            {
                return resultQuizContext.QuizResults;
            }
        }

        public bool IsBusy
        {
            get { return studentContext.IsLoading; }
        }
    }
}
