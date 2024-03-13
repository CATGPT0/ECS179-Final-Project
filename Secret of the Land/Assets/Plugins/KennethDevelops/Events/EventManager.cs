using System;
using System.Collections.Generic;
using System.Reflection;

namespace Plugins.KennethDevelops.Events {

    /// <summary>
    /// This static class is a central point for managing events in your game. It allows you to subscribe and unsubscribe to events,
    /// which can then be triggered elsewhere in your code when a particular game action occurs.
    /// </summary>
    /// <remarks>
    /// For more detailed information, check the full documentation here: https://github.com/kennethdevelops/EventManagerDocs/wiki
    /// </remarks>
    public static class EventManager {
        
        /// <summary>
        /// This dictionary contains callbacks to unsubscribe methods for each event type.
        /// </summary>
        private static List<Type> _subscribedEventTypes = new List<Type>();

        /// <summary>
        /// Subscribes the given callback to the specified event type with an optional priority.
        /// </summary>
        /// <remarks>
        /// For more detailed information and examples, check the Subscribe Doc here: https://github.com/kennethdevelops/EventManagerDocs/wiki/Subscribing-to-Events
        /// </remarks>
        public static void Subscribe<TEvent>(Action<TEvent> callback, object subscriber,
                                             int            priority = 100) where TEvent : BaseEvent<TEvent> {
            BaseEvent<TEvent>.Subscribe(callback, subscriber, priority);
            
            var genericClassType = typeof(BaseEvent<>).MakeGenericType(typeof(TEvent));
            
            if (!_subscribedEventTypes.Contains(genericClassType))
                _subscribedEventTypes.Add(genericClassType);
        }

        /// <summary>
        /// Subscribes the given callback to the specified event type with an optional priority, 
        /// and limits the callback to events triggered by the specified sender object.
        /// </summary>
        /// <remarks>
        /// For more detailed information and examples, check the Subscribe Doc here: https://github.com/kennethdevelops/EventManagerDocs/wiki/Subscribing-to-Events
        /// </remarks>
        public static void Subscribe<TEvent>(Action<TEvent> callback, object subscriber, object sender,
                                             int            priority = 100) where TEvent : BaseEvent<TEvent> {
            BaseEvent<TEvent>.Subscribe(callback, subscriber, sender, priority);
            
            var genericClassType = typeof(BaseEvent<>).MakeGenericType(typeof(TEvent));
            
            if (!_subscribedEventTypes.Contains(genericClassType))
                _subscribedEventTypes.Add(genericClassType);
        }

        /// <summary>
        /// Unsubscribes the given callback from the specified event type.
        /// </summary>
        /// <remarks>
        /// For more detailed information and examples, check the Unsubscribe Doc here: https://github.com/kennethdevelops/EventManagerDocs/wiki/Unsubscribing-from-Events
        /// </remarks>
        public static void Unsubscribe<TEvent>(Action<TEvent> callback) where TEvent : BaseEvent<TEvent> {
            BaseEvent<TEvent>.Unsubscribe(callback);
        }
        
        /// <summary>
        /// Unsubscribes the specified subscriber from all events it is currently subscribed to.
        /// </summary>
        /// <remarks>
        /// For more detailed information and examples, check the Unsubscribe Doc here: https://github.com/kennethdevelops/EventManagerDocs/wiki/Unsubscribing-from-Events
        /// </remarks>
        public static void Unsubscribe(object subscriber){
            foreach (var eventType in _subscribedEventTypes) {
                var unsubscribeMethod = eventType.GetMethod("Unsubscribe", BindingFlags.Static | BindingFlags.Public, null, new[] { typeof(object) }, null);

                unsubscribeMethod?.Invoke(null, new[]{ subscriber });
            }
        }


        public static void Subscribe(Type eventType, Action callback, object subscriber, int priority = 100)
        {
            // Get the closed generic BaseEvent<> type
            Type genericClassType = typeof(BaseEvent<>).MakeGenericType(eventType);

            // Now get the 'Subscribe' method from the closed generic type
            var subscribeMethod = genericClassType.GetMethod("Subscribe", BindingFlags.Static | BindingFlags.Public, null, new Type[] { typeof(Action), typeof(object), typeof(int) }, null);


            // Construct a delegate of the right type
            // Delegate actionDelegate = Delegate.CreateDelegate(typeof(Action), callback.Target, callback.Method);


            // Now we can invoke the method
            subscribeMethod?.Invoke(null, new[] { callback, subscriber, priority });

            if (!_subscribedEventTypes.Contains(genericClassType))
                _subscribedEventTypes.Add(genericClassType);
        }

    }

}
