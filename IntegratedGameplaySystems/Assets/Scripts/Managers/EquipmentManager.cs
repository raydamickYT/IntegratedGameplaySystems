using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EquipmentManager : ICommand
{
    public GameManager gameManager;
    public static IWeapon currentlyEquippedWeapon = null;
    public static IWeapon[] WeaponsInScene;
    public EquipmentManager(GameManager manager)
    {
        gameManager = manager;

        WeaponsInScene = new IWeapon[manager.Weapons.Length];
    }

    public void SelectWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= WeaponsInScene.Length)
        {
            Debug.LogWarning("Invalid weapon index");
            return;
        }

        if (currentlyEquippedWeapon != null)
        {
            currentlyEquippedWeapon.WeaponInScene.SetActive(false);  // Deactivate the current weapon
        }

        // Activate the selected weapon
        currentlyEquippedWeapon = WeaponsInScene[weaponIndex];
        currentlyEquippedWeapon.WeaponInScene.SetActive(true);
    }

    public void Execute(object context = null)
    {
        //Very Spammy
        //Debug.Log(context);
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
