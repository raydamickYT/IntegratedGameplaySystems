using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestBulletActor : ActorBase, IPoolable
{
   private GameManager manager;
    private ObjectPool objectPool;

    //deze base assigned gelijk de prefab aan de actorbase
    public TestBulletActor(GameObject Prefab, GameManager _manager, int NameInt, ObjectPool _objectPool) : base(Prefab)
    {
        this.objectPool = _objectPool;
        this.manager = _manager;
        //dit voegt dit script toe niet de gameobject. die zit vast aan dit script.
        Registry.AddToRegistry(Prefab.name + NameInt, this);
        ActiveObjectInScene = InstantiateGameObjects.TestInstantiate(Prefab.name + NameInt, this);

       // objectPool.DeActivate(this);
    }

    public void Recycle()
    {
        //wat te doen als dit object deactiveerd.
        objectPool.DeActivate(this);
    }
}
