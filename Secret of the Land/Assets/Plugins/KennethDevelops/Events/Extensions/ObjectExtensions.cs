using Plugins.KennethDevelops.Events;

namespace Plugins.KennethDevelops.Extensions {
    public static class ObjectExtensions {
        
        /// <summary>
        /// A shortcut for subscribing a callback to an event with a specific sender.
        /// </summary>
        /// <typeparam name="T">Type of the event that extends BaseEvent</typeparam>
        /// <param name="obj">The sender of the event</param>
        /// <param name="action">The callback to be triggered when the event is triggered</param>
        /// <param name="subscriber">The subscriber of the event</param>
        /// <param name="priority">The priority level for the callback. Default is 100.</param>
        /// <remarks>
        /// For more detailed information and examples, check the Subscribe Doc here: https://github.com/kennethdevelops/EventManagerDocs/wiki/Subscribing-to-Events
        /// </remarks>
        public static void Subscribe<T>(this object obj, System.Action<T> action, object subscriber, int priority = 100) where T : BaseEvent<T> {
            EventManager.Subscribe(action, subscriber, obj, priority);
        }
    }
}