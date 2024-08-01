using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Utils;

public class Character : CharacterVisual
{
    //[SerializeField] ColorData colorData;
    private Utils.PoolType weaponPoolType;
    [SerializeField] protected float range;
    [SerializeField] private float size;
    [SerializeField] private AttackArea attackArea;
    protected CharacterStatus characterStatus;
    protected List<Character> targets = new();
    [SerializeField] private Transform attackPos;

    [SerializeField] private TextMeshPro nameText;
    private Vector3 nameOffset = new(0, 180, 0);
    public bool IsDead => characterStatus == CharacterStatus.dead;
    private List<Throwable> throwables;
    [SerializeField] private Collider collider;
    protected virtual void Update()
    {
        if (nameText != null)
        {
            nameText.transform.LookAt(Camera.main.transform);
            nameText.transform.Rotate(nameOffset);
        }

        if (IsDead)
        {
            return;
        }
        if (characterStatus == CharacterStatus.idle && targets.Count > 0)
        {
            Attack();
        }
    }

    public virtual void OnInit(Vector3 position, Weapon weapon = null)
    {
        TF.position = position;
        if (weapon == null && this.weapon == null)
        {
            weapon = WeaponManager.instance.weapons[0];
        }
        targets = new List<Character>();
        throwables = new List<Throwable>();
        if (weapon != null)
        {
            SetWeapon(weapon);
        }

        gameObject.SetActive(true);
    }

    public virtual void OnDeath()
    {
        ChangeAnim(Utils.animDie);
        ChangeCharacterStatus(CharacterStatus.dead);
        StopAllCoroutines();
        foreach (Throwable throwable in throwables.ToArray())
        {
            throwable.OnDespawn();
        }
        Invoke(nameof(OnDespawn), 2f);
    }

    public virtual void OnDespawn()
    {
        gameObject.SetActive(false);
    }


    public virtual void ResetStatus()
    {

    }

    internal void AddTarget(Character target)
    {
        targets.Add(target);
    }

    internal void RemoveTarget(Character target)
    {
        targets.Remove(target);
    }

    public Collider GetCollider()
    {
        return collider;
    }

    protected bool ChangeCharacterStatus(Utils.CharacterStatus characterStatus)
    {
        if (IsDead)
        {
            return false;
        }

        this.characterStatus = characterStatus;
        return true;
    }

    //Start the attack process
    protected void Attack()
    {
        if (characterStatus != CharacterStatus.idle)
        {
            return;
        };

        if (ChangeCharacterStatus(CharacterStatus.attacking))
        {
            Transform chosen = null;
            foreach (Character target in targets)
            {
                if (target.isActiveAndEnabled)
                {
                    chosen = target.TF;
                    break;
                }
            }

            if (chosen == null) { return; }


            TF.LookAt(chosen);
            ChangeAnim(animThrow);
            StartCoroutine(ThrowWeapon(chosen));
        }
    }

    //Spawn and start moving the throwable towards target
    protected IEnumerator ThrowWeapon(Transform target)
    {
        yield return new WaitForSeconds(0.5f);
        if (characterStatus == CharacterStatus.attacking)
        {
            if (weapon != null)
            {
                weapon.gameObject.SetActive(false);
                Throwable throwable = (Throwable)ObjectPool.SpawnObject(attackPos.position, this.transform.rotation, weapon.poolType, null);
                throwable.StartMoving(target, this);
                throwables.Add(throwable);
            }
            if (ChangeCharacterStatus(CharacterStatus.waiting))
            {
                Invoke(nameof(ResetAttackStatus), 3f);
            }
        }
        else
        {
            StopAllCoroutines();
        }
    }

    protected void ResetAttackStatus()
    {
        StopAllCoroutines();

        if (ChangeCharacterStatus(CharacterStatus.idle))
        {
            if (weapon != null)
            {
                weapon.gameObject.SetActive(true);
            }
            ChangeAnim(Utils.animIdle);
        }
    }

    public void RemoveThrowable(Throwable throwable)
    {
        throwables.Remove(throwable);
    }

    public bool CheckTargetsInRange()
    {
        foreach (var target in targets)
        {
            if (Vector3.Distance(TF.position, target.TF.position) <= range && !target.IsDead)
            {
                return true;
            }
        }

        targets.Clear();
        return false;
    }
}
