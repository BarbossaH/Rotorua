

using System;
using System.Collections.Generic;

public class BaseController 
{
    private readonly Dictionary<string, Action<object[]>> _eventDictonary = new();
    protected BaseModel _model;

    //initialize controller and initialize model in this method,but sometime model is dynamic, so I don't have to set model in this method
    //The external interface requires only a call to InitController,
    //and subclasses are then responsibe for the specific implementation details within this method;
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

    public void UnregisterControllerEvent(string eventName, Action<object[]> action) 
    { 
        if(_eventDictonary.TryGetValue(eventName, out var existingAction))
        {
            existingAction -= action;
            if(existingAction == null)
            {
                _eventDictonary.Remove(eventName);
            }
            else
            {
                _eventDictonary[eventName] = existingAction;
            }
        }
    }

    //call the callback functions belong to this contoller
    public void TriggerControllerEvent(string eventName, params object[] args) 
    {
        //if (_eventDictonary.ContainsKey(eventName))
        //{
        //    _eventDictonary[eventName].Invoke(args);
        //}
        if (_eventDictonary.TryGetValue(eventName, out var action)) 
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

    public virtual void InitRelatedViews() { }
    public virtual void DestroyRelatedViews() { }
    //via the method to register event in the child class and bind the specific event method
    public virtual void InitControllerEvent() { }
    //this is to register event into the event center manager as a global event
    public virtual void InitGlobalControllerEvent() { }
    public virtual void RemoveControllerEvent() { }
    public virtual void RemoveGlobalControllerEvent() { }
}
