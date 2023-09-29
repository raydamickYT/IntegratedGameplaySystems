using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBullet", menuName = "Bullet")]
public class Bullets : ScriptableObject
{
    public GameObject BulletObject;
    public float BulletSpeed = 100f, FireRate = .5f, BulletLife = 1, Damage = 0;
}
