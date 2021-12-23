using De.HsFlensburg.ClientApp064.Logic.Ui.Wrapper;
using De.HsFlensburg.ClientApp064.Services.MessageBus;
using De.HsFlensburg.ClientApp064.Services.AddBib;
using De.HsFlensburg.ClientApp064.Services.SerializationService;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace De.HsFlensburg.ClientApp064.Logic.Ui.ViewModels
{
    public class AddBibViewModel
    {
        private ModelFileHandler modelFileHandler;
        private string pathForSerialization;
        public ICommand SaveBibCommand { get; }
        public ICommand LoadBibCommand { get; }
        public ICommand OpenBibFileDialog { get; }
        public BookCollectionViewModel MyBibList { get; set; }
        private AnalyzeString analyzeString;

        public AddBibViewModel(BookCollectionViewModel bibViewModelCollection)
        {
            SaveBibCommand = new RelayCommand(SaveBibModel);
            LoadBibCommand = new RelayCommand(LoadBibModel);
            MyBibList = bibViewModelCollection;
            modelFileHandler = new ModelFileHandler();
            pathForSerialization = Environment.GetFolderPath(
                Environment.SpecialFolder.MyDocuments) +
                "\\BookCollectionSerialization";
            OpenBibFileDialog = new RelayCommand(OpenFileDialogWin32Method);
            analyzeString = new AnalyzeString();
        }

        //LoadBibModel reads the Model form the savefile should one be saved
        private void LoadBibModel()
        {
            try
            {
                MyBibList.Model = modelFileHandler.ReadModelFromFile(pathForSerialization);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //saveModel saves the Model to a file
        private void SaveModel()
        {
            modelFileHandler.WriteModelToFile(pathForSerialization, MyBibList.Model);
        }

        //Opens a file Dialog and reads the selected File to a String Array
        private void OpenFileDialogWin32Method()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            string[] lines = System.IO.File.ReadAllLines(ofd.FileName);
            categorizeString(lines);
        }

        //Invokes the Categories Method in Services in order to parse the string and get an array
        //Provides the AddBookMethod with the Informations of a book
        private void categorizeString(string[] lines)
        {
            int index = 0;
            while (index >= 0 && index < lines.Length)
            {
                if (lines[index] != "" && lines[index][0] == '@' && lines[index][1] == 'B')
                {
                    string[] book = analyzeString.categories(lines, index); 
                    AddBookMethod(book);
                }
                index++;
            }
        }

        //puts book informations into the Model
        private void AddBookMethod(string[] book)
        {
            BookViewModel bvm = new BookViewModel();
            bvm.Title = book[4];
            bvm.Author = book[0];
            bvm.ISBN = book[5];
            bvm.Publisher = book[2];
            string yearNumberStr = book[1];
            int yearNumber;
            bool isParsableYear = Int32.TryParse(yearNumberStr, out yearNumber);
            if (isParsableYear)
            {
                bvm.Year = yearNumber;
            }
            else
            {
                bvm.Year = 0;
            }

            string editionNumberStr = book[3];
            int editionNumber;
            bool isParsableEdition = Int32.TryParse(editionNumberStr, out editionNumber);
            if (isParsableEdition)
            {
                bvm.Edition = editionNumber;
            }
            else
            {
                bvm.Edition = 0;
            }
            MyBibList.Add(bvm);
        }

        //Saves the Model to File
        private void SaveBibModel()
        {
            modelFileHandler.WriteModelToFile(pathForSerialization, MyBibList.Model);
        }
    }
}
