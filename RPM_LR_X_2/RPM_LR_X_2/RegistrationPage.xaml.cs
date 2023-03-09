using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RPM_X_2.Models;
using Xamarin.Forms;
using Xamarin.Forms.Themes;
using Xamarin.Forms.Xaml;

namespace RPM_X_2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public User User { get; set; }
        public bool IsEdit { get; set; }

        public IReadOnlyList<RoleEnum> RoleEnums { get; set; } = new List<RoleEnum>()
        {
            RoleEnum.Client,
            RoleEnum.Administrator,
            RoleEnum.Libranian
        };

        public RegistrationPage(User user = null, bool isEdit = false)
        {
            IsEdit = isEdit;
            User = IsEdit
                ? user
                : new User
                {
                    Login = "Enter login here",
                    Password = "Enter password here",
                    About = "User description",
                    BirthDate = DateTime.Now - new TimeSpan(14 * 365, 0, 0, 0),
                    PhoneNumber = "88005553535"
                };
            BindingContext = this;
            Resources = App.GlobalThemeResources;
            InitializeComponent();
            birthDatePicker.MinimumDate = DateTime.Now - new TimeSpan(99 * 365, 0, 0, 0);
            birthDatePicker.MaximumDate = DateTime.Now - new TimeSpan(14 * 365, 0, 0, 0);
            RegistrationButton.Text = IsEdit ? "Save" : "Registration";
        }

        private void ThemeSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            if (Resources?.GetType() == typeof(DarkThemeResources))
            {
                Resources = new LightThemeResources();
                App.GlobalThemeResources = Resources;
                return;
            }

            Resources = new DarkThemeResources();
            App.GlobalThemeResources = Resources;
        }

        private async void RegistrationButton_OnClicked(object sender, EventArgs e)
        {
            var rx = new Regex(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            if (passwordEntry is {Text: {Length: >= 3}} or {Placeholder: {Length: >= 3}} &&
                (IsEdit || repeatPassword is {Text: {Length: >= 3}} or {Placeholder: {Length: >= 3}}) &&
                loginEntry is {Text: {Length: >= 3}} or {Placeholder: {Length: >= 3}})
            {
                if (birthDatePicker.Date < DateTime.Now)
                {
                    if (rx.IsMatch(phoneEntry.Text ?? phoneEntry.Placeholder))
                    {
                        if (rolePicker.SelectedItem is not null)
                        {
                            if (IsEdit)
                            {
                                var newUser = new User
                                {
                                    Login = loginEntry.Text ?? loginEntry.Placeholder,
                                    Password = passwordEntry.Text ?? passwordEntry.Placeholder,
                                    BirthDate = birthDatePicker.Date,
                                    PhoneNumber = phoneEntry.Text ?? phoneEntry.Placeholder,
                                    Role = (RoleEnum)rolePicker.SelectedItem
                                };
                                
                                var oldUser = App.Users.FirstOrDefault(u => u.ID == User.ID);
                                oldUser!.Login = newUser.Login;
                                oldUser.Password = newUser.Password;
                                oldUser.BirthDate = newUser.BirthDate;
                                oldUser.PhoneNumber = newUser.PhoneNumber;
                                oldUser.Role = newUser.Role;
                                
                                statusLabel.Text = "Edit succesful";
                                await Navigation.PopAsync();
                            }
                            else
                            {
                                if (repeatPassword.Text.Contains(passwordEntry.Text))
                                {
                                    var newUser = new User
                                    {
                                        Login = loginEntry.Text ?? loginEntry.Placeholder,
                                        Password = passwordEntry.Text ?? passwordEntry.Placeholder,
                                        BirthDate = birthDatePicker.Date,
                                        PhoneNumber = phoneEntry.Text ?? phoneEntry.Placeholder,
                                        Role = (RoleEnum) (rolePicker.SelectedItem ?? RoleEnum.Client)
                                    };
                                    
                                    statusLabel.Text = "Registration succesful";
                                    
                                    await Navigation.PushAsync(new UserPage(newUser));
                                    Navigation.RemovePage(this);
                                }
                                else
                                    statusLabel.Text = "Please repeat password correctly";
                            }
                        }
                        else
                            statusLabel.Text = "Please select role";
                    }
                    else
                        statusLabel.Text = "Please enter valid phone";
                }
                else
                    statusLabel.Text = "Please enter valid date";
            }
            else
                statusLabel.Text = "Text length must be greater than 3";
        }

        private void ExitButton_OnClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}