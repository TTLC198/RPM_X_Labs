using System.ComponentModel;
using System.Runtime.CompilerServices;
using RPM_LR_X_2.Annotations;

namespace RPM_X_2.Models
{
    public class Book : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private string _author;
        private string _text;

        public int ID
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private int _downloadCount;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Author
        {
            get => _author;
            set
            {
                _author = value;
                OnPropertyChanged();
            }
        }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        public int DownloadCount
        {
            get => _downloadCount;
            set
            {
                _downloadCount = value;
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