namespace Common
{
    public interface ISingleton
    {
        void PreInit();
        void Init();
        void Release();
    }
}