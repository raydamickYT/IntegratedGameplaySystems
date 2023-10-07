using UnityEngine;

public class InstantiateGameObjects : State
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
        //manager.PrefabLibrary.Add("player", manager.Prefab);

        for (int i = 0; i < manager.AmountToPool; i++)
        {
            GameObject instantiatedObject = GameObject.Instantiate(manager.Prefab);
            instantiatedObject.SetActive(false);

            objectPool.AddObjectToPool(instantiatedObject);
            //owner.pOwner.PrefabLibrary.Add("Bullet" + i.ToString(), owner.pOwner.bullets.BulletObject);
        }
        //manager.PrefabLibrary.Add("enemy", manager.PreFab);

        foreach (var kvp in manager.PrefabLibrary)
        {
            Vector3 startPos = Vector3.zero;

            if (kvp.Key == "player")
            {
                startPos = new Vector3(0, -4.4f, 0);
            }
            if (kvp.Key.StartsWith("Bullet"))
            {
                GameObject test = manager.InstantiatedObjects["player"];
                startPos = test.transform.position;
            }

            GameObject instantiatedObject = GameObject.Instantiate(kvp.Value, startPos, Quaternion.identity);
            instantiatedObject.name = kvp.Key;

            if (kvp.Key.StartsWith("Bullet"))
            {
                instantiatedObject.SetActive(false);
                objectPool.AddObjectToPool(instantiatedObject);
            }

            //hier de instantiated object toevoegden aan de library
            manager.InstantiatedObjects.Add(kvp.Key, instantiatedObject);
        }

        owner.SwitchState(typeof(IdleState));
    }
}

public class IdleState : State
{
    public FSM<GameManager> owner;

    public IdleState(FSM<GameManager> _owner)
    {
        owner = _owner;
    }
}
