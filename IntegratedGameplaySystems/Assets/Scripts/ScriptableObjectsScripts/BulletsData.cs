using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBullet", menuName = "ScriptableObjects/Bullet")]
public class BulletsData : ScriptableObject
{
    public GameObject BulletObject;
    public LayerMask EnemyLayerMask;
    public float BulletSpeed = 100f, FireRate = .5f, BulletLife = 1, Damage = 0;
}
