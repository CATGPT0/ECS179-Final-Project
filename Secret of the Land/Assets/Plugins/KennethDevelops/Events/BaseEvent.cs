using System;
using System.Collections.Generic;
using System.Linq;
using KennethDevelops.DevUtils.Model;
#if KD_UNIVERSAL_TIME
using KennethDevelops.Time;
#endif
using UnityEngine;

namespace Plugins.KennethDevelops.Events {
    
    /// <summary>
    /// The BaseEvent class is an abstract base class for defining specific game events.
    /// It includes methods for subscribing, unsubscribing, and triggering the event.
    /// To create your own custom events, you should create a class that extends BaseEvent.
    /// </summary>
    /// <typeparam name="T">Type of the event that extends BaseEvent</typeparam>
    /// <remarks>
    /// For more detailed information, check the full documentation here: https://github.com/kennethdevelops/EventManagerDocs/wiki/Creating-your-own-Events
    /// </remarks>
    public abstract class BaseEvent<T> where T : BaseEvent<T>{

        /// <summary>
        /// An ordered list of callback holders for this event type.
        /// </summary>
        private static OrderedList<CallbackHolder<T>> _callbacks = new OrderedList<CallbackHolder<T>>();

        /// <summary>
        /// The sender of the event.
        /// </summary>
        public object sender;


        /// <summary>
        /// Trigger the event and call the subscribed callbacks, ensuring destroyed subscribers are cleaned up first.
        /// </summary>
        /// <remarks>
        /// For more detailed information and examples, check the Trigger Doc here: https://github.com/kennethdevelops/EventManagerDocs/wiki/Triggering-an-Event
        /// </remarks>
        public void Trigger(object sender) {
            this.sender = sender;

            #region Remove destroyed subscribers

            var toBeRemoved = new List<CallbackHolder<T>>();

            foreach (var callback in _callbacks.Select(n => n.element).Where(n => !n.IsAlive())) {
                Debug.LogWarning($"[EventManager] Trying to trigger event '{typeof(T)}', but a subscriber has been destroyed");
                toBeRemoved.Add(callback);
            }

            
            foreach (var callback in toBeRemoved) {
                _callbacks.Remove(callback);
            }
            
            #endregion

            #region Trigger callbacks

            foreach (var callbackHolder in _callbacks.Select(n => n.element)) {
                //we check if the subscriber asked for a specific sender
                //if so, we only trigger if the sender is the same
                if (callbackHolder.sender != null && callbackHolder.sender != sender)
                    continue;
                
                //we trigger the callback
                callbackHolder.callback?.Invoke(this as T);
                callbackHolder.noParamCallback?.Invoke();
            }

            #endregion
        }
        
        //You can only tr
        #if KD_UNIVERSAL_TIME
        /// <summary>
        /// Trigger the event after a delay.
        /// </summary>
        /// <remarks>
        /// For more detailed information and examples, check the Trigger Doc here: https://github.com/kennethdevelops/EventManagerDocs/wiki/Triggering-an-Event
        /// </remarks>
        public void Trigger(object sender, float delay, bool pausable = true) {
            UniversalTime.Instance.StartTimer(delay, () => Trigger(sender), pausable);
        }
        #endif
        
        /// <summary>
        /// Subscribes a callback to the event with an optional priority.
        /// </summary>
        /// <remarks>
        /// For more detailed information and examples, check the Subscribe Doc here: https://github.com/kennethdevelops/EventManagerDocs/wiki/Subscribing-to-Events
        /// </remarks>
        public static void Subscribe(Action<T> callback, object subscriber, int priority = 100) {
            _callbacks.Add(priority, new CallbackHolder<T>{ callback = callback, subscriber = subscriber });
        }

        /// <summary>
        /// Subscribes a callback to the event with a specific sender and priority.
        /// </summary>
        /// <remarks>
        /// For more detailed information and examples, check the Subscribe Doc here: https://github.com/kennethdevelops/EventManagerDocs/wiki/Subscribing-to-Events
        /// </remarks>
        public static void Subscribe(Action<T> callback, object subscriber, object sender, int priority) {
            _callbacks.Add(priority, new CallbackHolder<T>{ callback = callback, subscriber = subscriber, sender = sender});
        }
        
        public static void Subscribe(Action noParamCallback, object subscriber, int priority) {
            _callbacks.Add(priority, new CallbackHolder<T>{ noParamCallback = noParamCallback, subscriber = subscriber });
        }
        
        /// <summary>
        /// Unsubscribes a callback from the event.
        /// </summary>
        /// <remarks>
        /// For more detailed information and examples, check the Unsubscribe Doc here: https://github.com/kennethdevelops/EventManagerDocs/wiki/Unsubscribing-from-Events
        /// </remarks>
        public static void Unsubscribe(Action<T> callback) {
            _callbacks.RemoveAll(new CallbackHolder<T>{ callback = callback });
        }
        
        public static void Unsubscribe(Action callback) {
            _callbacks.RemoveAll(new CallbackHolder<T>{ noParamCallback = callback });
        }
        
        /// <summary>
        /// Unsubscribes a subscriber from the event.
        /// </summary>
        /// <remarks>
        /// For more detailed information and examples, check the Unsubscribe Doc here: https://github.com/kennethdevelops/EventManagerDocs/wiki/Unsubscribing-from-Events
        /// </remarks>
        public static void Unsubscribe(object subscriber) {
            _callbacks.RemoveAll(new CallbackHolder<T>{ subscriber = subscriber });
        }
        
        /// <summary>
        /// Attempts to retrieve the GameObject from the sender of the event.
        /// </summary>
        /// <remarks>
        /// For more detailed information and examples, check the Docs here: https://github.com/kennethdevelops/EventManagerDocs/wiki/Subscribing-to-Events
        /// </remarks>
        public bool TryGetSenderGameObject(out GameObject senderGameObject) {
            senderGameObject = null;
            
            if (sender == null) return false;

            if (sender is GameObject go) {
                senderGameObject = go;
                return true;
            }
            
            if (sender is Component component) {
                senderGameObject = component.gameObject;
                return true;
            }

            return false;
        }

    }
}