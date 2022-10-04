using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Linq;
using TestBook.BL;
using TestBook.ViewModel;

namespace TestBook.Model
{
    public class Book : ViewModelBase
    {
        private string _name;
        private string _date;
        private string _isbn;
        private string _description;
        private string _image;
        private Bitmap _img;
        public int Id { get; set; }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
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
        public string ISBN
        {
            get { return _isbn; }
            set
            {
                _isbn = value;
                OnPropertyChanged("ISBN");
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
        public string Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }

        [NotMapped]
        public ImageSource Img
        {
            get { return ConvertImage.BitpamToImageSource(ConvertImage.Byte64ToImg(_image)); }
            set
            {
                _image = ConvertImage.BitmapToBase64(ConvertImage.ImageSourceToBitMap(value));
                OnPropertyChanged("Img");
            }
        }

        public List<Author> Authors { get; set; }

       
    }
}
