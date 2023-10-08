using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewEnemy", menuName = "ScriptableObjects/Enemy")]
public class EnemyData : ScriptableObject
{
    public GameObject EnemObject;
    public LayerMask enemyLayerMask;
}
