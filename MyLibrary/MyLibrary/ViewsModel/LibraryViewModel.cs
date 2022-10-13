using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyLibrary.ViewsModel
{
    public class LibraryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Book> BooksSet { get; } 
        public ICommand AddBook { get; private set; }

        public LibraryViewModel()
        {
            AddBook = new Command(OnAdd);
        }

        private void OnAdd(object o)
        {
            App.Current.MainPage.DisplayPromptAsync("Add Book", "Info book");
        }
    }
}