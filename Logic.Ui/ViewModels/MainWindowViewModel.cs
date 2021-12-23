using De.HsFlensburg.ClientApp064.ImportXML.MessageBusLogic;
using De.HsFlensburg.ClientApp064.ImportXML.MessageBusMessages;
using De.HsFlensburg.ClientApp064.Logic.Ui.MessageBusMessages;
using De.HsFlensburg.ClientApp064.Logic.Ui.Wrapper;
using De.HsFlensburg.ClientApp064.Services.MessageBus;
using De.HsFlensburg.ClientApp064.Services.MessageBusImportXML;
using De.HsFlensburg.ClientApp064.Services.SerializationService;
using System;
using System.Windows.Input;

namespace De.HsFlensburg.ClientApp064.Logic.Ui.ViewModels
{
    public class MainWindowViewModel
    {
        private ModelFileHandler modelFileHandler;
        private string pathForSerialization;
        public ICommand SaveCommand { get; }
        public ICommand LoadCommand { get; }
        public ICommand OpenNewBookWindowCommand { get; }
        public ICommand ExportInXmlCommand { get; }
        public ICommand ImportXmlCommand { get; }
        public ICommand ImportInTexFilesCommand { get; }
        public ICommand OpenPrintWindowCommand { get; }
        public ICommand SearchCommand { get; }

        private void OpenNewBookWindowMethod()
        {
            ServiceBus.Instance.Send(new OpenNewBookWindowMessage());
        }


        public BookCollectionViewModel MyList { get; set; }

        public MainWindowViewModel(BookCollectionViewModel viewModelCollection)
        {
            SaveCommand = new RelayCommand(SaveModel);
            LoadCommand = new RelayCommand(LoadModel);
            OpenNewBookWindowCommand = new RelayCommand(OpenNewBookWindowMethod);

            // Import XML
            ImportXmlCommand = new RelayCommand(OpenImportXmlWindowMethod); //Lea

            // Export XML
            ExportInXmlCommand = new RelayCommand(OpenExportWindowMethod); //Nico

            //Print
            OpenPrintWindowCommand = new RelayCommand(OpenPrintWindowMethod); //Julian

            //Tex
            ImportInTexFilesCommand = new RelayCommand(OpenAddBibWindowMethod); //Michael

            MyList = viewModelCollection;
            modelFileHandler = new ModelFileHandler();
            pathForSerialization = Environment.GetFolderPath(
                Environment.SpecialFolder.MyDocuments) +
                "\\BookCollectionSerialization";

            //Initialize the MessageListener from ImportXML, so that it can register at the ServiceBus.
            new ImportXmlMessageListener();
            MessageBus.Instance.Register<ImportBookFromXMLMessage>(this, AddBookFromXML);
        }

        private void SaveModel()
        {
            modelFileHandler.WriteModelToFile(pathForSerialization, MyList.Model);
        }

        private void LoadModel()
        {
            MyList.Model = modelFileHandler.ReadModelFromFile(pathForSerialization);
        }

        private void OpenExportWindowMethod()
        {
            ServiceBus.Instance.Send(new OpenExportWindowMessage());
        }

        private void OpenAddBibWindowMethod()
        {
            ServiceBus.Instance.Send(new OpenAddBibWindowMessage());
        }

        private void OpenImportXmlWindowMethod()
        {
            // create new message in MessageBus to decouple XML import from everything else
            ServiceBus.Instance.Send(new OpenImportXmlWindowMessage());
        }
        private void OpenPrintWindowMethod()
        {
            ServiceBus.Instance.Send(new OpenPrintWindowMessage());
        }

        // Adding a book to the collection based on the transferred data
        // this Listener is listening to the message from the ServiceBus
        // needed for Import XML
        private void AddBookFromXML(ImportBookFromXMLMessage message)
        {
            BookViewModel bvm = new BookViewModel();
            bvm.Title = message.Title;
            bvm.Author = message.Author;
            bvm.Style = message.Style;
            bvm.ISBN = message.ISBN;
            bvm.Publisher = message.Publisher;
            bvm.Language = message.Language;
            bvm.Price = message.Price;
            bvm.Edition = message.Edition;
            bvm.Year = message.Year;
            bvm.Genre = message.Genre;
            bvm.Location = message.Location;
            bvm.Cover = message.Cover;

            MyList.Add(bvm);
        }
    }
}
