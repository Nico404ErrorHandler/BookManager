using De.HsFlensburg.ClientApp064.Business.Model.BusinessObjects;
using De.HsFlensburg.ClientApp064.Logic.Ui.Base;
using System.Drawing;
using System.Windows.Media.Imaging;
using System;
using System.IO;

namespace De.HsFlensburg.ClientApp064.Logic.Ui.Wrapper
{
    public class BookViewModel: ViewModelBase<Book>
    {
        public BitmapImage BindableCover
        {
            get
            {
                if (Model.Cover != null)
                {
                    MemoryStream localMemoryStream = new MemoryStream();
                    Model.Cover.Save(localMemoryStream, System.Drawing.Imaging.ImageFormat.Png);
                    localMemoryStream.Position = 0;
                    BitmapImage localBitmapImage = new BitmapImage();
                    localBitmapImage.BeginInit();
                    localBitmapImage.StreamSource = localMemoryStream;
                    localBitmapImage.EndInit();
                    return localBitmapImage;
                }
                else return null;
            }
            private set { }
        }

        public String Title
        {
            get
            {
                return Model.Title;
            }
            set
            {
                Model.Title = value;
            }
        }

        public String Author
        {
            get
            {
                return Model.Author;
            }
            set
            {
                Model.Author = value;
            }
        }

        public String Style
        {
            get
            {
                return Model.Style;
            }
            set
            {
                Model.Style = value;
            }
        }

        public String ISBN
        {
            get
            {
                return Model.ISBN;
            }
            set
            {
                Model.ISBN = value;
            }
        }

        public String Publisher
        {
            get
            {
                return Model.Publisher.Name;
            }
            set
            {
                // Code to create the object goes here into the property, so that this can be of the type string so the assignment 
                //in NewBookWindowViewModel works again with bvm.Publisher
                Publisher myNewPublisher = new Publisher();
                myNewPublisher.Name = value;
                Model.Publisher = myNewPublisher;
            }
        }

        public String Language
        {
            get
            {
                return Model.Language;
            }
            set
            {
                Model.Language = value;
            }
        }

        public Decimal Price
        {
            get
            {
                return Model.Price;
            }
            set
            {
                Model.Price = value;
            }
        }

        public int Edition
        {
            get
            {
                return Model.Edition;
            }
            set
            {
                Model.Edition = value;
            }
        }

        public int Year
        {
            get
            {
                return Model.Year;
            }
            set
            {
                Model.Year = value;
            }
        }

        public String Genre
        {
            get
            {
                return Model.Genre;
            }
            set
            {
                Model.Genre = value;
            }
        }

        public String Location
        {
            get
            {
                return Model.Location;
            }
            set
            {
                Model.Location = value;
            }
        }

        public Image Cover
        {
            get
            {
                return Model.Cover;
            }
            set
            {
                Model.Cover = value;
                OnPropertyChanged("Cover");
                OnPropertyChanged("BindableCover");
            }
        }

        public override void NewModelAssigned()
        {
            throw new NotImplementedException();
        }
    }
}
