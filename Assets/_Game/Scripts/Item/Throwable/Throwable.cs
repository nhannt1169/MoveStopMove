using UnityEngine;

public class Throwable : GameUnit
{
    [SerializeField] private ThrowableScriptableObject throwableData;
    public Utils.PoolType poolType;
    private bool isMoving = false;
    private Vector3 target;
    private Character owner;

    private void FixedUpdate()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, throwableData.GetSpeed() * Time.fixedDeltaTime);
        }
    }

    public void StartMoving(Transform target, Character owner)
    {
        this.target = new Vector3(target.position.x, 1, target.position.z);
        //this.transform.LookAt(target.position);
        this.owner = owner;
        isMoving = true;
        Invoke(nameof(OnDespawn), 5f);
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

    private void OnDespawn()
    {
        ObjectPool.DespawnObject(this, poolType);
    }
}
