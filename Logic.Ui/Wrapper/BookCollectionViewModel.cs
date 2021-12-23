using De.HsFlensburg.ClientApp064.Business.Model.BusinessObjects;
using De.HsFlensburg.ClientApp064.Logic.Ui.Base;
using System.ComponentModel;

namespace De.HsFlensburg.ClientApp064.Logic.Ui.Wrapper
{
    public class BookCollectionViewModel : ViewModelSyncCollection<BookViewModel, Book, BookCollection>
    {
        public override void NewModelAssigned()
        {
            // if the new model comes e.g. from deserialization, then for each element of the list 
            //(here: each client in the ClientCollection) the wrapper class has to register in the PropertyChanged event again.

            foreach (var bvm in this)
            {
                var modelPropChanged = bvm.Model as INotifyPropertyChanged;
                if(modelPropChanged != null)
                {
                    modelPropChanged.PropertyChanged += bvm.OnPropertyChangedInModel;
                }
            }
        }
    }
}
