using UnityEngine;

public class BulletActor : ActorBase, IPoolable
{
    private GameManager manager;
    private ObjectPool objectPool;
    private Bullets bullet;

    //deze base assigned gelijk de prefab aan de actorbase
    public BulletActor(Bullets _bullet, GameManager _manager, int NameInt, ObjectPool _objectPool) : base(_bullet.BulletObject)
    {
        this.objectPool = _objectPool;
        this.manager = _manager;

        manager.OnUpdate += BulletUpdate;

        //dit voegt dit script toe niet de gameobject. die zit vast aan dit script.
        bullet = _bullet;
        Registry.AddToRegistry($"{bullet.BulletObject.name}{NameInt}", this);
        ActiveObjectInScene = InstantiateGameObjects.Instantiate($"{bullet.BulletObject.name}{NameInt}");
    }

    public void BulletUpdate()
    {
        if (ActiveObjectInScene.activeSelf)
        {
            if (base.BulletHit != null)
            {
                float actualDistance = Vector3.Distance(base.BulletHit.transform.position, ActiveObjectInScene.transform.position);
                if (actualDistance <= 5)
                {
                    Debug.Log(BulletHit.layer);
                    manager.DeactivationDelegate?.Invoke(this);
                }
            }
        }
    }

    public void Recycle(Vector3 direction)
    {
        //wat te doen als dit object deactiveerd.
        ActiveObjectInScene.transform.position = EquipmentManager.currentlyEquippedWeapon.BulletPoint.position;
        ActiveObjectInScene.transform.rotation = EquipmentManager.currentlyEquippedWeapon.BulletPoint.rotation * Quaternion.Euler(0, 0, -90);
        //ActiveObjectInScene.transform.forward = direction.normalized;
        Rigidbody rb = ActiveObjectInScene.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
