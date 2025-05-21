using Game.Character;

namespace Common.Factories
{
    public interface IFilterFactory
    {
        IItentFilter CreateIntentFilter(string filterId);
    }

    public class FilterFactory : IFilterFactory
    {
        public IItentFilter CreateIntentFilter(string filterId)
        {
            throw new System.NotImplementedException();
        }
    }
}