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
using System.ServiceModel.DomainServices.Client;
using System.ComponentModel.DataAnnotations;
using MRZS;
using MRZS.Web.Services;
using MRZS.Web.Models;
using MRZS.Web;
using System.ServiceModel.DomainServices.Client.ApplicationServices;

namespace MRZS.Views.Admin.Popups
{
    public partial class CreateNewUser : ChildWindow
    {
        #region Services
        
        private RoleContext RoleService = new RoleContext();
        private GroupContext GroupService = new GroupContext();
        private RegistrationData registrationData = new RegistrationData();
        private UserRegistrationContext userRegistrationContext = new UserRegistrationContext();
        private UserContext UserService = new UserContext();

        #endregion

        public bool EditMode { get; set; }

        public int UserId { get; set; }

        public CreateNewUser()
        {
            InitializeComponent();
            DataBindOtherControls();
        }

        private void DataBindOtherControls()
        {
            comboBoxUserRole.ItemsSource = RoleService.aspnet_Roles;
            RoleService.Load(RoleService.GetAspnet_RolesQuery());
            comboBoxUserGroup.ItemsSource = GroupService.Groups;
            GroupService.Load(GroupService.GetGroupsQuery());

            PasswordBox passwordBox = textBoxUserPassword;
            this.registrationData.PasswordAccessor = () => passwordBox.Password;
            PasswordBox passwordConfirmationBox = textBoxUserOneMorePassword;
            this.registrationData.PasswordConfirmationAccessor = () => passwordConfirmationBox.Password;
            TextBox textBox = textBoxUserLogin;
            textBox.LostFocus += this.UserNameLostFocus;

        }

        /// <summary>
        /// The callback for when the UserName TextBox loses focus.  Call into the
        /// registration data to allow logic to be processed, possibly setting
        /// the FriendlyName field.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        private void UserNameLostFocus(object sender, RoutedEventArgs e)
        {
            this.registrationData.UserNameEntered(((TextBox)sender).Text);
        }


        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            //if (this.ValidateItem())
            {
                this.registrationData.Email = textBoxUserEmail.Text;
                this.registrationData.UserName = textBoxUserLogin.Text;
                registrationData.Question = "1?";
                registrationData.Answer = "1";

                MRZS.Web.Models.User user = new MRZS.Web.Models.User();
                registrationData.FirstName = textBoxFirstName.Text;
                registrationData.LastName = textBoxLastName.Text;
                if (comboBoxUserRole.SelectedIndex > 0)
                {
                    registrationData.UserRole = (comboBoxUserRole.SelectedValue as aspnet_Roles).RoleName;
                }
                var gr = comboBoxUserGroup.SelectedValue as Group;
                if (gr != null)
                {
                    registrationData.GroupId = gr.GroupId;
                }
                this.registrationData.CurrentOperation = this.userRegistrationContext.CreateUser(
                    this.registrationData,
                    this.registrationData.Password,
                    this.RegistrationOperation_Completed, null);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void textBoxUserRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBoxUserGroup.IsEnabled = (comboBoxUserRole.SelectedValue as aspnet_Roles).RoleName.ToLower() == "student";
        }

        /// <summary>
        /// Completion handler for the registration operation. If there was an error, an
        /// <see cref="ErrorWindow"/> is displayed to the user. Otherwise, this triggers
        /// a login operation that will automatically log in the just registered user.
        /// </summary>
        private void RegistrationOperation_Completed(InvokeOperation<CreateUserStatus> operation)
        {
            if (!operation.IsCanceled)
            {
                if (operation.HasError)
                {
                    ErrorWindow.CreateNew(operation.Error);
                    operation.MarkErrorAsHandled();
                }
                else if (operation.Value == CreateUserStatus.Success)
                {

                }
                else if (operation.Value == CreateUserStatus.DuplicateUserName)
                {
                    this.registrationData.ValidationErrors.Add(new ValidationResult(ErrorResources.CreateUserStatusDuplicateUserName, new string[] { "UserName" }));
                }
                else if (operation.Value == CreateUserStatus.DuplicateEmail)
                {
                    this.registrationData.ValidationErrors.Add(new ValidationResult(ErrorResources.CreateUserStatusDuplicateEmail, new string[] { "Email" }));
                }
                else
                {
                    ErrorWindow.CreateNew(ErrorResources.ErrorWindowGenericError);
                }
            }
            this.DialogResult = true;
        }

        private void LoginOperation_Completed(LoginOperation loginOperation)
        {
            if (!loginOperation.IsCanceled)
            {

                if (loginOperation.HasError)
                {
                    ErrorWindow.CreateNew(string.Format(System.Globalization.CultureInfo.CurrentUICulture, ErrorResources.ErrorLoginAfterRegistrationFailed, loginOperation.Error.Message));
                    loginOperation.MarkErrorAsHandled();
                }
                else if (loginOperation.LoginSuccess == false)
                {
                    // The operation was successful, but the actual login was not
                    ErrorWindow.CreateNew(string.Format(System.Globalization.CultureInfo.CurrentUICulture, ErrorResources.ErrorLoginAfterRegistrationFailed, ErrorResources.ErrorBadUserNameOrPassword));
                }
            }
        }

    }
}

