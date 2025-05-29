using System;
using System.Collections.Generic;
using Game.CardSystem;

namespace Game.Character
{
    public class BuffManager
    {
        //TODO:这里的buff要区分来源，一个是源自于圣物，永久存在，而一个是来源于card，打了牌才存在
        private readonly List<BuffBase> _buffsFromCard = new();
        private readonly List<BuffBase> _buffsFromRelic = new();

        public void AddBuffFromCard(BuffBase buff)
        {
            _buffsFromCard.Add(buff);
        }

        public void RemoveBuffFromCard(BuffBase buff)
        {
            _buffsFromCard.Remove(buff);
        }

        public void AddBuffFromRelic(BuffBase buff)
        {
            _buffsFromRelic.Add(buff);
        }
        public void RemoveBuffFromRelic(BuffBase buff)
        {
            _buffsFromRelic.Remove(buff);
        }
        public void ExecuteBuffs()
        {
            foreach (var b in _buffsFromCard)
            {
                // check buff 是什么类型，该干什么事
            }
        }
    }
}