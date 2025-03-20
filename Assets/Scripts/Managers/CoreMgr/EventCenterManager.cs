using System;
using System.Collections.Generic;

public class EventCenterManager : Singleton<EventCenterManager>
{
    private readonly Dictionary<string, Action<object[]>> _eventDic = new();

    public void RegisterEvent(string eventName, Action<object[]> callback)
    {
        if (!_eventDic.ContainsKey(eventName))
        {
            _eventDic[eventName] = delegate { };
        }
        _eventDic[eventName] += callback;
    }

    public void UnregisterEvent(string eventName, Action<object[]> callback)
    {
        if (_eventDic.ContainsKey(eventName))
        {
            _eventDic[eventName]-=callback;
            if (_eventDic[eventName] == null)
            {
                _eventDic.Remove(eventName);
            }
        }
    }

    public void PublishEvent(string eventName, params object[] args) { 
        if(_eventDic.TryGetValue(eventName, out Action<object[]> callback))
        {
            //callback(args);
            callback.Invoke(args);
        }
    }
}