using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TestBook.Commands;
using TestBook.Model;

namespace TestBook.ViewModel
{
    public class DeleteBookViewModel : ViewModelBase
    {
        private int _id;
        private readonly BookContext _db;


        private ICommand _closeAndDeleteCommand; 

        public ICommand CloseAndDeleteCommand => _closeAndDeleteCommand ?? (_closeAndDeleteCommand = new RelayCommand(DeleteBook));

        public int Id 
        { get
            {
                return _id;
            } set
            { 
                _id = value;
                OnPropertyChanged("Id");
            } 
        }

        public DeleteBookViewModel(BookContext db)
        {
            _db = db;
            
        }

        private void DeleteBook()
        {
            if (_db is not null)
            {
                var book = _db.Books.Where(x => x.Id == _id).ToList();
                if (book is not null)
                {
                    _db.RemoveRange(book);
                    _db.SaveChangesAsync();
                    foreach (Window item in Application.Current.Windows)
                    {
                        if (item.DataContext == this) item.Close();
                    }
                }
                Console.WriteLine("Такого id не существует");
            }
            Console.WriteLine("Контекста базы данных не существует");
            
        }
    }
}
