using De.HsFlensburg.ClientApp064.Logic.Ui.Wrapper;
using System.Drawing;
using System.Windows.Input;
using Microsoft.Win32;

namespace De.HsFlensburg.ClientApp064.Logic.Ui.ViewModels
{
    public class NewBookWindowViewModel
    {
        // Use of BookViewModel to avoid code duplication
        public BookViewModel BVM { get; set; }

        private BookCollectionViewModel bookCollectionViewModel;
        // ICommand properties needed so that xaml can bind to them
        public ICommand AddBook { get; }
        public ICommand OpenFileDialog { get; }

        public NewBookWindowViewModel(BookCollectionViewModel viewModelCollection)
        {
            AddBook = new RelayCommand(AddBookMethod);
            OpenFileDialog = new RelayCommand(OpenFileDialogWin32Method);
            bookCollectionViewModel = viewModelCollection;
            // This object holds the data that will be displayed in the window (NewBookWindow).
            BVM = new BookViewModel();
        }

        private void AddBookMethod()
        {
            // Transfer the data from the BVM property to a new object of type BookViewModel (bvm), to decouple properties 
            // so that the books in the list are not all the same object and are processed together.
            BookViewModel bvm = new BookViewModel();
            bvm.Title = BVM.Title;
            bvm.Author = BVM.Author;
            bvm.Style = BVM.Style;
            bvm.ISBN = BVM.ISBN;
            bvm.Year = BVM.Year;
            bvm.Language = BVM.Language;
            bvm.Publisher = BVM.Publisher;
            bvm.Price = BVM.Price;
            bvm.Edition = BVM.Edition;
            bvm.Genre = BVM.Genre;
            bvm.Location = BVM.Location;
            bvm.Cover = BVM.Cover;

            // Adding the new book to the Collection.
            bookCollectionViewModel.Add(bvm);
        }

        private void OpenFileDialogWin32Method()
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.ShowDialog();

            // load the selected image
            BVM.Cover = Image.FromFile(ofd.FileName);
        }
    }
}
