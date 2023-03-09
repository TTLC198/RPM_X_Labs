using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json;
using RPM_X_2.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace RPM_X_2
{
    public partial class App : Application
    {
        private static ObservableCollection<Book> _books;
        private static ObservableCollection<User> _users;

        public static ObservableCollection<Book> Books
        {
            get => _books;
            set
            {
                _books = value;
                Preferences.Set("books", JsonConvert.SerializeObject(value));
            } 
        }

        public static ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                Preferences.Set("users", JsonConvert.SerializeObject(value));
            } 
        }
        
        public static ResourceDictionary GlobalThemeResources { get; set; }
        
        public App()
        {
            
#if VALUES
            if (_books.Count == 0)
            {
                Books = new()
                {
                    new () { ID = 0, Author = "А.С.Пушкин", Name = "Капитанская Дочка", Text = "Капитанская дочка Капитанская дочка Капитанская дочка Капитанская дочка"},
                    new () { ID = 1, Author = "Рэй Брэдбери", Name = "451 градус по фаренгейту", Text = "Something something something"}
                };
            }
            if (_users.Count == 0)
            {
                Users =
                    new()
                    {
                        new User
                        {
                            ID = 0,
                            Login = "ex1",
                            Password = "ex1",
                            About = "something about user 1",
                            Role = RoleEnum.Client,
                            PhoneNumber = "8800553535",
                            BirthDate = new DateTime(2005, 05, 30),
                        },
                        new User
                        {
                            ID = 1,
                            Login = "ex2",
                            Password = "ex2",
                            About = "something about user 2",
                            Role = RoleEnum.Libranian,
                            PhoneNumber = "8800553535",
                            BirthDate = new DateTime(2007, 05, 30)
                        },
                        new User
                        {
                            ID = 2,
                            Login = "ex3",
                            Password = "ex3",
                            About = "something about user 3",
                            Role = RoleEnum.Administrator,
                            PhoneNumber = "8800553535",
                            BirthDate = new DateTime(2006, 05, 30)
                        }
                    };
            }
#endif
            
            var booksJson = Preferences.Get("books", JsonConvert.SerializeObject(Books));
            var usersJson = Preferences.Get("users", JsonConvert.SerializeObject(Users));
            Books = JsonConvert.DeserializeObject<ObservableCollection<Book>>(booksJson);
            Users = JsonConvert.DeserializeObject<ObservableCollection<User>>(usersJson);
            
            Users.CollectionChanged += (_, _) =>
            {
                Preferences.Set("users", JsonConvert.SerializeObject(_users));
            };
                        
            Books.CollectionChanged += (_, _) =>
            {
                Preferences.Set("books", JsonConvert.SerializeObject(_books));
            };
            
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}