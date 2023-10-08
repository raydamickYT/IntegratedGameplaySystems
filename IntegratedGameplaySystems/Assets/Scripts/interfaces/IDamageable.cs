using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IDamageable
{
    void Initialize();
}
public class IDamageableActor : IDamageable
{
    public LayerMask targetLayer;
    public GameObject DamageAbleObject;
    GameManager manager;
    public EnemyData DamageAbleScriptableObject = null;
    public IDamageableActor(EnemyData gameObject, GameManager _manager)
    {
        manager = _manager;
        DamageAbleScriptableObject = gameObject;
        manager.OnUpdate += DamageAbleUpdate;
        //Initialize();
    }
    public void Initialize()
    {

    }
    public void DamageAbleUpdate()
    {
    }
}
