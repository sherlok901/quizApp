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
using MRZS.Web;
using MRZS.Web.Services;
using MRZS.Web.Models;
using System.Windows.Data;
using System.ServiceModel.DomainServices.Client;
using System.ComponentModel.DataAnnotations;

namespace MRZS.Views.Admin.Popups
{
    public partial class NewUser : ChildWindow
    {

        public bool EditMode { get; set; }

        public Web.Models.User CurrentUser { get; set; }

        private UserContext userService = new UserContext();
        private RoleContext RoleService = new RoleContext();
        private GroupContext GroupService = new GroupContext();
        private RegistrationData registrationData = new RegistrationData();
        private UserRegistrationContext userRegistrationContext = new UserRegistrationContext();
        public NewUser()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(NewUser_Loaded);
        }

        void NewUser_Loaded(object sender, RoutedEventArgs e)
        {
            if (EditMode)
            {
                registrationData.UserId = CurrentUser.UserId;
                registrationData.UserName = CurrentUser.UserLogin;
                registrationData.FirstName = CurrentUser.FirstName;
                registrationData.LastName = CurrentUser.LastName;
                registrationData.UserRole = CurrentUser.UserRole;
                registrationData.GroupId = CurrentUser.GroupId.HasValue ? CurrentUser.GroupId.Value : 0;
                dataForm.DataContext = registrationData;
                return;
            }
            dataForm.DataContext = registrationData;
        }


        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            var currentItem = dataForm.CurrentItem as RegistrationData;

            if ((string.IsNullOrEmpty(currentItem.Password)
                || string.IsNullOrEmpty(currentItem.PasswordConfirmation)) ? dataForm.ValidateItem() : true)
            {
                registrationData.Question = "1?";
                registrationData.Answer = "1";
                registrationData.Email = "User@company.com";
                if (comboBoxGroups.SelectedValue != null)
                    registrationData.GroupId = (comboBoxGroups.SelectedValue as Group).GroupId;
                registrationData.UserRole = (comboBoxRoles.SelectedValue as aspnet_Roles).RoleName;
                if (EditMode)
                {
                    var operation = this.userRegistrationContext.EditUser(this.registrationData,
                       this.registrationData.Password);
                    operation.Completed += new EventHandler(operation_Completed);
                }
                else
                {
                    this.registrationData.CurrentOperation = this.userRegistrationContext.CreateUser(
                        this.registrationData,
                        this.registrationData.Password,
                        this.RegistrationOperation_Completed, null);
                }
            }
        }

        void operation_Completed(object sender, EventArgs e)
        {
            this.DialogResult = true;
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
                    this.DialogResult = true;
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
           
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        private void RegisterForm_AutoGeneratingField(object dataForm, DataFormAutoGeneratingFieldEventArgs e)
        {
            // Put all the fields in adding mode
            e.Field.Mode = DataFieldMode.AddNew;

            if (e.PropertyName == "Password")
            {
                PasswordBox passwordBox = new PasswordBox();
                if (EditMode)
                {
                    //passwordBox.IsEnabled = false;
                    //passwordBox.Visibility = System.Windows.Visibility.Collapsed;
                    passwordBox.Password = "1";
                }
                e.Field.ReplaceTextBox(passwordBox, PasswordBox.PasswordProperty);
                this.registrationData.PasswordAccessor = () => passwordBox.Password;
            }
            else if (e.PropertyName == "PasswordConfirmation")
            {
                PasswordBox passwordConfirmationBox = new PasswordBox();
                if (EditMode)
                {
                    //passwordConfirmationBox.IsEnabled = false;
                    passwordConfirmationBox.Password = "1";
                    //passwordConfirmationBox.Visibility = System.Windows.Visibility.Collapsed;
                }
                e.Field.ReplaceTextBox(passwordConfirmationBox, PasswordBox.PasswordProperty);

                this.registrationData.PasswordConfirmationAccessor = () => passwordConfirmationBox.Password;
            }
            else if (e.PropertyName == "UserName" && !string.IsNullOrEmpty(registrationData.UserName))
            {
                TextBox textBox = (TextBox)e.Field.Content;
                if (EditMode)
                {
                    textBox.Text = registrationData.UserName;
                    textBox.IsEnabled = false;
                }
                textBox.LostFocus += this.UserNameLostFocus;
            }
            else if (e.PropertyName == "Question")
            {
                // Create a ComboBox populated with security questions
                ComboBox comboBoxWithSecurityQuestions = new ComboBox(); //DataFrom.CreateComboBoxWithSecurityQuestions();

                // Replace the control
                // Note: Since TextBox.Text treats empty text as string.Empty and ComboBox.SelectedItem
                // treats an empty selection as null, we need to use a converter on the binding
                e.Field.ReplaceTextBox(comboBoxWithSecurityQuestions, ComboBox.SelectedItemProperty, binding => binding.Converter = new TargetNullValueConverter());
            }
            else if (e.PropertyName == "GroupName")
            {
                comboBoxGroups = new ComboBox();
                comboBoxGroups.ItemsSource = GroupService.Groups;
                var groupLoadResult = GroupService.Load(GroupService.GetGroupsQuery());
                comboBoxGroups.DisplayMemberPath = "GroupName";
                Binding b = new Binding();
                b.Path = new PropertyPath("IsStudent");
                b.Mode = BindingMode.OneWay;
                b.Source = this;
                comboBoxGroups.SetBinding(ComboBox.IsEnabledProperty, b);
                if (EditMode)
                {
                    groupLoadResult.Completed += new EventHandler(groupLoadResult_Completed);
                }

                e.Field.ReplaceTextBox(comboBoxGroups, ComboBox.SelectedItemProperty);
            }

            else if (e.PropertyName == "UserRole")
            {
                comboBoxRoles = new ComboBox();
                comboBoxRoles.ItemsSource = RoleService.aspnet_Roles;
                var roleLoadResult = RoleService.Load(RoleService.GetAspnet_RolesQuery());
                comboBoxRoles.DisplayMemberPath = "RoleName";
                comboBoxRoles.SelectionChanged += new SelectionChangedEventHandler(comboBoxWithSecurityQuestions_SelectionChanged);
                if (EditMode)
                {
                    roleLoadResult.Completed += new EventHandler(roleLoadResult_Completed);
                }
                e.Field.ReplaceTextBox(comboBoxRoles, ComboBox.SelectedItemProperty, binding => binding.Converter = new TargetNullValueConverter());
            }
            else if (e.PropertyName == "FirstName" && EditMode && !string.IsNullOrEmpty(registrationData.FirstName))
            {
                var firstNameTextBox = (TextBox)e.Field.Content;
                firstNameTextBox.Text = registrationData.FirstName;
            }
            else if (e.PropertyName == "LastName" && EditMode && !string.IsNullOrEmpty(registrationData.LastName))
            {
                var lastNameTextBox = (TextBox)e.Field.Content;
                lastNameTextBox.Text = registrationData.LastName;
            }
        }

        void groupLoadResult_Completed(object sender, EventArgs e)
        {
            comboBoxGroups.SelectedValue = GroupService.Groups.SingleOrDefault(g => g.GroupId == registrationData.GroupId);
        }

        void roleLoadResult_Completed(object sender, EventArgs e)
        {
            comboBoxRoles.SelectedValue = RoleService.aspnet_Roles.SingleOrDefault(r => r.RoleName == registrationData.UserRole);
        }

        ComboBox comboBoxGroups;
        ComboBox comboBoxRoles;

        void comboBoxWithSecurityQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;
            aspnet_Roles selectedRole = e.AddedItems[0] as aspnet_Roles;
            IsStudent = selectedRole.RoleName == "Student";
        }

        public bool IsStudent { get; set; }

        private void UserNameLostFocus(object sender, RoutedEventArgs e)
        {
            this.registrationData.UserNameEntered(((TextBox)sender).Text);
        }

    }
}

