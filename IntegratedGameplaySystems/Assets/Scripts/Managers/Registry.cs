using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
public interface IPoolable
{
    //zet hier iets neer
    public void Recycle(Vector3 Direction);
}

public class Registry
{
    public readonly static Dictionary<string, object> ObjectRegistry = new Dictionary<string, object>();

    public Registry()
    {

    }

    public static void AddToRegistry(string e, ActorBase a)
    {
        if (!ObjectRegistry.ContainsKey(e))
        {
            ObjectRegistry.Add(e, a);
        }
    }
    public static void AddToRegistry(string e, IWeapon a)
    {
        if (!ObjectRegistry.ContainsKey(e))
        {
            ObjectRegistry.Add(e, a);
        }
    }

    public static void AddToRegistry(string e, Enemy a)
    {
        if (!ObjectRegistry.ContainsKey(e))
        {
            ObjectRegistry.Add(e, a);
        }
    }
}
