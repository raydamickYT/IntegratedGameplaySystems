using System.Security.Cryptography;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Unity.VisualScripting;

public class Shooting : ICommand
{
    public static bool _canFire = true;
    GameManager manager;
    PlayerData playerData;

    public Shooting(GameManager _manager, PlayerData _playerData)
    {
        playerData = _playerData;
        this.manager = _manager;
        Debug.Log(manager.playerData.GunHolder.transform.position);
    }

    public async void FireGun(object context = null)
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
            }
            else
            {
                targetHit = ray.GetPoint(75);
            }

            //bereken de direction
            Vector3 directionWithoutSpread = targetHit - playerData.GunHolder.transform.position;

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
            rb.AddForce(directionWithoutSpread.normalized * manager.bullets.BulletSpeed, ForceMode.Impulse);

            //logic voor fire rate en bullet life
            await Wait();

            await BulletLifeTime(bullet);

        }
    }

    public async Task Wait()
    {
        _canFire = false;
        // yield return new WaitForSeconds(manager.bullets.FireRate); //FireRate kan je in de "GameManager" aanpassen, kleiner getal = sneller schieten.
        await Task.Delay(TimeSpan.FromSeconds(manager.bullets.FireRate));
        _canFire = true;
    }

    public async Task BulletLifeTime(ActorBase bullet)
    {
        //net zo simpel, als er een bepaalde tijd verstreken is, dan word de bullet weer naar de inactive pool verplaatst.
        //yield return new WaitForSeconds(manager.bullets.BulletLife); //bulletlife kan je in de "GameManager" aanpassen.
        await Task.Delay(TimeSpan.FromSeconds(manager.bullets.BulletLife));
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
