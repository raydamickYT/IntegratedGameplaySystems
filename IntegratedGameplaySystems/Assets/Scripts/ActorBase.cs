using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActorBase
{
    public GameObject GameObject {get; private set;}

    public ActorBase(GameObject Prefab){
        this.GameObject = Prefab;
    }

}
