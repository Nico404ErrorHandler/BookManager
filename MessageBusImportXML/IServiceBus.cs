using System;

namespace De.HsFlensburg.ClientApp064.Services.MessageBusImportXML
{
    interface IServiceBus
    {
        void Register<TNotification>(object listener, Action<TNotification> action);
        void Send<TNotification>(TNotification notification);
        void Unregister<TNotification>(object listener);
    }
}
