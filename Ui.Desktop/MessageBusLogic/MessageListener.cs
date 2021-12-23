using De.HsFlensburg.ClientApp064.Logic.Ui.MessageBusMessages;
using De.HsFlensburg.ClientApp064.Services.MessageBus;

namespace De.HsFlensburg.ClientApp064.Ui.Desktop.MessageBusLogic
{
    class MessageListener
    {
       public bool BindableProperty => true; 
       public MessageListener()
        {
            InitMessenger();
        }

        private void InitMessenger()
        {
            ServiceBus.Instance.Register<OpenNewBookWindowMessage>(this, OpenBookWindow);
            ServiceBus.Instance.Register<OpenExportWindowMessage>(this, OpenExportWindow);
            ServiceBus.Instance.Register<OpenPrintWindowMessage>(this, OpenPrintWindow);
            ServiceBus.Instance.Register<OpenAddBibWindowMessage>(this, OpenAddBibWindow);
        }

        private void OpenExportWindow()
        {
            ExportWindow myWindow = new ExportWindow();
            myWindow.ShowDialog();
        }

        private void OpenBookWindow()
        {
            NewBookWindow myWindow = new NewBookWindow();
            myWindow.ShowDialog();
        }
        private void OpenPrintWindow()
        {
            PrintWindow myWindow = new PrintWindow();
            myWindow.ShowDialog();
        }
        private void OpenAddBibWindow()
        {
            AddBibWindow myWindow = new AddBibWindow();
            myWindow.ShowDialog();
        }
    }
}
