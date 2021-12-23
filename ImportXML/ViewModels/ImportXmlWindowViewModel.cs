using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.Schema;
using System.ComponentModel;
using De.HsFlensburg.ClientApp064.ImportXML.MessageBusMessages;
using De.HsFlensburg.ClientApp064.Services.MessageBusImportXML;

namespace De.HsFlensburg.ClientApp064.ImportXML.ViewModels
{
    public class ImportXmlWindowViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public bool XmlIsValid { get; set; }
        //##################################################
        // These properties will hold the xml-data
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

        private string publisher;
        public string Publisher
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
                OnPropertyChanged("BindableCover");
            }
        }
        // makes the image displayable
        public BitmapImage BindableCover
        {
            get
            {
                if (Cover != null)
                {
                    MemoryStream localMemoryStream = new MemoryStream();
                    Cover.Save(localMemoryStream, System.Drawing.Imaging.ImageFormat.Png);
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
        //##################################################

        public ICommand LoadXmlCommand { get; }
        public ICommand ImportXmlCommand { get; }
        public ICommand CancelCommand { get; }

        public ImportXmlWindowViewModel()
        {
            XmlIsValid = false;

            LoadXmlCommand = new RelayCommand<Window>(LoadXml);
            ImportXmlCommand = new RelayCommand<Window>(ImportXML);
            CancelCommand = new RelayCommand<Window>(CancelMethod);
        }

        private void LoadXml(Window param)
        {
            XmlIsValid = true;

            // Open the OpenFileDialog
            string fileName = OpenFileDialogWin32Method();

            // Create XmlReader thats using a schema-File for Validation
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add("XmlSchemaBook.xsd", "./XmlSchemas/XmlSchemaBook.xsd");
            settings.ValidationEventHandler += new ValidationEventHandler(HandleXmlValidation);
            settings.ValidationFlags = settings.ValidationFlags | XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationType = ValidationType.Schema;

            XmlReader reader = XmlReader.Create(fileName, settings);

            // create Xml-Document-Object, this Object represents an xml-Document
            XmlDocument bookDataXml = new XmlDocument();
            bookDataXml.Load(reader);

            // what happens when the xml isnt valid, because it doesnt follow the xml-schema
            if (!XmlIsValid)
            {
                MessageBox.Show("Ihre XML-Datei ist ungültig. Bitte passen Sie diese gemäß dem Schema an.");
                return;
            }

            Console.WriteLine(bookDataXml.GetElementsByTagName("title").Item(0).InnerText);

            // what happens when the xml is valid
            Title = bookDataXml.GetElementsByTagName("title").Item(0).InnerText;
            Author = bookDataXml.GetElementsByTagName("author").Item(0).InnerText;
            Style = bookDataXml.GetElementsByTagName("style").Item(0).InnerText;
            ISBN = bookDataXml.GetElementsByTagName("isbn").Item(0).InnerText;
            Publisher = bookDataXml.GetElementsByTagName("publisher").Item(0).InnerText;
            Language = bookDataXml.GetElementsByTagName("language").Item(0).InnerText;
            Price = Decimal.Parse(bookDataXml.GetElementsByTagName("price").Item(0).InnerText);
            Edition = Int32.Parse(bookDataXml.GetElementsByTagName("edition").Item(0).InnerText);
            Year = Int32.Parse(bookDataXml.GetElementsByTagName("year").Item(0).InnerText);
            Genre = bookDataXml.GetElementsByTagName("genre").Item(0).InnerText;
            Location = bookDataXml.GetElementsByTagName("location").Item(0).InnerText;

            string path = bookDataXml.GetElementsByTagName("image").Item(0).InnerText;
            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(Path.GetDirectoryName(fileName), path);
            }
            Cover = Image.FromFile(path);
        }

        private void ImportXML(Window param)
        {
            // Cache the properties in a message in order to send it via the MessageBus.
            ImportBookFromXMLMessage message = new ImportBookFromXMLMessage();
            message.Title = Title;
            message.Author = Author;
            message.Style = Style;
            message.ISBN = ISBN;
            message.Publisher = Publisher;
            message.Language = Language;
            message.Price = Price;
            message.Edition = Edition;
            message.Year = Year;
            message.Genre = Genre;
            message.Location = Location;
            message.Cover = Cover;

            MessageBus.Instance.Send(message);
        }

        private void CancelMethod(Window param)
        {
            param.Close();
        }

        private string OpenFileDialogWin32Method()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            return ofd.FileName;
        }

        private void HandleXmlValidation(Object sender, ValidationEventArgs e)
        {
            // when this function is called, the XML isnt valid --> false
            XmlIsValid = false;
            // Code for Debugging :)
            Console.WriteLine("Message: " + e.Message);
        }

        // is needed for notifying that the property has changed
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
