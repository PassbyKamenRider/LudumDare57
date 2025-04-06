using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine;

public enum GlobalEvent
{
    AnyTileChangedByPlayer,
    AnyTileChangedByLevel
}

public class EventManager : Singleton<EventManager>
{
    public Dictionary<GlobalEvent, IEventInfo> globalEvents_0arg;
    public Dictionary<GlobalEvent, IEventInfo> globalEvents_1arg;
    public Dictionary<GlobalEvent, IEventInfo> globalEvents_2arg;


    // Adding listener for events with no arguments
    public void AddListener(GlobalEvent globalEvent, UnityAction action)
    {
        if (globalEvents_0arg == null) { globalEvents_0arg = new Dictionary<GlobalEvent, IEventInfo>(); }
        if (!globalEvents_0arg.ContainsKey(globalEvent))
        {
            globalEvents_0arg[globalEvent] = new EventInfo();
        }
        (globalEvents_0arg[globalEvent] as EventInfo).AddListener(action);
    }

    // Adding listener for events with one argument
    public void AddListener<T>(GlobalEvent globalEvent, UnityAction<T> action)
    {
        if (globalEvents_1arg == null) { globalEvents_1arg = new Dictionary<GlobalEvent, IEventInfo>(); }
        if (!globalEvents_1arg.ContainsKey(globalEvent))
        {
            globalEvents_1arg[globalEvent] = new EventInfo<T>();
        }
        (globalEvents_1arg[globalEvent] as EventInfo<T>).AddListener(action);
    }

    // Adding listener for events with two arguments
    public void AddListener<T0, T1>(GlobalEvent globalEvent, UnityAction<T0, T1> action)
    {
        if (globalEvents_2arg == null) { globalEvents_2arg = new Dictionary<GlobalEvent, IEventInfo>(); }
        if (!globalEvents_2arg.ContainsKey(globalEvent))
        {
            globalEvents_2arg[globalEvent] = new EventInfo<T0, T1>();
        }
        (globalEvents_2arg[globalEvent] as EventInfo<T0, T1>).AddListener(action);
    }

    // Removing listener for events with no arguments
    public void RemoveListener(GlobalEvent globalEvent, UnityAction action)
    {
        if (globalEvents_0arg == null || !globalEvents_0arg.ContainsKey(globalEvent)) { return; }
        (globalEvents_0arg[globalEvent] as EventInfo).RemoveListener(action);
    }

    // Removing listener for events with one argument
    public void RemoveListener<T>(GlobalEvent globalEvent, UnityAction<T> action)
    {
        if (globalEvents_1arg == null || !globalEvents_1arg.ContainsKey(globalEvent)) { return; }
        (globalEvents_1arg[globalEvent] as EventInfo<T>).RemoveListener(action);
    }

    // Removing listener for events with two arguments
    public void RemoveListener<T0, T1>(GlobalEvent globalEvent, UnityAction<T0, T1> action)
    {
        if (globalEvents_2arg == null || !globalEvents_2arg.ContainsKey(globalEvent)) { return; }
        (globalEvents_2arg[globalEvent] as EventInfo<T0, T1>).RemoveListener(action);
    }

    // Invoking events with no arguments
    public void Invoke(GlobalEvent globalEvent)
    {
        if (globalEvents_0arg == null || !globalEvents_0arg.ContainsKey(globalEvent)) { return; }
        (globalEvents_0arg[globalEvent] as EventInfo).Invoke();
    }

    // Invoking events with one argument
    public void Invoke<T>(GlobalEvent globalEvent, T arg)
    {

        if (globalEvents_1arg == null || !globalEvents_1arg.ContainsKey(globalEvent)) { return; }
        (globalEvents_1arg[globalEvent] as EventInfo<T>).Invoke(arg);
        if (globalEvents_0arg == null || !globalEvents_0arg.ContainsKey(globalEvent)) { return; }
        (globalEvents_0arg[globalEvent] as EventInfo).Invoke();
    }

    // Invoking events with two arguments
    public void Invoke<T0, T1>(GlobalEvent globalEvent, T0 arg0, T1 arg1)
    {
        if (globalEvents_2arg == null || !globalEvents_2arg.ContainsKey(globalEvent)) { return; }
        (globalEvents_2arg[globalEvent] as EventInfo<T0, T1>).Invoke(arg0, arg1);
        if (globalEvents_0arg == null || !globalEvents_0arg.ContainsKey(globalEvent)) { return; }
        (globalEvents_0arg[globalEvent] as EventInfo).Invoke();
    }

    // Example setup function
    public void SetUpScene(string sceneName)
    {
        
    }

    // Example function to be called when the scene exits
    public void ExitScene()
    {
        // clear all events
        globalEvents_0arg?.Clear();
        globalEvents_1arg?.Clear();
        globalEvents_2arg?.Clear();
    }
}

public interface IEventInfo { }

public class EventInfo : IEventInfo
{
    public UnityEvent unityEvent;

    public EventInfo()
    {
        unityEvent = new UnityEvent();
    }

    public void AddListener(UnityAction action)
    {
        unityEvent.AddListener(action);
    }

    public void RemoveListener(UnityAction action)
    {
        unityEvent.RemoveListener(action);
    }

    public void Invoke()
    {
        unityEvent.Invoke();
    }
}

public class EventInfo<T> : IEventInfo
{
    public UnityEvent<T> unityEvent;

    public EventInfo()
    {
        unityEvent = new UnityEvent<T>();
    }

    public void AddListener(UnityAction<T> action)
    {
        unityEvent.AddListener(action);
    }

    public void RemoveListener(UnityAction<T> action)
    {
        unityEvent.RemoveListener(action);
    }

    public void Invoke(T arg)
    {
        unityEvent.Invoke(arg);
    }
}

public class EventInfo<T0, T1> : IEventInfo
{
    public UnityEvent<T0, T1> unityEvent;

    public EventInfo()
    {
        unityEvent = new UnityEvent<T0, T1>();
    }

    public void AddListener(UnityAction<T0, T1> action)
    {
        unityEvent.AddListener(action);
    }

    public void RemoveListener(UnityAction<T0, T1> action)
    {
        unityEvent.RemoveListener(action);
    }

    public void Invoke(T0 arg0, T1 arg1)
    {
        unityEvent.Invoke(arg0, arg1);
    }
}
