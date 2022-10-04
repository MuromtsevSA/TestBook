using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TestBook.BL;
using TestBook.Commands;
using TestBook.Model;

namespace TestBook.ViewModel
{
    public class EditBookViewModel : ViewModelBase
    {
        private readonly BookContext _db;
        private ICommand _addImageCommand;
        private ICommand _closeAndEditCommand;

        public ICommand AddImageCommand => _addImageCommand ?? (_addImageCommand = new RelayCommand(AddImage));

        public ICommand CloseAndEditCommand => _closeAndEditCommand ?? (_closeAndEditCommand = new RelayCommand(CloseAndEditWindowCommand));

        private int _id;
        private string _name;
        private string _auth;
        private string _description;
        private string _date;
        private string _image;
        private string _isbn;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

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
            get { return _auth; }
            set
            {
                _auth = value;
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

        public EditBookViewModel(BookContext db)
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

        private void CloseAndEditWindowCommand()
        {
            if (_id >= 0)
            {
                if (_name != null && _auth != null)
                {
                    if (_isbn != null && _date != null)
                    {
                        if (_description != null && _image != null)
                        {
                            if (_db is not null)
                            {
                                var entity = _db.Books.FirstOrDefault(x => x.Id == _id);
                                if (entity is not null)
                                {
                                    entity.ISBN = _isbn;
                                    entity.Name = _name;
                                    entity.Date = _date;
                                    entity.Description = _description;
                                    var author = _db.Authors.FirstOrDefault(x => x.Id == _id);
                                    if (author != null)
                                    {
                                        author.Name = _auth;
                                    }
                                    _db.SaveChangesAsync();
                                    foreach (Window item in Application.Current.Windows)
                                    {
                                        if (item.DataContext == this) item.Close();
                                    }
                                }
                            }
                            Console.WriteLine("DbCntext is null");
                        }
                    }
                }
                Console.WriteLine("Один из аргументов пустой");
            }
        }
    }
}
