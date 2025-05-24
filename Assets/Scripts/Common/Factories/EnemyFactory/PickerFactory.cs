using Game.Character.Enemy;

namespace Common.Factories
{
    public interface IPickerFactory
    {
        IIntentPicker CreatePicker(string className);
    }

    [AutoRegisterFactory(typeof(IPickerFactory))]
    public class PickerFactory : IPickerFactory
    {
        public IIntentPicker CreatePicker(string className)
        {
            throw new System.NotImplementedException();
        }
    }
}