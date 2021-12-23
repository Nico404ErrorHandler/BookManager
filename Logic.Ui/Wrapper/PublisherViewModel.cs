using System;
using De.HsFlensburg.ClientApp064.Business.Model.BusinessObjects;
using De.HsFlensburg.ClientApp064.Logic.Ui.Base;

namespace De.HsFlensburg.ClientApp064.Logic.Ui.Wrapper
{
    public class PublisherViewModel : ViewModelBase<Publisher>
    {

        public String Name
        {
            get
            {
                return Model.Name;
            }
            set
            {
                Model.Name = value;
            }
        }

        public override void NewModelAssigned()
        {
            throw new NotImplementedException();
        }

    }
}
