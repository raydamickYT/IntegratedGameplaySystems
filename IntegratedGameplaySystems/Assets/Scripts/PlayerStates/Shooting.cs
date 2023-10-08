using System.Security.Cryptography;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Unity.VisualScripting;

public class Shooting : ICommand
{
    public static bool _canFire = true;
    GameManager manager;
    ActorData playerData;

    public Shooting(GameManager _manager, ActorData _playerData)
    {
        playerData = _playerData;
        this.manager = _manager;
    }

    public void FireGun(object context = null)
    {
        ActorBase bullet = manager.objectPoolDelegate?.Invoke();
        if (bullet != null && _canFire)
        {
            Ray ray = playerData.playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // een ray in het midden van je scherm
            RaycastHit hit;
            //checken of de ray iets hit

            Vector3 targetHit;
            if (Physics.Raycast(ray, out hit))
            {
                targetHit = hit.point;
                bullet.BulletHit = hit.collider.gameObject;
            }
            else
            {
                targetHit = ray.GetPoint(10);
            }

            //bereken de direction
            Vector3 directionWithoutSpread = targetHit - EquipmentManager.currentlyEquippedWeapon.BulletPoint.position;

            float x = UnityEngine.Random.Range(10, 10);
            float y = UnityEngine.Random.Range(10, 10);

            //bereken de direction met spread
            Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);
            //Reset the positions (might do this in the bullet itself instead of here in the future.)
            if (bullet is IPoolable poolableBullet)
            {
                poolableBullet.Recycle(directionWithoutSpread);
            }

            bullet.ActiveObjectInScene.SetActive(true);
            
            Rigidbody rb = bullet.ActiveObjectInScene.GetComponent<Rigidbody>();

            rb.AddForce(directionWithoutSpread.normalized * EquipmentManager.currentlyEquippedWeapon.BulletForce, ForceMode.Impulse);

            //logic voor fire rate en bullet life
            //geeft een warning omdat er verwacht wordt dat deze awaited worden, maar omdat dat de tijd niet meer accurate maakt doen we dat niet
            Wait();

            BulletLifeTime(bullet);
        }
    }

    public async Task Wait()
    {
        _canFire = false;
        await Task.Delay(TimeSpan.FromSeconds(EquipmentManager.currentlyEquippedWeapon.FireRate));
        _canFire = true;
    }

    public async Task BulletLifeTime(ActorBase bullet)
    {
        //net zo simpel, als er een bepaalde tijd verstreken is, dan word de bullet weer naar de inactive pool verplaatst.
        await Task.Delay(TimeSpan.FromSeconds(EquipmentManager.currentlyEquippedWeapon.BulletLife));
        manager.DeactivationDelegate?.Invoke(bullet);
    }

    public void Execute(object context = null)
    {
        FireGun(context);
    }

    public void OnKeyDownExecute(object context = null)
    {
    }

    public void OnKeyUpExecute()
    {
    }
}