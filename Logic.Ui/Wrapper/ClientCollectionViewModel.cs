using De.HsFlensburg.ClientApp064.Business.Model.BusinessObjects;
using De.HsFlensburg.ClientApp064.Logic.Ui.Base;
using System.ComponentModel;

namespace De.HsFlensburg.ClientApp064.Logic.Ui.Wrapper
{
    public class ClientCollectionViewModel : ViewModelSyncCollection<ClientViewModel, Client, ClientCollection>
    {
        public override void NewModelAssigned()
        {
            // wenn das neue Model z.B. aus der Deserialiserung kommt, dann muss für jedes Element der Liste
            // (hier: jeder Client in der ClientCollection) sich die Wrapper-Klasse neu im PropertyVhanged-event registrieren

            foreach (var cvm in this)
            {
                var modelPropChanged = cvm.Model as INotifyPropertyChanged;
                if(modelPropChanged != null)
                {
                    modelPropChanged.PropertyChanged += cvm.OnPropertyChangedInModel;
                }
            }
        }
    }
}
