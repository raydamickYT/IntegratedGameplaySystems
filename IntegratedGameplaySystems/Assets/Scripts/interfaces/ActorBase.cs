using System;
using UnityEngine;

public class ActorBase
{
    public Action OnUpdateEvent;
    public Action OnFixedUpdateEvent;
    public Action OnDisableEvent;

    public Action NoLongerMoving;
    public Action StartedMoving;

    public GameObject ActorObject { get; private set; }
    public GameObject ActiveObjectInScene { get; set; }

    public ActorBase(GameObject Prefab)
    {
        this.ActorObject = Prefab;
    }
}
