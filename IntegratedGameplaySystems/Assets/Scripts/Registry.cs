using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
public interface IPoolable
{
    //zet hier iets neer
    void Recycle();
}

public class Registry
{
    public readonly static Dictionary<string, ActorBase> ObjectRegistry = new Dictionary<string, ActorBase>();

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
}
