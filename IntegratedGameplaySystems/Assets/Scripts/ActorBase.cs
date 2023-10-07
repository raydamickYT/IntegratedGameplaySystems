using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActorBase
{
    public GameObject ActorObject { get; private set; }
    public GameObject ActiveObjectInScene { get; set; }

    public ActorBase(GameObject Prefab)
    {
        this.ActorObject = Prefab;
    }

}
