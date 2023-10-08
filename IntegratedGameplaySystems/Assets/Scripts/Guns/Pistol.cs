using UnityEngine;

public class Pistol : IWeapon
{
    GameManager gameManager;
    public Pistol(WeaponData assign, GameManager _gameManager, GameObject gameObject) : base(assign, gameObject, _gameManager)
    {
        gameManager = _gameManager;
        WeaponScriptableObject = assign;
        //WeaponInitialization();
    }
}
