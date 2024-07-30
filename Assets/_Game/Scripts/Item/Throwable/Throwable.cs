using UnityEngine;

public class Throwable : GameUnit
{
    [SerializeField] private ThrowableScriptableObject throwableData;
    public Utils.PoolType poolType;
    private bool isMoving = false;
    private Vector3 target;
    private Character owner;
    private float timer = 0;
    [SerializeField] private Collider collider;

    private void FixedUpdate()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.forward * Time.fixedDeltaTime * throwableData.GetSpeed());
        }
        timer += Time.fixedDeltaTime;

        if (timer > 5f)
        {
            OnDespawn();
        }
    }

    public void StartMoving(Transform target, Character owner)
    {
        this.target = new Vector3(target.position.x, 1, target.position.z);
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
