using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : IDamageableActor
{
    public EnemyData enemyScriptableObject;
    public Transform SpawnPoint;
    public Enemy(GameObject gameobject, EnemyData scriptableObject, Transform _spawnPoint, int i, GameManager _manager) : base(scriptableObject, _manager)
    {
        enemyScriptableObject = scriptableObject;
        DamageAbleObject = gameobject;
        Registry.AddToRegistry(gameobject.name, this);
        Debug.Log(gameobject.name + i);
        SpawnPoint = _spawnPoint;
        //SpawnEnemies();
    }
    public void SpawnEnemies()
    {

        DamageAbleObject = GameObject.Instantiate(enemyScriptableObject.EnemObject);
        DamageAbleObject.transform.position = SpawnPoint.position;
    }
}
