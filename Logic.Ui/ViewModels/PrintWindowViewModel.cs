using De.HsFlensburg.ClientApp064.Logic.Ui.Wrapper;
using De.HsFlensburg.ClientApp064.Services.PrintingService;
using System;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Timers;
using System.Windows.Input;

namespace De.HsFlensburg.ClientApp064.Logic.Ui.ViewModels
{
    public class PrintWindowViewModel : INotifyPropertyChanged
    {
        public BookCollectionViewModel BookCollectionViewModel { get; }
        private PrintBook PrintBook = new PrintBook();
        private PrintDocument printDoc = new PrintDocument();
        private static Timer timer;
        public short Copies { get; set; } //gets changed in the PrintWindow direclty through binding
        public int FromPage { get; set; } //gets changed in the PrintWindow directly through binding
        public int ToPage { get; set; } //gets changed in the PrintWindow directly through binding
        public string[] listPrinter;
        public string[] ListPrinter
        {
            get
            {
                return listPrinter;
            }
            set
            {
                listPrinter = value;
                OnPropertyChanged("ListPrinter");
            }
        }
        public int SelectedPrinter { get; set; }
        private string[] listLandscape;
        public string[] ListLandscape
        {
            get
            {
                return listLandscape;
            }
            set
            {
                listLandscape = value;
                OnPropertyChanged("ListLandscape");
            }
        }
        public int SelectedLandscape { get; set; }
        public bool IsLandscape { get; set; }
        private string[] listPaperSize;
        public string[] ListPaperSize
        {
            get
            {
                return listPaperSize;
            }
            set
            {
                listPaperSize = value;
                OnPropertyChanged("ListPaperSize");
            }
        }
        public int SelectedPaperSize { get; set; }
        private string[] listPrinterResolution;
        public string[] ListPrinterResolution
        {
            get
            {
                return listPrinterResolution;
            }
            set
            {
                listPrinterResolution = value;
                OnPropertyChanged("ListPrinterResolution");
            }
        }
        public int SelectedPrinterResolution { get; set; }
        private string[] listDuplex;
        public string[] ListDuplex
        {
            get
            {
                return listDuplex;
            }
            set
            {
                listDuplex = value;
                OnPropertyChanged("ListDuplex");
            }
        }
        public int SelectedDuplex { get; set; }
        private string[] listColor;
        public string[] ListColor
        {
            get
            {
                return listColor;
            }
            set
            {
                listColor = value;
                OnPropertyChanged("ListColor");
            }
        }
        public int SelectedColor { get; set; }
        public int Index { get; set; }
        private int HelpPrinterValue { get; set; }
        public ICommand PrintCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;
        public PrintWindowViewModel(BookCollectionViewModel bookCollectionViewModel)
        {
            BookCollectionViewModel = bookCollectionViewModel;
            PrintCommand = new RelayCommand(SendPrinterSettings);
            HelpPrinterValue = SelectedPrinter;
            SetComboBoxPrinterContent();
            SelectedPrinter = PrinterSettings.InstalledPrinters.Count - 1;
            SetPrinterSettings();
            SetTimer();
        }
        //This method calls the PrinterChanged method every 1 second
        private void SetTimer()
        {
            timer = new Timer(1000);
            timer.Elapsed += PrinterChanged;
            timer.AutoReset = true;
            timer.Enabled = true;
        }
        //This method checks with the help of HelpPrinterValue if the selectedPrinter has changed or not
        //so that the SetPrinterSettings only gets called when the selected printer has changed
        private void PrinterChanged(object o, ElapsedEventArgs e)
        {
            if (SelectedPrinter != HelpPrinterValue)
            {
                SetPrinterSettings();
                HelpPrinterValue = SelectedPrinter;
            }
        }
        //This method calls all methods that are used to get the specific content for the comboboxes
        //This method gets called at the beginning to select the default printer and settings
        //It's combined with a timer to refresh the comboboxes values
        private void SetPrinterSettings()
        {
            printDoc.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters[SelectedPrinter];
            SetComboBoxPaperSizeContent();
            SetComboBoxLandscapeContent();
            SetComboBoxPrinterResolutionContent();
            SetComboBoxDuplexContent();
            SetComboBoxColorContent();
            Copies = 1;
            HelpPrinterValue = SelectedPrinter;
        }
        //This method gets all printers that are installed on the pc and stores it in an array
        //The values are displayed in the ComboBoxPrinter in the PrintWindow
        private void SetComboBoxPrinterContent()
        {
            string[] listPrinterInit = new string[PrinterSettings.InstalledPrinters.Count];
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                listPrinterInit[i] = PrinterSettings.InstalledPrinters[i];
            }
            ListPrinter = listPrinterInit;
        }
        //This method gets all supported papersizes of the selected printer and stores its values in an array
        //The first case limits the settings to 20 because of performance issues. The Values are displayed in
        //the PrintWindow in the ComboBoxPaperSize
        private void SetComboBoxPaperSizeContent()
        {
            if (printDoc.PrinterSettings.PaperSizes.Count > 20)
            {
                string[] listPaperSizeInit = new string[20];
                for (int i = 0; i < listPaperSizeInit.Length; i++)
                {
                    listPaperSizeInit[i] = printDoc.PrinterSettings.PaperSizes[i].Kind.ToString();
                }
                ListPaperSize = listPaperSizeInit;
            }
            else
            {
                string[] listPaperSizeInit = new string[printDoc.PrinterSettings.PaperSizes.Count];
                for (int i = 0; i < printDoc.PrinterSettings.PaperSizes.Count - 1; i++)
                {
                    listPaperSizeInit[i] = printDoc.PrinterSettings.PaperSizes[i].Kind.ToString();
                }
                ListPaperSize = listPaperSizeInit;
            }
            SelectedPaperSize = 0;
        }
        //This method stores the landscape settings in a string-array and displays it in the PrintWindow
        //in the ComboBoxLandscape
        private void SetComboBoxLandscapeContent()
        {
            string[] listLandscape = new string[2];
            listLandscape[0] = "Hochformat";
            listLandscape[1] = "Querformat";
            ListLandscape = listLandscape;
            SelectedLandscape = 0;
        }
        //This method defines the available printresolutions of the selected printer by asking the selected
        //printer for supported resolutions. The for-loop stores the information and displays its content
        //in the ComboBoxPrintResolution in the PrintWindow. It's limited to the four most wanted resolutions
        //for performance 
        private void SetComboBoxPrinterResolutionContent()
        {
            string[] listPrinterResolution = new string[4];
            for (int i = 0; i < listPrinterResolution.Length; i++)
            {
                listPrinterResolution[i] = printDoc.PrinterSettings.PrinterResolutions[i].Kind.ToString();
            }
            ListPrinterResolution = listPrinterResolution;
            SelectedPrinterResolution = 1;
        }
        //This method checks if the selected printer supports duplex print or not and displays for each case
        //the specific settings in the ComboBoxDuplex in the PrintWindow
        private void SetComboBoxDuplexContent()
        {
            if (printDoc.PrinterSettings.CanDuplex)
            {
                string[] listDuplex = new string[2];
                listDuplex[0] = "Nein";
                listDuplex[1] = "Ja";
                ListDuplex = listDuplex;
            }
            else
            {
                string[] listDuplex = new string[1];
                listDuplex[0] = "Duplex nicht verfügbar";
                ListDuplex = listDuplex;
            }
            SelectedDuplex = 0;
        }
        //This method asks the selected printer if colorprint is supported and shows then the specific
        //settings to each case where color is supported or not and gets displayed in the PrintWindow
        private void SetComboBoxColorContent()
        {
            if (printDoc.PrinterSettings.SupportsColor)
            {
                string[] listColor = new string[2];
                listColor[0] = "Schwarz/Weiß";
                listColor[1] = "Farbe";
                ListColor = listColor;
            }
            else
            {
                string[] listColor = new string[1];
                listColor[0] = "Schwarz/Weiß";
                ListColor = listColor;
            }
            SelectedColor = 0;
        }
        //GetBookDetails-method gets the information of a specific book of the bookcollection
        //Index determines which book of the collection gets stored in the bookDetails-String-Array
        private string[] GetBookDetails()
        {
            string[] bookDetails = new string[13];
            bookDetails[0] = BookCollectionViewModel[Index].BindableCover.ToString();
            bookDetails[1] = BookCollectionViewModel[Index].Title;
            bookDetails[2] = BookCollectionViewModel[Index].Author;
            bookDetails[3] = BookCollectionViewModel[Index].Style;
            bookDetails[4] = BookCollectionViewModel[Index].ISBN;
            bookDetails[5] = BookCollectionViewModel[Index].Publisher;
            bookDetails[6] = BookCollectionViewModel[Index].Language;
            bookDetails[7] = BookCollectionViewModel[Index].Price.ToString();
            bookDetails[8] = BookCollectionViewModel[Index].Edition.ToString();
            bookDetails[9] = BookCollectionViewModel[Index].Year.ToString();
            bookDetails[10] = BookCollectionViewModel[Index].Genre;
            bookDetails[11] = BookCollectionViewModel[Index].Location;
            bookDetails[12] = BookCollectionViewModel[Index].Cover.ToString();
            return bookDetails;
        }
        //SendPrinterSettings-method sends all settings and information of one book to the PrintBook-class
        private void SendPrinterSettings()
        {
            IsLandscape = SelectedLandscape == 1 ? true : false;
            try
            {
                string[] bookDetails = GetBookDetails();
                System.Drawing.Image bookCover = BookCollectionViewModel[Index].Cover;
                PrintBook.Print(PrinterSettings.InstalledPrinters[SelectedPrinter],
                    SelectedPaperSize, IsLandscape, Copies, FromPage, ToPage,
                    SelectedPrinterResolution, SelectedDuplex, bookDetails,
                    bookCover, SelectedColor);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        //OnPropertyChanged is neccessary to change the values of the comboboxes after changes are made
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
