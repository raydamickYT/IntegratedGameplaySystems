using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : IDamageableActor
{
    public EnemyData enemyScriptableObject;
    public Transform SpawnPoint;
    public Enemy(EnemyData scriptableObject, Transform _spawnPoint, int i, GameManager _manager) : base(scriptableObject, _manager)
    {
        enemyScriptableObject = scriptableObject;
        Registry.AddToRegistry(enemyScriptableObject.EnemObject.name + i,this);
        SpawnPoint = _spawnPoint;
        SpawnEnemies();
    }
    public void SpawnEnemies()
    {

        DamageAbleObject = GameObject.Instantiate(enemyScriptableObject.EnemObject);
        DamageAbleObject.transform.position = SpawnPoint.position;
    }
}
