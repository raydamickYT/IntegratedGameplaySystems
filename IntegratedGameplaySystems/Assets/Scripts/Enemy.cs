using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : IDamageableActor
{
    public EnemyData enemyScriptableObject;
    public GameObject enemyObject;
    public Transform SpawnPoint;
    public Enemy(EnemyData scriptableObject, Transform _spawnPoint) : base(scriptableObject)
    {
        enemyScriptableObject = scriptableObject;
        SpawnPoint = _spawnPoint;
        SpawnEnemies();
    }
    public void SpawnEnemies()
    {
        Registry.AddToRegistry(enemyScriptableObject.EnemObject.name ,this);

        enemyObject = GameObject.Instantiate(enemyScriptableObject.EnemObject);
        enemyObject.transform.position = SpawnPoint.position;
    }
}
