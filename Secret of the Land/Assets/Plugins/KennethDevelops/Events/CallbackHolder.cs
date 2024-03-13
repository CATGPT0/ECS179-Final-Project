using System;
using Object = UnityEngine.Object;

namespace Plugins.KennethDevelops.Events {
    /// <summary>
    /// Represents a callback holder which contains a callback, subscriber, and a sender.
    /// </summary>
    /// <typeparam name="T">The type of the event associated with the callback.</typeparam>
    /// <remarks>
    /// For more detailed information, check the full documentation here: [CallbackHolder Doc Link]
    /// </remarks>
    public class CallbackHolder<T> {
        /// <summary>
        /// Represents the callback associated with a specific event.
        /// </summary>
        public Action<T> callback;

        public Action noParamCallback;

        /// <summary>
        /// Represents the subscriber object that subscribed to a specific event.
        /// </summary>
        public object subscriber;

        /// <summary>
        /// Represents the sender object that triggers a specific event.
        /// </summary>
        public object sender;

        /// <summary>
        /// Checks whether the subscriber is still active.
        /// </summary>
        /// <returns>Returns true if the subscriber is still active, false otherwise.</returns>
        public bool IsAlive() {
            if (subscriber is Object unityObject) {
                return unityObject.ToString() != "null";
            }

            return subscriber != null;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current CallbackHolder.
        /// </summary>
        /// <param name="obj">The object to compare with the current CallbackHolder.</param>
        /// <returns>True if the specified object is equal to the current CallbackHolder; otherwise, false.</returns>
        public override bool Equals(object obj) {
            if (obj is Action<T> callback) {
                return Equals(callback);
            }

            if (obj is Action noParamCallback) {
                return Equals(noParamCallback);
            }

            if (obj is CallbackHolder<T> holder) {
                if (holder.callback != null)
                    return Equals(holder.callback);
                if (holder.subscriber != null)
                    return subscriber == holder.subscriber;
            }

            return base.Equals(obj);
        }

        /// <summary>
        /// Determines whether the specified Action is equal to the current callback.
        /// </summary>
        /// <param name="callback">The Action to compare with the current callback.</param>
        /// <returns>True if the specified Action is equal to the current callback; otherwise, false.</returns>
        public bool Equals(Action<T> callback) {
            return callback == this.callback;
        }
        
        public bool Equals(Action callback) {
            return callback == this.noParamCallback;
        }
    }

}