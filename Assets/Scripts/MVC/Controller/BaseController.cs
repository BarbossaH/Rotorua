

using System;
using System.Collections.Generic;

public class BaseController 
{
    private readonly Dictionary<string, Action<object[]>> _eventDictonary = new();
    protected BaseModel _model;


    public void RegisterControllerEvent(string eventName, Action<object[]> action)
    {
        if(!_eventDictonary.TryAdd(eventName, action))
        {
            _eventDictonary[eventName]+= action;
        }
    }

    public void UnregisterControllerEvent(string eventName)
    {
        if (_eventDictonary.ContainsKey(eventName))
        {
            _eventDictonary.Remove(eventName);
        }
    }

    //call the callback functions belong to this contoller
    public void TriggerControllerEvent(string eventName, params object[] args) 
    {
        if (!_eventDictonary.ContainsKey(eventName))
        {
            _eventDictonary[eventName].Invoke(args);
        }
    }

    //call the callback functions belong to other controller
    public void TriggerOtherControllerEvent(ControllerType type, string eventName, params object[] args)
    {
        GameManager.controllerManager.TriggerControllerEvent(type, eventName, args);
    }

    public void SetModel(BaseModel baseModel)
    {
        _model = baseModel;
        _model.controller = this;
    }
    public BaseModel GetModel()
    {
        return _model;
    }

    public virtual void DestroyController() 
    {
        RemoveControllerEvent();
        RemoveGlobalControllerEvent();
    }

    //via the method to register event in the child class and bind the specific event method
    public virtual void InitControllerEvent() { }
    //this is to register event into the event center manager as a global event
    public virtual void InitGlobalControllerEvent() { }
    public virtual void RemoveControllerEvent() { }
    public virtual void RemoveGlobalControllerEvent() { }
}
