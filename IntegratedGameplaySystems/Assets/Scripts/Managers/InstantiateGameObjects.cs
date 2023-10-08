using UnityEngine;

public class InstantiateGameObjects : State
{
    private static ObjectPool objectPool;

    protected FSM<GameManager> owner;
    private GameManager manager;
    public int AmountToPool = 60;
    public LayerMask targetLayer;

    public InstantiateGameObjects(FSM<GameManager> _owner, ObjectPool _objectPool, GameManager _manager)
    {
        owner = _owner;
        objectPool = _objectPool;
        manager = _manager;
    }

    public override void OnEnter()
    {
        for (int i = 0; i < AmountToPool; i++)
        {
            //int i staat erbij omdat anders alle bullets dezelfde naam hebben in de registry
            //Gives an error, null reference.
            new BulletActor(manager.bullets.BulletObject, manager, i, objectPool);
        }
        for (int i = 0; i < manager.Weapons.Length; i++)
        {
            if (manager.Weapons[i].ItemPrefab.activeInHierarchy)
            {
                break;
            }
            else
            {
                //niet netjes, maar kan nu omdat we weinig wapens hebben.
                //Zou een Switch case niet werken? uh ja wel dus lmao
                switch (manager.Weapons[i].itemName)
                {
                    case WeaponType.Pistol:
                        {
                            new Pistol(manager.Weapons[i], manager, manager.Weapons[i].ItemPrefab);
                            break;
                        }
                    case WeaponType.AssaultRifle:
                        {
                            new AssaultRifle(manager.Weapons[i], manager, manager.Weapons[i].ItemPrefab);
                            break;
                        }
                    case WeaponType.Knife:
                        {
                            new Pistol(manager.Weapons[i], manager, manager.Weapons[i].ItemPrefab);
                            break;
                        }
                }
            }
        }
        targetLayer = manager.enemyData.enemyLayerMask;

        Collider[] colliders = Physics.OverlapSphere(Vector3.zero, float.MaxValue, targetLayer);
        // Filter these colliders based on a specific type.
        GameObject[] targetObjects = new GameObject[colliders.Length];
        int index = 0;

        foreach (var collider in colliders)
        {
            GameObject targetType = collider.gameObject;
            if (targetType != null)
            {
                targetObjects[index] = targetType;
                new Enemy(manager.enemyData, targetType.transform);
                targetType.SetActive(false);
                index++;
            }
        }
        //als alle objecten die uit de collider list komen null zijn dan wordt de targetobjects list ook null.
        //System.Array.Resize(ref targetObjects, index);
    }

    public static GameObject Instantiate(string e)
    {
        if (Registry.ObjectRegistry[e] is ActorBase actorBase)
        {
            var AnObject = GameObject.Instantiate(actorBase.ActorObject);
            //deze check kan omdat hij nu niet een dictionary door hoeft te zoeken dus het is niet zo zwaar.
            //You're still using the StartsWith "Bullet" though.
            if (e.StartsWith("Bullet"))
            {
                AnObject.transform.SetParent(GameObject.Find("BulletHolder").transform);
                objectPool.DeActivate(actorBase);
            }
            return AnObject;
        }
        if (Registry.ObjectRegistry[e] is IWeapon weapon)
        {
            var AnObject = GameObject.Instantiate(weapon.WeaponInScene);
            //deze check kan omdat hij nu niet een dictionary door hoeft te zoeken dus het is niet zo zwaar.
            return AnObject;
        }
        return null;
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