using System;
using System.Collections.Generic;
using System.Threading;

namespace De.HsFlensburg.ClientApp064.Services.MessageBusImportXML
{
    public class MessageBus : IServiceBus
    {
        private static volatile MessageBus instance;
        private static object syncRoot = new Object();
        private Dictionary<Type, List<WeakReferenceAction>> references =
            new Dictionary<Type, List<WeakReferenceAction>>();
        #region Singleton
        private MessageBus() { }
        public static MessageBus Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new MessageBus();
                    }
                }
                return instance;
            }
        }
        #endregion Singleton
        public void Register<TNotification>(object listener, Action<TNotification> action)
        {
            Type messageType = typeof(TNotification);
            if (!references.ContainsKey(messageType))
                references.Add(messageType, new List<WeakReferenceAction>());
            WeakReferenceAction actionReference = new WeakReferenceAction(listener, new Action<object>((param) => action((TNotification) param)));
            references[messageType].Add(actionReference);
        }
        public void Send<TNotification>(TNotification notification)
        {
            Type messageType = typeof(TNotification);
            List<WeakReferenceAction> weakReferenceActions = references[messageType];
            foreach (WeakReferenceAction wra in weakReferenceActions)
            {
                wra.Execute(notification);
            }
        }
        public void Unregister<TNotification>(object listener)
        {
            bool isLocked = false;
            try
            {
                Monitor.Enter(references, ref isLocked);
                foreach (Type targetType in references.Keys)
                {
                    foreach (WeakReferenceAction wra in references[targetType])
                    {
                        if (wra.Target == listener)
                            wra.Unload();
                    }
                }
            }
            finally
            {
                if (isLocked)
                    Monitor.Exit(references);
            }
        }
    }
}
