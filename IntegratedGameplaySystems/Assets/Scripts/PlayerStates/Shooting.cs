using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Threading.Tasks;
using UnityEngine;

public class Shooting : ICommand
{
    public static bool _canFire = true;
    GameManager manager;
    private float nextFireTime = 0f;
    public Shooting(GameManager _manager)
    {
        this.manager = _manager;
    }

    public async void FireGun(object context = null)
    {
        ActorBase bullet = manager.objectPoolDelegate?.Invoke();

        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + manager.bullets.FireRate;
            if (bullet != null && _canFire)
            {
                //set position
                bullet.ActiveObjectInScene.transform.position = manager.playerData.playerPosition;
                bullet.ActiveObjectInScene.transform.rotation = manager.playerData.PlayerMesh.transform.rotation;

                Rigidbody rb = bullet.ActiveObjectInScene.GetComponent<Rigidbody>();
                if (rb != null && context is MovementContext movementContext)
                {
                    rb.velocity = movementContext.Direction.normalized * manager.bullets.BulletSpeed; //bulletspeed is te veranderen in de scriptable object
                }

                //logic voor fire rate en bullet life
                await Wait();
                if (bullet is IPoolable poolableBullet)
                {
                    await BulletLifeTime(poolableBullet);
                }

            }
        }
    }

    public async Task Wait()
    {
        _canFire = false;
        // yield return new WaitForSeconds(manager.bullets.FireRate); //FireRate kan je in de "GameManager" aanpassen, kleiner getal = sneller schieten.
        await Task.Delay(TimeSpan.FromSeconds(manager.bullets.FireRate));
        _canFire = true;
    }

    public async Task BulletLifeTime(IPoolable bullet)
    {
        //net zo simpel, als er een bepaalde tijd verstreken is, dan word de bullet weer naar de inactive pool verplaatst.
        //yield return new WaitForSeconds(manager.bullets.BulletLife); //bulletlife kan je in de "GameManager" aanpassen.
        await Task.Delay(TimeSpan.FromSeconds(manager.bullets.BulletLife));
        bullet.Recycle();
        Debug.Log("dedbullet");
        //manager.DeactivationDelegate?.Invoke(bullet);
    }

    public void Execute(object context = null)
    {
        FireGun(context);
    }

    public void OnKeyDownExecute()
    {

    }

    public void OnKeyUpExecute()
    {
    }
}
