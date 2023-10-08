using UnityEngine;

public class IWeapon
{
    GameManager gameManager;
    public WeaponData WeaponScriptableObject { get; set; }
    public GameObject WeaponInScene { get; set; }
    public float FireRate = 0, BulletLife = 0, BulletForce = 0;
    public Transform BulletPoint = null;

    public IWeapon(WeaponData script, GameObject Prefab, GameManager _gameManager)
    {
        this.WeaponScriptableObject = script;
        this.WeaponInScene = Prefab;
        this.gameManager = _gameManager;
    }

    public void WeaponInitialization()
    {
        Registry.AddToRegistry(WeaponInScene.name, this);

        WeaponInScene = InstantiateGameObjects.Instantiate(WeaponScriptableObject.ItemPrefab.name);
        WeaponInScene.transform.position = gameManager.playerData.GunHolder.transform.position;

        //weer niet heel netjes omdat er van alles fout kan gaan als de naam niet klopt. 
        //als hier een nullreference uit komt check dan eerst de naam.
        BulletPoint = WeaponInScene.transform.Find("BulletPoint").transform;

        WeaponInScene.transform.SetParent(gameManager.playerData.GunHolder.transform);
        WeaponInScene.transform.localRotation = Quaternion.Euler(0, 90, 0);

        EquipmentManager.WeaponsInScene[(int)WeaponScriptableObject.weaponsStyle] = this;
        if (WeaponScriptableObject.itemName == WeaponType.Pistol)
        {
            EquipmentManager.currentlyEquippedWeapon = this;
            WeaponInScene.SetActive(true);
        }
        else
        {
            WeaponInScene.SetActive(false);
        }

        FireRate = WeaponScriptableObject.FireRateToFloat(WeaponScriptableObject.fireRate);
        BulletLife = WeaponScriptableObject.BulletLifeToFloat(WeaponScriptableObject.bulletLife);
        BulletForce = WeaponScriptableObject.BulletForceToFloat(WeaponScriptableObject.itemName);
    }
}