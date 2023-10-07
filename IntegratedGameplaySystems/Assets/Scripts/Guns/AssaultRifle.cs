using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssaultRifle : IWeapon
{
    InstantiateGameObjects instantiateGameObjects;
    GameManager gameManager;
    public AssaultRifle(WeaponData assign, GameManager _gameManager, GameObject gameObject) : base(assign, gameObject)
    {
        gameManager = _gameManager;
        WeaponScriptableObject = assign;
        Initialization();
    }

    public void Initialization()
    {
        Registry.AddToRegistry(WeaponInScene.name, this);
        WeaponInScene = InstantiateGameObjects.Instantiate(WeaponScriptableObject.ItemPrefab.name);
        WeaponInScene.transform.SetParent(gameManager.playerData.GunHolder.transform);
        WeaponInScene.transform.localRotation = Quaternion.Euler(0, 90, 0);
        EquipmentManager.WeaponsInScene[(int)WeaponScriptableObject.weaponsStyle] = WeaponInScene;
        if (WeaponScriptableObject.itemName == WeaponType.Pistol)
        {
            EquipmentManager.currentlyEquippedWeapon = WeaponInScene;
            WeaponInScene.SetActive(true);
        }
        else
        {
            WeaponInScene.SetActive(false);

        }
    }
}
