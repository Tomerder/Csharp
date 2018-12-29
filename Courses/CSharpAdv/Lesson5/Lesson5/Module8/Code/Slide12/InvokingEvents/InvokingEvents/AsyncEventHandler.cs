using System;

namespace InvokingEvents
{
    /// <summary>
    /// Represents a type-safe event whose handlers are executed asynchronously
    /// when the event is raised.
    /// </summary>
    /// <typeparam name="TEventArgs">The event arguments type for this event.</typeparam>
    public sealed class AsyncEventHandler<TEventArgs> where TEventArgs : EventArgs
    {
        private EventHandler<TEventArgs> _handlers;

        public void AddHandler(EventHandler<TEventArgs> handler)
        {
            _handlers = (EventHandler<TEventArgs>)Delegate.Combine(_handlers, handler);
        }

        public void RemoveHandler(EventHandler<TEventArgs> handler)
        {
            _handlers = (EventHandler<TEventArgs>)Delegate.Remove(_handlers, handler);
        }

        public void Invoke(object sender, TEventArgs args)
        {
            AsyncInvoker invoker = new AsyncInvoker(_handlers);
            invoker.Invoke(sender, args);
        }
    }
}
