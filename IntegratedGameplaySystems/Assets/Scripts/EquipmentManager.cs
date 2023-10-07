using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EquipmentManager : ICommand
{
    public GameManager gameManager;
    public Inventory inventory;
    public static GameObject currentlyEquippedWeapon = null;
    public static GameObject[] WeaponsInScene;
    public EquipmentManager(GameManager manager, Inventory _inventory)
    {
        gameManager = manager;
        inventory = _inventory;

        WeaponsInScene = new GameObject[manager.Weapons.Length];
    }

    public void SelectWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= WeaponsInScene.Length)
        {
            Debug.LogWarning("Invalid weapon index");
            return;
        }

        if (EquipmentManager.currentlyEquippedWeapon != null)
        {
            Debug.Log("we have an active item");
            EquipmentManager.currentlyEquippedWeapon.SetActive(false);  // Deactivate the current weapon
        }

        // Activate the selected weapon
        EquipmentManager.currentlyEquippedWeapon = WeaponsInScene[weaponIndex];
        EquipmentManager.currentlyEquippedWeapon.SetActive(true);
    }

    public void Execute(object context = null)
    {
        Debug.Log(context);

    }

    public void OnKeyDownExecute(object context = null)
    {
        if (context is WeaponSelectContext weaponSelectContext)
        {
            SelectWeapon(weaponSelectContext.WeaponIndex);
        }
        if (gameManager.Weapons[0].itemName == WeaponType.Pistol)
        {
            gameManager.Weapons[0].ItemPrefab.SetActive(true);
        }
    }

    public void OnKeyUpExecute()
    {
    }
}
