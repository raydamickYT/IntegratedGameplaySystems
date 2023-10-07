using UnityEngine;
using System;
using System.Collections.Generic;

public class InstantiateGameObjects : State<GameManager>
{
    private static ObjectPool objectPool;
    
    protected FSM<GameManager> owner;
    private GameManager manager;
    public int AmountToPool = 30;


    public InstantiateGameObjects(FSM<GameManager> _owner, ObjectPool _objectPool, GameManager _manager)
    {
        owner = _owner;
        objectPool = _objectPool;
        Debug.Log(objectPool);
        manager = _manager;
    }

    public override void OnEnter()
    {
        for (int i = 0; i < AmountToPool; i++)
        {
            // int i staat erbij omdat anders alle bullets dezelfde naam hebben in de registry
            new TestBulletActor(manager.bullets.BulletObject, manager, i, objectPool);
        }
        // //manager.PrefabLibrary.Add("player", manager.Prefab);

        // for (int i = 0; i < manager.AmountToPool; i++)
        // {
        //     GameObject instantiatedObject = GameObject.Instantiate(manager.BulletPrefab);
        //     instantiatedObject.SetActive(false);
        //     Debug.Log("making");
        //     objectPool.AddObjectToPool(instantiatedObject);
        //     manager.PrefabLibrary.Add("Bullet" + i.ToString(), manager.bullets.BulletObject);
        // }

        // foreach (var kvp in manager.PrefabLibrary)
        // {
        //     Vector3 startPos = Vector3.zero;

        //     if (kvp.Key == "player")
        //     {
        //         startPos = new Vector3(0, -4.4f, 0);
        //     }

        //     if (kvp.Key.StartsWith("Bullet"))
        //     {
        //         GameObject test = manager.InstantiatedObjects["player"];
        //         startPos = test.transform.position;
        //     }

        //     GameObject instantiatedObject = GameObject.Instantiate(kvp.Value, startPos, Quaternion.identity);
        //     instantiatedObject.name = kvp.Key;

        //     if (kvp.Key.StartsWith("Bullet"))
        //     {
        //         instantiatedObject.SetActive(false);
        //         objectPool.AddObjectToPool(instantiatedObject);
        //     }

        //     //hier de instantiated object toevoegden aan de library
        //     manager.InstantiatedObjects.Add(kvp.Key, instantiatedObject);
        // }

        // owner.SwitchState(typeof(IdleState));
    }
    public static GameObject TestInstantiate(string e, IPoolable actor)
    {
        var EenObject = GameObject.Instantiate(Registry.ObjectRegistry[e].ActorObject);
        //deze check kan omdat hij nu niet een dictionary door hoeft te zoeken dus het is niet zo zwaar.
        if (e.StartsWith("Bullet"))
        {
            objectPool.DeActivate(Registry.ObjectRegistry[e]);
        }
        return EenObject;
    }
    private void testDeactivate(ActorBase actor)
    {

    }
}

public class IdleState : State<GameManager>
{
    public FSM<GameManager> owner;

    public IdleState(FSM<GameManager> _owner)
    {
        owner = _owner;
    }
}
