using System;
using System.Collections.Generic;

public static class PubSub
{
    private static Dictionary<Type, List<Action<object>>> listeners = new Dictionary<Type, List<Action<object>>>();

    public static void AddListener<T>(Action<object> listener) where T : class
    {
        //Check for existing list
        if (!listeners.ContainsKey(typeof(T)))
            listeners.Add(typeof(T), new List<Action<object>>());
        //Add
        listeners[typeof(T)].Add(listener);
    }

    public static void RemoveListener<T>(Action<object> listener) where T : class
    {
        if (listeners.ContainsKey(typeof(T)))
        {
            listeners[typeof(T)].Remove(listener);
        }
    }

    public static void Publish<T>(T publishedEvent) where T : class
    {
        if (listeners.ContainsKey(typeof(T)))
        {
            foreach(var action in listeners[typeof(T)])
            {
                action.Invoke(publishedEvent);
            }
        }
    }
}
