using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MRZS.Web.Services;
using MRZS.Web.Models;

namespace MRZS.Views.Admin.Popups
{
    public partial class CreateNewGroup : ChildWindow
    {
        private GroupContext groupService = new GroupContext();
        public CreateNewGroup()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Group group = new Group();
            group.GroupName = textBoxGroupName.Text;
            groupService.Groups.Add(group);
            var groupSubmiting = groupService.SubmitChanges();
            groupSubmiting.Completed += new EventHandler(groupSubmiting_Completed);
        }

        void groupSubmiting_Completed(object sender, EventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

