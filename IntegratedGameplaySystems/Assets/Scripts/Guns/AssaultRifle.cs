using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssaultRifle : IWeapon
{
    GameManager gameManager;
    public AssaultRifle(WeaponData assign, GameManager _gameManager, GameObject gameObject) : base(assign, gameObject, _gameManager)
    {
        gameManager = _gameManager;
        WeaponScriptableObject = assign;
        WeaponInitialization();
    }
}
