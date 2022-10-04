using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TestBook.Commands;
using TestBook.Model;
using TestBook.View;

namespace TestBook.ViewModel
{
    public class MainWindowViewModel: ViewModelBase
    {
        private readonly BookContext _db;
        private string _authors;

        private ICommand _openEditWindowCommand;
        private ICommand _openDeleteWindowCommand;
        private ICommand _openWindowCommand;

        public ICommand OpenEditWindowCommand => _openEditWindowCommand ?? (_openEditWindowCommand = new RelayCommand(OpenWindowEditBook));
        public ICommand OpenDeleteWindowCommand => _openDeleteWindowCommand ?? (_openDeleteWindowCommand = new RelayCommand(OpenWindowDeleteBook));
        public ICommand OpenWindowCommand => _openWindowCommand ?? (_openWindowCommand = new RelayCommand(OpenWindow));
        public ObservableCollection<Book> Books { get; set; }

        public ObservableCollection<string> Authors { get; set; }
        public string Author
        {

            get { return _authors; }
            set
            {
                _authors = value;
                OnPropertyChanged("Name");
            }
        }



        public MainWindowViewModel(BookContext db)
        {
            _db = db;
            _db.Books.Load();
            Books = _db.Books.Local.ToObservableCollection();
            GetAuthor();
        }

        private void GetAuthor()
        {
            ObservableCollection<string> Auth = new ObservableCollection<string>();
            if (_db is not null)
            {
                if (_db.Authors is not null)
                {
                    foreach(var author in _db.Authors)
                    {
                        Author = author.Name;
                    }
                    Authors = Auth;
                }
            }
            Console.WriteLine("Контекст базы данных пустой");
        }

        private void OpenWindow()
        {
            
            AddBookView edit = new AddBookView(_db);
            edit.Show();
        }

        private void OpenWindowDeleteBook()
        {
            DeleteBookView delete = new DeleteBookView(_db);
            delete.Show();
        }

        private void OpenWindowEditBook()
        {
            EditBookView edit = new EditBookView(_db);
            edit.Show();
        }


    }
}
