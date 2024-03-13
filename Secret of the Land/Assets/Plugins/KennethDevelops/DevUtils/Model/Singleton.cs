namespace KennethDevelops.DevUtils.Model {    /// <summary>
    /// Singleton abstract class that implements the Singleton pattern.
    /// </summary>
    /// <typeparam name="T">The type that extends Singleton</typeparam>
    /// <remarks>
    /// For more detailed information, check the full documentation here: [Singleton Doc Link]
    /// </remarks>
    public abstract class Singleton<T> where T : Singleton<T>, new (){
        
        private static T _instance;

        /// <summary>
        /// Gets the instance of the singleton.
        /// </summary>
        /// <value>The instance of the singleton.</value>
        /// <remarks>
        /// For more detailed information, check the full documentation here: https://github.com/kennethdevelops/DevUtilsDocs/wiki/Singleton
        /// </remarks>
        public static T Instance {
            get {
                if (_instance != null) return _instance;
                
                _instance = new T();
                _instance.Init();

                return _instance;
            }
        }

        /// <summary>
        /// This method is called when the Singleton instance is created. Override it to add initialization logic.
        /// </summary>
        /// <remarks>
        /// For more detailed information and examples, check the Init Doc here: [Init Doc Link]
        /// </remarks>
        protected abstract void Init();
    }
}