namespace GameDevTV.Utils
{
    /// <summary>
    /// Container class that wraps a value and ensures initialisation is 
    /// called just before first use.
    /// </summary>
    public class LazyValue<T>
    {
        private T _value;
        private bool _initialized = false;
        
        public delegate T InitializerDelegate();
        private InitializerDelegate _initializer;

        public LazyValue(InitializerDelegate initializer)
        {
            _initializer = initializer;
        }

        public T value
        {
            get
            {
                // Ensure we init before returning a value.
                ForceInit();
                return _value;
            }
            set
            {
                // Don't use default init anymore.
                _initialized = true;
                _value = value;
            }
        }

        public void ForceInit()
        {
            if (!_initialized)
            {
                _value = _initializer();
                _initialized = true;
            }
        }
    }
}