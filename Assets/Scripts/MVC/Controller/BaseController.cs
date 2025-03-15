

using System;
using System.Collections.Generic;

public class BaseController 
{
    private readonly Dictionary<string, Action<object[]>> _eventDictionary = new();
    protected BaseModel _model;

    //initialize controller and initialize model in this method,but sometime model is dynamic, so I don't have to set model in this method
    //The external interface requires only a call to InitController,
    //and subclasses are then responsible for the specific implementation details within this method;
    internal virtual void InitController() 
    {
        InitRelatedViews();
        InitControllerEvent();
        InitGlobalControllerEvent();
    }
    internal virtual void DestroyController()
    {
        RemoveControllerEvent();
        RemoveGlobalControllerEvent();
        DestroyRelatedViews();
    }
    public void RegisterControllerEvent(string eventName, Action<object[]> action)
    {
        if(!_eventDictionary.TryAdd(eventName, action))
        {
            _eventDictionary[eventName]+= action;
        }
    }

    public void UnregisterControllerEvent(string eventName)
    {
        if (_eventDictionary.ContainsKey(eventName))
        {
            _eventDictionary.Remove(eventName);
        }
    }

    public void UnregisterControllerEvent(string eventName, Action<object[]> action) 
    { 
        if(_eventDictionary.TryGetValue(eventName, out var existingAction))
        {
            existingAction -= action;
            if(existingAction == null)
            {
                _eventDictionary.Remove(eventName);
            }
            else
            {
                _eventDictionary[eventName] = existingAction;
            }
        }
    }

    //call the callback functions belong to this controller
    public void TriggerControllerEvent(string eventName, params object[] args) 
    {
        //if (_eventDictionary.ContainsKey(eventName))
        //{
        //    _eventDictionary[eventName].Invoke(args);
        //}
        if (_eventDictionary.TryGetValue(eventName, out var action)) 
        { 
            action(args);
        }
    }

    //call the callback functions belong to other controller
    public void TriggerOtherControllerEvent(ControllerType type, string eventName, params object[] args)
    {
        GameManager.controllerManager.TriggerControllerEvent(type, eventName, args);
    }

    public void SetModel(BaseModel baseModel)
    {
        if (_model != null)
        {
            throw new InvalidOperationException($"{GetType().Name} already is bound");
        }
        _model = baseModel;
        _model.controller = this;
    }
    public BaseModel GetModel()
    {
        return _model;
    }

    protected virtual void InitRelatedViews() { }
    protected virtual void DestroyRelatedViews() { }
    //via the method to register event in the child class and bind the specific event method
    protected virtual void InitControllerEvent() { }
    //this is to register event into the event center manager as a global event
    protected virtual void InitGlobalControllerEvent() { }
    protected virtual void RemoveControllerEvent() { }
    protected virtual void RemoveGlobalControllerEvent() { }
}
