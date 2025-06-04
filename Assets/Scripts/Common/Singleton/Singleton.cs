
namespace Common
{
    public class Singleton<T> : ISingleton where T : new()
    {
        // private static T _instance;
        // public static T Instance => _instance ??= new T();

        private static T _instance;
        static Singleton()
        {
            _instance = new T();
        }
        public static T Instance => _instance;

        //the purpose of the Init method is to create a singleton instance and if needed, to initialize certain variables as required
        public virtual void Init() { }
        public virtual void Release() { }

        public virtual void OnUpdate(float dt) { }
    }
}