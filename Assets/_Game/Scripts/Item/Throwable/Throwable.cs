using UnityEngine;

public class Throwable : GameUnit
{
    [SerializeField] private ScriptableObjectThrowable throwableData;
    public Utils.PoolType poolType;
    private bool isMoving = false;
    private Character owner;
    private float timer = 0;
    [SerializeField] private Collider collider;
    [SerializeField] private Transform throwableModel;

    private void FixedUpdate()
    {
        if (isMoving)
        {
            transform.Translate(throwableData.GetSpeed() * Time.fixedDeltaTime * Vector3.forward);
        }

        if (throwableData.GetWeaponThrowType() == Utils.WeaponThrowType.spinning)
        {
            throwableModel.transform.Rotate(0, 20 * Time.fixedDeltaTime, 0);
        }
        timer += Time.fixedDeltaTime;

        if (timer > 5f)
        {
            OnDespawn();
        }
    }

    public void StartMoving(Transform target, Character owner)
    {
        this.owner = owner;
        isMoving = true;
        timer = 0;
        Physics.IgnoreCollision(collider, owner.GetCollider(), true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Utils.botTag) || other.CompareTag(Utils.playerTag))
        {
            if (other.TryGetComponent<Character>(out var target))
            {
                OnDespawn();
                target.OnDeath();
                owner.RemoveTarget(target);
                owner.EarnCoinIfPlayer();
            }
        }
    }

    public void OnDespawn()
    {
        Physics.IgnoreCollision(collider, owner.GetCollider(), false);
        owner.RemoveThrowable(this);
        ObjectPool.DespawnObject(this, poolType);
        timer = 0;
    }
}
