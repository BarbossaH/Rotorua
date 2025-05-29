using System;
using System.Collections.Generic;
using Game.CardSystem;

namespace Game.Character
{
    public class BuffManager
    {
        private readonly List<BuffBase> _skillBuffBases = new();

        public void AddBuff(BuffBase buff)
        {
            _skillBuffBases.Add(buff);
        }

        public void RemoveBuff(BuffBase buff)
        {
            _skillBuffBases.Remove(buff);
        }

        public void ExecuteBuffs()
        {
            foreach (var b in _skillBuffBases)
            {
                // check buff 是什么类型，该干什么事
            }
        }
    }
}