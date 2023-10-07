using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Guns/Weapon")]
public class WeaponData : ItemData
{

    public float FireRateToFloat(FireRate rate)
    {
        switch (rate)
        {
            case FireRate.Slow:
                return 0.1f;
            case FireRate.Medium:
                return .3f;
            case FireRate.Fast:
                return .6f;
            default:
                throw new System.Exception("No value for rate: " + rate.ToString());
        }
    }
    public float BulletLifeToFloat(BulletLife rate)
    {
        switch (rate)
        {
            case BulletLife.Short:
                return 0.2f;
            case BulletLife.Medium:
                return .5f;
            case BulletLife.Long:
                return .1f;
            case BulletLife.Extended:
                return 2.0f;
            default:
                throw new System.Exception("No value for rate: " + rate.ToString());
        }
    }
    public float BulletForceToFloat(WeaponType rate)
    {
        switch (rate)
        {
            case WeaponType.Pistol:
                return 100f;
            case WeaponType.Knife:
                return 100f;
            case WeaponType.AssaultRifle:
                return 200f;
            default:
                throw new System.Exception("No value for rate: " + rate.ToString());
        }
    }

    public WeaponType itemName;
    public WeaponStyle weaponsStyle;
    public FireRate fireRate;
    public BulletLife bulletLife;
}
public enum WeaponType { Pistol, Knife, AssaultRifle }
public enum WeaponStyle { Primary, Secondary, Melee }
public enum FireRate { Slow, Medium, Fast }
public enum BulletLife { Short, Medium, Long, Extended }