using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPM_X_2.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RPM_X_2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookPage : ContentPage
    {
        public Book Book { get; set; }
        public bool IsEditable { get; set; }
        public bool IsNew { get; set; }
    
        public BookPage(Book book, bool isEditable = false, bool isNew = false)
        {
            Book = book;
            IsEditable = isEditable;
            IsNew = isNew;
            BindingContext = this;
            Resources = App.GlobalThemeResources;
            InitializeComponent();
        }

        private async void BackButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void SaveButton_OnClicked(object sender, EventArgs e)
        {
            if (IsNew)
            {
                App.Books.Add(new Book
                {
                    ID = App.Books.OrderByDescending(item => item.ID).First().ID + 1,
                    Name = Book.Name,
                    Author = Book.Author,
                    DownloadCount = 0,
                    Text = Book.Text
                });
            }
            else 
                App.Books.FirstOrDefault(b => b.ID == Book.ID)!.Text = Book.Text;
            await Navigation.PopAsync();
        }
    }
}