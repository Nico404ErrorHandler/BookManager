using De.HsFlensburg.ClientApp064.ImportXML.MessageBusMessages;
using De.HsFlensburg.ClientApp064.ImportXML.UiImportXML;
using De.HsFlensburg.ClientApp064.Services.MessageBus;

namespace De.HsFlensburg.ClientApp064.ImportXML.MessageBusLogic
{
    // own MessageListener for the import
    public class ImportXmlMessageListener
    {
        public bool BindableProperty => true;
        public ImportXmlMessageListener()
        {
            InitMessenger();
        }

        // Method to register the recipient of the "OpenImportXmlWindowMessage".
        // this allows something to happen when the "Import XML" button is pressed
        private void InitMessenger()
        {
            ServiceBus.Instance.Register<OpenImportXmlWindowMessage>(this, StartImportXml);
        }

        private void StartImportXml()
        {
            // open Import Window, when the Button "Import XML" is clicked
            (new ImportXmlWindow()).ShowDialog();
        }

    }
}
