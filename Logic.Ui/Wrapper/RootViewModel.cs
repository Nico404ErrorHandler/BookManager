using De.HsFlensburg.ClientApp064.Business.Model.BusinessObjects;
using De.HsFlensburg.ClientApp064.Logic.Ui.Base;
using System;

namespace De.HsFlensburg.ClientApp064.Logic.Ui.Wrapper
{
    public class RootViewModel : ViewModelBase<Root>
    {
        public BookCollection newBookCollection
        {
            get
            {
                return Model.BookCollection;
            }

            set
            {
                Model.BookCollection = value;
            }
        }

        public PublisherCollection newPublisherCollection
        {
            get
            {
                return Model.PublisherCollection;
            }

            set
            {
                Model.PublisherCollection = value;
            }
        }

        public override void NewModelAssigned()
        {
            throw new NotImplementedException();
        }
    }
}