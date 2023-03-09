using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RPM_LR_X_2.Annotations;
using Xamarin.Forms;

namespace RPM_X_2.Models
{
    public class User : INotifyPropertyChanged
    {
        private int _id;
        private string _login;
        private string _password;
        private RoleEnum _role;
        private string _about;
        private DateTime _birthDate;
        private string _phoneNumber;
        private ImageSource _photo;
    
        public int ID
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }
        
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }
        
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
    
        public RoleEnum Role
        {
            get => _role;
            set
            {
                _role = value;
                OnPropertyChanged();
            }
        }
        
        public string About
        {
            get => _about;
            set
            {
                _about = value;
                OnPropertyChanged();
            }
        }
        
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                OnPropertyChanged();
            }
        }
        
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }
        
        public ImageSource Photo
        {
            get => _photo;
            set
            {
                _photo = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}