public class Singleton<T> : IGameManger where T : new()
{
    private static T _instance;

    public static T Instance => _instance ??= new T();


    public virtual void PreInit() { }
    public virtual void Init() { }
    public virtual void Release() { }

    public virtual void OnUpdate(float dt) { }
}