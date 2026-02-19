using System;
using System.Collections.Generic;
using UnityEngine;

public static class SystemHub
{
    private static Dictionary<Type, MonoBehaviour> systems = new();

    public static void Register(MonoBehaviour system)
    {
        Type type = system.GetType();

        if (!systems.ContainsKey(type))
        {
            systems.Add(type, system);
        }
    }

    public static T Get<T>() where T : MonoBehaviour
    {
        if (systems.TryGetValue(typeof(T), out MonoBehaviour system))
            return system as T;

        Debug.LogError($"System {typeof(T).Name} not found.");
        return null;
    }
}
