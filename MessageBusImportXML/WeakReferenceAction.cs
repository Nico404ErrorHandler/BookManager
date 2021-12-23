using System;

namespace De.HsFlensburg.ClientApp064.Services.MessageBusImportXML
{
    class WeakReferenceAction
    {
        private WeakReference target;
        private Action<object> action;

        public WeakReferenceAction(object target, Action<object> action)
        {
            this.target = new WeakReference(target);
            this.action = action;
        }
        public WeakReference Target
        {
            get
            {
                return target;
            }
        }
        public virtual void Execute(object param)
        {
            if (action != null && target != null && target.IsAlive)
                action.Invoke(param);
        }

        public void Unload()
        {
            if (this.action != null)
            {
                target = null;
                action = null;
            }
        }
    }
}
