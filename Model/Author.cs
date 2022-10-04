using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TestBook.ViewModel;

namespace TestBook.Model
{
    public class Author : ViewModelBase
    {
        private string _name;
        public int Id { get; set; }
        public string Name {

            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
