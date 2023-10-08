using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class BulletActor : ActorBase, IPoolable
{
    private GameManager manager;
    private ObjectPool objectPool;
    private BulletsData bullet;

    //deze base assigned gelijk de prefab aan de actorbase
    public BulletActor(BulletsData _bullet, GameManager _manager, int NameInt, ObjectPool _objectPool) : base(_bullet.BulletObject)
    {
        this.objectPool = _objectPool;
        this.manager = _manager;
        //dit voegt dit script toe niet de gameobject. die zit vast aan dit script.
        bullet = _bullet;
        Registry.AddToRegistry($"{bullet.BulletObject.name}{NameInt}", this);
        ActiveObjectInScene = InstantiateGameObjects.Instantiate($"{bullet.BulletObject.name}{NameInt}");
    }

    public void BulletUpdate()
    {
        //logic voor de bullet hit
        //check om te kijken dat de raycast hit die is meegegeven vannuit Shoot.cs niet null is

        if (BulletHit != null)
        {
            float actualDistance = Vector3.Distance(BulletHit.transform.position, ActiveObjectInScene.transform.position);

            if (actualDistance <= 2)
            {
                //omdat de bullethit alleen een gameobject is, kunnen we kijken of hij in de objeectregistry zit(de grote dictionary die alle gameobjecten opslaat)
                //als dat zo is, dan kunnen we vervolgens bij de andere waardes zoals de health, enz.
                if (Registry.ObjectRegistry.TryGetValue(BulletHit.name, out var BulletDataFromDict))
                {
                    //doe hier een check voor het object dat je wilt raken. in dit geval damageableActor.
                    if (BulletDataFromDict is IDamageableActor damageableActor)
                    {
                        //hier kan je logic uitvoeren, wat moet er gebeuren als de enemy het juiste object raakt.
                        manager.timer.ReduceTime(4);
                        damageableActor.DamageAbleObject.SetActive(false);
                        return;
                    }
                }
                //nadeel van dit systeem is dat alles in een layermask moet zitten anders deactiveerd de bullet niet als hij een object raakt.
                manager.DeactivationDelegate?.Invoke(this);
            }
        }
    }

    public void Recycle(Vector3 direction)
    {
        manager.OnFixedUpdate += BulletUpdate;
        //wat te doen als dit object deactiveerd.
        ActiveObjectInScene.transform.position = EquipmentManager.currentlyEquippedWeapon.BulletPoint.position;
        ActiveObjectInScene.transform.rotation = EquipmentManager.currentlyEquippedWeapon.BulletPoint.rotation * Quaternion.Euler(0, 0, -90);
        //ActiveObjectInScene.transform.forward = direction.normalized;
        Rigidbody rb = ActiveObjectInScene.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
