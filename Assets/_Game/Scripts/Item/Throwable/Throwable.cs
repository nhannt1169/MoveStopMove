using UnityEngine;

public class Throwable : GameUnit
{
    [SerializeField] private ScriptableObjectThrowable throwableData;
    public Utils.PoolType poolType;
    private bool isMoving = false;
    private bool isReturning = false;
    private Character owner;
    private float timer = 0;
    [SerializeField] private Collider throwableCollider;
    [SerializeField] private Transform throwableModel;

    private void FixedUpdate()
    {
        if (isMoving)
        {
            transform.Translate(throwableData.GetSpeed() * Time.fixedDeltaTime * Vector3.forward);
        }
        else if (throwableData.GetWeaponThrowType() == Utils.WeaponThrowType.returning && isReturning)
        {
            TF.position = Vector3.MoveTowards(TF.position, owner.TF.position, throwableData.GetSpeed() * Time.fixedDeltaTime);
        }

        if (throwableData.GetWeaponThrowType() != Utils.WeaponThrowType.straight)
        {
            throwableModel.transform.Rotate(0, 0, -1000 * Time.fixedDeltaTime);
        }

        timer += Time.fixedDeltaTime;

        if (timer > 5f)
        {
            OnDespawn();
        }
    }

    public void StartMoving(Character owner)
    {
        this.owner = owner;
        isMoving = true;
        timer = 0;
        if (owner != null)
        {
            Physics.IgnoreCollision(throwableCollider, owner.GetCollider(), true);
        }
    }

    public void StartReturning()
    {
        isMoving = false;
        isReturning = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Utils.botTag) || other.CompareTag(Utils.playerTag))
        {
            if (other.TryGetComponent<Character>(out var target))
            {
                if (throwableData.GetWeaponThrowType() == Utils.WeaponThrowType.returning)
                {
                    StartReturning();
                }
                else
                {
                    OnDespawn();
                }

                target.OnDeath();
                owner.RemoveTarget(target);
                owner.OnKill();
            }
        }
    }

    public void OnDespawn()
    {
        if (owner != null)
        {
            Physics.IgnoreCollision(throwableCollider, owner.GetCollider(), false);
            owner.RemoveThrowable(this);
        }
        ObjectPool.DespawnObject(this, poolType);
        timer = 0;
    }
}
