using UnityEngine;

namespace Plugins.KennethDevelops.Events {
    public abstract class BaseDispatcher<T> : MonoBehaviour where T : BaseEvent<T> {
        
        public void Dispatch(){
            GetEventData().Trigger(this);
        }

        #if KD_UNIVERSAL_TIME
        public void Dispatch(float delay) {
            GetEventData().Trigger(this, delay);
        }
        #endif

        protected abstract BaseEvent<T> GetEventData();

    }
}