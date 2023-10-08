using UnityEngine;
using System;
using System.Collections.Generic;

public class InstantiateGameObjects : State
{
    private static ObjectPool objectPool;

    protected FSM<GameManager> owner;
    private GameManager manager;
    public int AmountToPool = 30;

    public InstantiateGameObjects(FSM<GameManager> _owner, ObjectPool _objectPool, GameManager _manager)
    {
        owner = _owner;
        objectPool = _objectPool;
        manager = _manager;
    }

    //Gives Errors
    //public override void OnEnter()
    //{
    //    for (int i = 0; i < AmountToPool; i++)
    //    {
    ////        int i staat erbij omdat anders alle bullets dezelfde naam hebben in de registry
    //        new BulletActor(manager.bullets.BulletObject, manager, i, objectPool);
    //    }
    //    for (int i = 0; i < manager.Weapons.Length; i++)
    //    {
    //        if (manager.Weapons[i].ItemPrefab.activeInHierarchy)
    //        {
    //            break;
    //        }
    //        else
    //        {
    ////            niet netjes, maar kan nu omdat we weinig wapens hebben.
    //            if (manager.Weapons[i].itemName == WeaponType.Pistol)
    //            {
    //                var t = new Pistol(manager.Weapons[i], manager, manager.Weapons[i].ItemPrefab);
    //            }
    //            if (manager.Weapons[i].itemName == WeaponType.AssaultRifle)
    //            {
    //                var t = new AssaultRifle(manager.Weapons[i], manager, manager.Weapons[i].ItemPrefab);
    //            }
    //            if (manager.Weapons[i].itemName == WeaponType.Knife)
    //            {
    //                var t = new Pistol(manager.Weapons[i], this, manager.Weapons[i].ItemPrefab);
    //            }
    //        }
    //    }
    //}

    public static GameObject Instantiate(string e)
    {
        if (Registry.ObjectRegistry[e] is ActorBase actorBase)
        {
            var EenObject = GameObject.Instantiate(actorBase.ActorObject);
            //deze check kan omdat hij nu niet een dictionary door hoeft te zoeken dus het is niet zo zwaar.
            //You're still using the StartsWith "Bullet" though.
            if (e.StartsWith("Bullet"))
            {
                objectPool.DeActivate(actorBase);
            }
            return EenObject;
        }
        if (Registry.ObjectRegistry[e] is IWeapon weapon)
        {
            var EenObject = GameObject.Instantiate(weapon.WeaponInScene);
            //deze check kan omdat hij nu niet een dictionary door hoeft te zoeken dus het is niet zo zwaar.
            return EenObject;
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