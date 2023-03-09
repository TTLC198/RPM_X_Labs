using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPM_X_2.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Themes;
using Xamarin.Forms.Xaml;

namespace RPM_X_2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        public User _user;
        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged();
            } 
        }
        public bool IsAdmin { get; set; }
        public bool IsLibrarian { get; set; }

        public ObservableCollection<Book> Books
        {
            get => App.Books;
            set => App.Books = value;
        }
        
        public ObservableCollection<User> Users
        {
            get => App.Users;
            set => App.Users = value;
        }
        
        public Book SelectedBook { get; set; }
        public User SelectedUser { get; set; }

        public UserPage(User user)
        {
            user.Photo ??= new FileImageSource()
            {
                File = "nullph.png"
            };
            User = user;
            IsAdmin = user.Role == RoleEnum.Administrator;
            IsLibrarian = user.Role == RoleEnum.Libranian;
            BindingContext = this;
            Resources = App.GlobalThemeResources;
            Title = $"Окно {(user.Role == RoleEnum.Administrator ? "администратора" : user.Role == RoleEnum.Libranian ? "библиотекаря" : "пользователя")}";

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

        private void ExitButton_OnClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private async void ReadOnlineButton_OnClicked(object sender, EventArgs e)
        {
            SelectedBook = App.Books.FirstOrDefault(b => b.ID == (int)(sender as Button)!.CommandParameter);
            await Navigation.PushAsync(new BookPage(SelectedBook));
        }

        private void SaveBookButton_OnClicked(object sender, EventArgs e)
        {
            SelectedBook = App.Books.FirstOrDefault(b => b.ID == (int)(sender as Button)!.CommandParameter);
            DisplayAlert("Скачивание книги", "Приятного чтения!", "OK");
        }

        private async void EditBookButton_OnClicked(object sender, EventArgs e)
        {
            SelectedBook = App.Books.FirstOrDefault(b => b.ID == (int)(sender as Button)!.CommandParameter);
            await Navigation.PushAsync(new BookPage(SelectedBook, true));
        }

        private void RemoveBookButton_OnClicked(object sender, EventArgs e)
        {
            SelectedBook = App.Books.FirstOrDefault(b => b.ID == (int)(sender as Button)!.CommandParameter);
            App.Books.Remove(SelectedBook);
        }

        private async void EditUserButton_OnClicked(object sender, EventArgs e)
        {
            SelectedUser = App.Users.FirstOrDefault(b => b.ID == (int)(sender as Button)!.CommandParameter);
            await Navigation.PushAsync(new RegistrationPage(SelectedUser, true));
        }

        private void RemoveUserButton_OnClicked(object sender, EventArgs e)
        {
            SelectedUser = App.Users.FirstOrDefault(b => b.ID == (int)(sender as Button)!.CommandParameter);
            App.Users.Remove(SelectedUser);
        }

        private async void AddBookButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BookPage(new Book(), true, true));
        }

        private async void UserPhotoChangeButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                // выбираем фото
                var photo = await MediaPicker.PickPhotoAsync();
                // загружаем в ImageView
                User.Photo = ImageSource.FromFile(photo.FullPath);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }

        }
    }
}