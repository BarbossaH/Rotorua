using Game.Character.Monster;

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
            var picker = GameTypeCache.CreateInstanceByClassName<IIntentPicker>(className, true);

            return picker;
        }
    }
}