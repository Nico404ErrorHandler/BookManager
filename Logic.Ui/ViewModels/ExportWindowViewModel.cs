using De.HsFlensburg.ClientApp064.Logic.Ui.Wrapper;
using De.HsFlensburg.ClientApp064.Services.ExportInXml;
using System;
using System.Windows;
using System.Windows.Input;

namespace De.HsFlensburg.ClientApp064.Logic.Ui.ViewModels
{
    public class ExportWindowViewModel
    {
        //-----ICommand for button-----//
        public ICommand ExportBookCollectionCommand { get; }

        //-----Fields-----//
        private ExportFileHandler exportFileHandler;

        //-----Propertys-----//
        public BookCollectionViewModel MyList { get; }
        public BookViewModel Book { get; set; }
        public String Filename { get; set; }
        public String Path { get; set; }
        public RelayCommand AbortExport { get; }

        //-----Parameterized Constructor-----//
        public ExportWindowViewModel(BookCollectionViewModel viewModelCollection)
        {
            ExportBookCollectionCommand = new RelayCommand(ExportBookMethod);
            MyList = viewModelCollection;

            exportFileHandler = new ExportFileHandler();
        }

        //-----ExportBookMethod()-----//
        public void ExportBookMethod()
        {
            try
            {
            
                // If filename is not set, it will save the xml under the name: default
                if (Filename == null)
                {
                    Filename = "default";
                }
                exportFileHandler.WriteModelToFile(Path + "/" + Filename + ".xml", Book.Model);
            }

            // I have a little problem here. I wan't to catch the NullReferenceException if no book is choosen for export.
            // But the IDE Visual Studio Community will stop and break at the given Exception at runtime.
            // So the solution is to continue after the IDE breaks in the code, just press the "Continue"-Button and the programm will proceed.
            // After that the catch-phrase gets executed and a window will pop up with a error-message.
            // I think this is a problem with the IDE and i don't really know how to get around it, so i will take this as given.
            catch(NullReferenceException)
            {
                MessageBox.Show("Please choose a book for export !", "Error");
            }
                
        }
    }
}
