using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class IWeapon
{
    public WeaponData WeaponScriptableObject { get; set; }
    public GameObject WeaponInScene { get; set; }
    public void Initialization(ScriptableObject assign){}

    public IWeapon(WeaponData script, GameObject Prefab){
        this.WeaponScriptableObject = script;
        this.WeaponInScene = Prefab;
    }
}
