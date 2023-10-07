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
        ActiveObjectInScene = InstantiateGameObjects.Instantiate(Prefab.name + NameInt);

        // objectPool.DeActivate(this);
    }

    public void Recycle(Vector3 direction)
    {
        //wat te doen als dit object deactiveerd.
        ActiveObjectInScene.transform.position = manager.playerData.GunHolder.transform.position;
        ActiveObjectInScene.transform.rotation = manager.playerData.GunHolder.transform.rotation;
        ActiveObjectInScene.transform.forward = direction.normalized;
    }
}
