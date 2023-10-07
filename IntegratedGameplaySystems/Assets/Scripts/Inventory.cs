using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory
{
    public GameManager manager;
    EquipmentManager equipmentManager;
    InstantiateGameObjects instantiateGameObjects;

    public Inventory(GameManager _manager, InstantiateGameObjects _instantiateGameObjects)
    {
        manager = _manager;
        instantiateGameObjects = _instantiateGameObjects;
        //Debug.Log(Resources.LoadAll("Weapons", typeof(WeaponData)).Length);
        InitVariables();
    }

    private void InitVariables()
    {
        // for (int i = 0; i < manager.Weapons.Length; i++)
        // {
        //     if (manager.Weapons[i].ItemPrefab.activeInHierarchy)
        //     {
        //         break;
        //     }
        //     else
        //     {
        //         //niet netjes, maar kan nu omdat we weinig wapens hebben.
        //         if (manager.Weapons[i].itemName == WeaponType.Pistol)
        //         {
        //             var t = new Pistol(manager.Weapons[i], instantiateGameObjects);
        //         }
        //         if (manager.Weapons[i].itemName == WeaponType.AssaultRifle)
        //         {
        //             var t = new Pistol(manager.Weapons[i], instantiateGameObjects);
        //         }
        //         if (manager.Weapons[i].itemName == WeaponType.Knife)
        //         {
        //             var t = new Pistol(manager.Weapons[i], instantiateGameObjects);
        //         }
        //         // var item = manager.Weapons[i].ItemPrefab;
        //         var item = GameObject.Instantiate(manager.Weapons[i].ItemPrefab, manager.playerData.GunHolder.transform.position, Quaternion.identity);
        //         item.transform.SetParent(manager.playerData.GunHolder.transform);
        //         item.transform.localRotation = Quaternion.Euler(0, 90, 0);
        //         equipmentManager.WeaponsInScene[i] = item;
        //         if (i == 0)
        //         {
        //             CurrentlyEquippedWeapon = item;
        //             //Debug.Log(CurrentlyEquippedWeapon);
        //             item.SetActive(true);
        //         }
        //         else
        //         {
        //             item.SetActive(false);
        //         }
        //     }
        // }
        // manager.Weapons[0].ItemPrefab.SetActive(true);
    }

    public void AddItem(WeaponData newItem)
    {
        int ItemIndex = (int)newItem.weaponsStyle;
        // 0 is primary, 1 secondary, 2 is melee (for now)
        if (manager.Weapons[ItemIndex] != null)
        {
            Debug.LogWarning("slot is occupied");
        }
        manager.Weapons[ItemIndex] = newItem;
    }

    public WeaponData GetItem(int index)
    {
        return manager.Weapons[index];
    }


}
