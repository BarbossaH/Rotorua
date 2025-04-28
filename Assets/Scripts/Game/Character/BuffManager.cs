using System;
using System.Collections.Generic;
using Game.SkillSystem;

namespace Game.Character
{
    public class BuffManager
    {
        private readonly List<SkillBuffBase> _skillBuffBases = new();

        public void AddBuff(SkillBuffBase buff)
        {
            _skillBuffBases.Add(buff);
        }

        public void RemoveBuff(SkillBuffBase buff)
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