public class Singleton<T> where T : new()
{
    private static T _instance;

    public static T Instance => _instance ??= new T();

    public virtual void Init() { }
    public virtual void OnDestroy() { }
    public virtual void OnUpdate(float dt) { }
}