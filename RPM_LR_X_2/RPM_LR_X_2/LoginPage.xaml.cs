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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            Resources = App.GlobalThemeResources;
            InitializeComponent();
        }

        private void ThemeSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            if (Resources?.GetType () == typeof (DarkThemeResources))
            { 
                Resources = new LightThemeResources ();
                App.GlobalThemeResources = Resources;
                return;
            }
            Resources = new DarkThemeResources ();
            App.GlobalThemeResources = Resources;
        }

        private async void LoginButton_OnClicked(object sender, EventArgs e)
        {
            if (passwordEntry.Text is {Length: >= 3}  &&
                loginEntry.Text is {Length: >= 3})
            {
                statusLabel.Text = "Login succesful";
                var user = App.Users.FirstOrDefault(u => u.Login == loginEntry.Text && u.Password == passwordEntry.Text);
                if (user != null)
                {
                    await Navigation.PushAsync(new UserPage(user));
                    return;
                }

                DisplayAlert("Пользователь не найден!", "Повторите ввод", "OK");
            }
            else
                statusLabel.Text = "Text length must be greater than 3";
        }
        
        private void ExitButton_OnClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private async void RegButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }
    }
}