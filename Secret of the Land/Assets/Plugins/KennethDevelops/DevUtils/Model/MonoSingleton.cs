using UnityEngine;

namespace KennethDevelops.DevUtils.Model {
    /// <summary>
    /// Singleton MonoBehaviour abstract class.
    /// </summary>
    /// <typeparam name="T">The type that extends MonoSingleton</typeparam>
    /// <remarks>
    /// For more detailed information, check the full documentation here: https://github.com/kennethdevelops/DevUtilsDocs/wiki/MonoSingleton
    /// </remarks>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T> {
        /// <summary>
        /// Override this property to set whether this singleton should be destroyed when a new scene is loaded.
        /// </summary>
        /// <returns>Default value is true, meaning the object will persist between scenes.</returns>
        protected virtual bool DoNotDestroyOnLoad { get; } = true;

        /// <summary>
        /// This flag is set to true when the application is quitting. Used to avoid creating new instances.
        /// </summary>
        public static bool ApplicationQuitting { get; private set; }

        /// <summary>
        /// Sets the Instance if it was null. Returns whether the Instance was set.
        /// </summary>
        /// <param name="instance">The instance to be set.</param>
        /// <returns>Returns false if Instance was already set, true if it hasn't</returns>
        protected bool SetInstanceIfNull(T instance) {
            if (_instance != null) return false;

            _instance = instance;
            if (DoNotDestroyOnLoad) DontDestroyOnLoad(_instance.gameObject);
            Init();
            return true;
        }

        // Similar to the method above, but does not check if Instance was already set.
        protected void SetInstance(T instance) {
            _instance = instance;
            if (DoNotDestroyOnLoad) DontDestroyOnLoad(_instance.gameObject);
            Init();
        }

        private static T _instance;

        /// <summary>
        /// Gets the instance of the singleton.
        /// </summary>
        /// <value>The instance of the singleton.</value>
        /// <remarks>
        /// For more detailed information, check the full documentation here: [MonoSingleton Instance Doc Link]
        /// </remarks>
        public static T Instance {
            get {
                if (_instance != null) return _instance;
                if (ApplicationQuitting) return null;

                var go = new GameObject(typeof(T).Name);
                _instance = go.AddComponent<T>();
                _instance.Init();

                if (_instance.DoNotDestroyOnLoad) DontDestroyOnLoad(go);

                return _instance;
            }

            protected set { _instance = value; }
        }

        /// <summary>
        /// This method is called when the Singleton instance is created. Override it to add initialization logic.
        /// </summary>
        /// <remarks>
        /// For more detailed information and examples, check the Init Doc here: [Init Doc Link]
        /// </remarks>
        protected abstract void Init();

        /// <summary>
        /// Sets ApplicationQuitting to true when the application is quitting.
        /// </summary>
        protected virtual void OnApplicationQuit() {
            ApplicationQuitting = true;
        }
    }

}