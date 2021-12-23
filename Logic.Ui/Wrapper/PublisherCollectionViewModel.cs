using De.HsFlensburg.ClientApp064.Business.Model.BusinessObjects;
using De.HsFlensburg.ClientApp064.Logic.Ui.Base;
using System.ComponentModel;

namespace De.HsFlensburg.ClientApp064.Logic.Ui.Wrapper
{
    class PublisherCollectionViewModel : ViewModelSyncCollection<PublisherViewModel, Publisher, PublisherCollection>
    {
        public override void NewModelAssigned()
        {
            foreach (var bvm in this)
            {
                var modelPropChanged = bvm.Model as INotifyPropertyChanged;
                if (modelPropChanged != null)
                {
                    modelPropChanged.PropertyChanged += bvm.OnPropertyChangedInModel;
                }
            }
        }
    }
}
