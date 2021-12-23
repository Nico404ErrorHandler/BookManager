using System.ComponentModel;

namespace De.HsFlensburg.ClientApp064.Logic.Ui.Base
{
    public interface IViewModel<TypeOfModel> : INotifyPropertyChanged
    {
        TypeOfModel Model { get; set; }

        void NewModelAssigned();
    }
}
