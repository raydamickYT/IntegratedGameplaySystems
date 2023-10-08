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
    public EnemyData DamageAbleScriptableObject = null;
    public IDamageableActor(EnemyData gameObject)
    {
        DamageAbleScriptableObject = gameObject;
        //Initialize();
    }
    public void Initialize()
    {

    }
}
