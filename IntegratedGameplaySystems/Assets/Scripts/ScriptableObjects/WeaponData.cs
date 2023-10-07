using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Guns/Weapon")]
public class WeaponData : ItemData
{
    
    public float ToFloat(FireRate rate)
    {
        switch (rate)
        {
            case FireRate.Slow:
                return 0.5f;
            case FireRate.Medium:
                return 1.0f;
            case FireRate.Fast:
                return 2.0f;
            default:
                throw new System.Exception("No value for rate: " + rate.ToString());
        }
    }

    public WeaponType itemName;
    public WeaponStyle weaponsStyle;
}
public enum WeaponType { Pistol, Knife, AssaultRifle }
public enum WeaponStyle { Primary, Secondary, Melee }

public enum FireRate { Slow, Medium, Fast }