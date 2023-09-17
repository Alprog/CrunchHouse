

namespace Crunch
{
    public class Singleton<T> where T : class
    {
        public readonly static T Instance;
        
        static Singleton()
        {
            Instance = typeof(T).InvokeDefaultConstructor() as T;
        }

        public static T Ensure()
        {            
            return Instance;
        }
    }
}