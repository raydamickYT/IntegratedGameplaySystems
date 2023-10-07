using UnityEngine;

public class InstantiateGameObjects : State<GameManager>
{
    private readonly ObjectPool objectPool;
    protected FSM<GameManager> owner;
    private GameManager manager;

    public InstantiateGameObjects(FSM<GameManager> _owner, ObjectPool _objectPool, GameManager _manager)
    {
        owner = _owner;
        objectPool = _objectPool;
        manager = _manager;
    }

    public override void OnEnter()
    {
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
    public static GameObject TestInstantiate(string e, ref Dictionary<string, IPoolable> gameObjects, IPoolable actor)
    {
        var EenObject = GameObject.Instantiate(Registry.ObjectRegistry[e].GameObject);
        gameObjects.Add(e, actor);
        return EenObject;
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
