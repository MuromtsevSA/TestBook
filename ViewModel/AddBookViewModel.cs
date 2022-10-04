using Microsoft.Win32;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TestBook.BL;
using TestBook.Commands;
using TestBook.Model;
using TestBook.View;

namespace TestBook.ViewModel
{
    public class AddBookViewModel : ViewModelBase
    {
        private readonly BookContext _db;
        private ICommand _addImageCommand;
        private ICommand _closeAndAddCommand;

        public ICommand CloseAndAddCommand => _closeAndAddCommand ?? (_closeAndAddCommand = new RelayCommand(CloseWindowCommand));
        public ICommand AddImageCommand => _addImageCommand ?? (_addImageCommand = new RelayCommand(AddImage));

        private string _name;
        private string _author;
        private string _description;
        private string _date;
        private string _image;
        private string _isbn;

        public string Name 
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string ISBN
        {
            get { return _isbn; }
            set
            {
                _isbn = value;
                OnPropertyChanged("ISBN");
            }
        }

        public string Author
        {
            get { return _author; }
            set
            {
                _author = value;
                OnPropertyChanged("Name");
            }
        }
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }
        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }
        public Bitmap Image 
        {
            get { return ConvertImage.Byte64ToImg(_image); ; }
            set
            {
                _image = ConvertImage.BitmapToBase64(value);
                OnPropertyChanged("Image");
            }
        }

        public AddBookViewModel(BookContext db)
        {
            _db = db;
        }

        private void AddImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    Image = new Bitmap(openFileDialog.FileName);
                    Console.WriteLine();
                    _image = ConvertImage.BitmapToBase64(Image);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);  
                }
                
            }
        }
        private void CloseWindowCommand()
        {
            if (_name != null && _author != null)
            {
                if (_isbn != null && _date != null)
                {
                    if (_description != null && _image != null)
                    {
                        if (_db is not null)
                        {
                            Book book = new Book
                            {
                                Name = _name,
                                Image = _image,
                                ISBN = _isbn,
                                Date = _date,
                                Description = _description,
                                Authors = new List<Author> { new Author
                                {
                                Name = _author
                                } }
                            };
                            _db.Add(book);
                            _db.SaveChangesAsync();
                            foreach (Window item in Application.Current.Windows)
                            {
                                if (item.DataContext == this) item.Close();
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Один из аргументов пустой");    
        }
    }
}
