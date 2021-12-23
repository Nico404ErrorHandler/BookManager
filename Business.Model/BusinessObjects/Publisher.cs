using System;
using System.ComponentModel;

namespace De.HsFlensburg.ClientApp064.Business.Model.BusinessObjects
{
    [Serializable]
    public class Publisher : INotifyPropertyChanged
    {
        public String Name { get; set; }


        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(
                    this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
