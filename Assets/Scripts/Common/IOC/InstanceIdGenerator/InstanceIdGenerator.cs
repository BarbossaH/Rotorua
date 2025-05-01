namespace Common
{


    using System.Collections.Generic;


    public enum IdCategoryEnum
    {
        Card = 10000,
        Item = 20000
    }

    public interface IInstanceIdService
    {
        int Next(IdCategoryEnum category);
        void Reset(IdCategoryEnum category);
        void ResetAll();
    }


    [AutoRegisterIDGenerator(typeof(IInstanceIdService))]
    public class InstanceIdGenerator : IInstanceIdService
    {
        private readonly Dictionary<IdCategoryEnum, int> _idCounters = new();
        public int Next(IdCategoryEnum category)
        {
            if (!_idCounters.TryGetValue(category, out int current))
            {
                current = (int)category;
            }
            int nextId = current;
            _idCounters[category] = current + 1;
            return nextId;
        }

        public void Reset(IdCategoryEnum category)
        {
            _idCounters[category] = (int)category;
        }

        public void ResetAll()
        {
            _idCounters.Clear();
        }
    }
}