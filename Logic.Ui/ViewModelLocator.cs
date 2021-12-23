using De.HsFlensburg.ClientApp064.Logic.Ui.ViewModels;
using De.HsFlensburg.ClientApp064.Logic.Ui.Wrapper;

namespace De.HsFlensburg.ClientApp064.Logic.Ui
{
    public class ViewModelLocator
    {
        public BookCollectionViewModel TheBookCollectionViewModel { get; set; }
        public MainWindowViewModel TheMainWindowViewModel { get; set; }
        public NewBookWindowViewModel TheNewBookWindowViewModel { get; set; }
        public ExportWindowViewModel TheExportWindowViewModel { get; set; }
        public PrintWindowViewModel ThePrintWindowViewModel { get; set; }
        public AddBibViewModel TheAddBibViewModel { get; set; }

        public ViewModelLocator()
        {
            TheBookCollectionViewModel = new BookCollectionViewModel();
            TheMainWindowViewModel = new MainWindowViewModel(TheBookCollectionViewModel);
            TheNewBookWindowViewModel = new NewBookWindowViewModel(TheBookCollectionViewModel);
            TheExportWindowViewModel = new ExportWindowViewModel(TheBookCollectionViewModel);
            ThePrintWindowViewModel = new PrintWindowViewModel(TheBookCollectionViewModel);
            TheAddBibViewModel = new AddBibViewModel(TheBookCollectionViewModel);
        }
    }
}
