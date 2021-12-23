using De.HsFlensburg.ClientApp064.Business.Model.BusinessObjects;
using De.HsFlensburg.ClientApp064.Logic.Ui.Base;
using System;

namespace De.HsFlensburg.ClientApp064.Logic.Ui.Wrapper
{
    public class ClientViewModel: ViewModelBase<Client>
    {
        public int Id 
        {
            get
            {
                return Model.Id;
            }
            set
            {
                Model.Id = value;
            }
        }
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
