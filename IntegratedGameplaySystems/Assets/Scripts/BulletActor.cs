using UnityEngine;

public class BulletActor : ActorBase, IPoolable
{
    private GameManager manager;
    private ObjectPool objectPool;

    //deze base assigned gelijk de prefab aan de actorbase
    public BulletActor(GameObject Prefab, GameManager _manager, int NameInt, ObjectPool _objectPool) : base(Prefab)
    {
        this.objectPool = _objectPool;
        this.manager = _manager;
        
        //dit voegt dit script toe niet de gameobject. die zit vast aan dit script.
        Registry.AddToRegistry(Prefab.name + NameInt, this);
        ActiveObjectInScene = InstantiateGameObjects.Instantiate(Prefab.name + NameInt);

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
