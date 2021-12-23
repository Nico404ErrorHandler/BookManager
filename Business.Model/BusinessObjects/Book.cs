using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace De.HsFlensburg.ClientApp064.Business.Model.BusinessObjects
{
    [Serializable]
    public class Book : INotifyPropertyChanged
    {   
        private String title;
        public String Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        private String author;
        public String Author
        {
            get
            {
                return author;
            }
            set
            {
                author = value;
                OnPropertyChanged("Author");
            }
        }

        private String style;
        public String Style
        {
            get
            {
                return style;
            }
            set
            {
                style = value;
                OnPropertyChanged("Style");
            }
        }

        private String isbn;
        public String ISBN
        {
            get
            {
                return isbn;
            }
            set
            {
                isbn = value;
                OnPropertyChanged("ISBN");
            }
        }

        private Publisher publisher = new Publisher();
        public Publisher Publisher
        {
            get
            {
                return publisher;
            }
            set
            {
                publisher = value;
                OnPropertyChanged("Publisher");
            }
        }

        private String language;
        public String Language
        {
            get
            {
                return language;
            }
            set
            {
                language = value;
                OnPropertyChanged("Language");
            }
        }

        private Decimal price;
        public Decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }

        private int edition;
        public int Edition
        {
            get
            {
                return edition;
            }
            set
            {
                edition = value;
                OnPropertyChanged("Edition");
            }
        }

        private int year;
        public int Year
        {
            get
            {
                return year;
            }
            set
            {
                year = value;
                OnPropertyChanged("Year");
            }
        }

        private String genre;
        public String Genre
        {
            get
            {
                return genre;
            }
            set
            {
                genre = value;
                OnPropertyChanged("Genre");
            }
        }

        private String location;
        public String Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
                OnPropertyChanged("Location");
            }
        }

        private Image cover;
        public Image Cover
        {
            get
            {
                return cover;
            }
            set
            {
                cover = value;
                OnPropertyChanged("Cover");
            }
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(
                    this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
