using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Utils;

public class Character : GameUnit
{
    //[SerializeField] ColorData colorData;
    [SerializeField] Animator anim;
    private Utils.PoolType weaponPoolType;
    [SerializeField] protected Rigidbody rb;
    private string currentAnimName;
    private Weapon weapon;
    [SerializeField] private Transform weaponPos;
    [SerializeField] protected float range;
    [SerializeField] private float size;
    [SerializeField] private AttackArea attackArea;
    protected CharacterStatus characterStatus;
    protected List<Character> targets = new();
    [SerializeField] private Transform attackPos;

    [SerializeField] private TextMeshPro nameText;
    private Vector3 nameOffset = new Vector3(0, 180, 0);
    protected virtual void Update()
    {
        nameText.transform.LookAt(Camera.main.transform);
        nameText.transform.Rotate(nameOffset);
        if (characterStatus == CharacterStatus.dead)
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
        if (weapon == null)
        {
            weapon = WeaponManager.instance.weapons[Random.Range(0, WeaponManager.instance.weapons.Length)];
        }
        targets = new List<Character>();
        SetWeapon(weapon);
        gameObject.SetActive(true);
    }

    public virtual void OnDeath()
    {
        ChangeAnim(Utils.animDie);
        ChangeCharacterStatus(CharacterStatus.dead);
        StopAllCoroutines();
        Invoke(nameof(OnDespawn), 2f);
    }

    public virtual void OnDespawn()
    {
        gameObject.SetActive(false);
    }


    public virtual void ResetStatus()
    {

    }

    public void SetWeapon(Weapon weapon)
    {
        if (this.weapon != null)
        {
            Destroy(this.weapon);
        }
        this.weapon = Instantiate(weapon, weaponPos);
        this.weapon.gameObject.SetActive(true);
    }

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(currentAnimName);

            currentAnimName = animName;

            anim.SetTrigger(currentAnimName);
        }
    }

    protected bool ChangeCharacterStatus(Utils.CharacterStatus characterStatus)
    {
        if (this.characterStatus == CharacterStatus.dead)
        {
            return false;
        }

        this.characterStatus = characterStatus;
        return true;
    }

    internal void AddTarget(Character target)
    {
        targets.Add(target);
    }

    internal void RemoveTarget(Character target)
    {
        targets.Remove(target);
    }

    //Start the attack process
    protected void Attack()
    {
        if (characterStatus != CharacterStatus.idle)
        {
            return;
        };
        Character chosen = null;
        foreach (Character target in targets)
        {
            if (target.isActiveAndEnabled)
            {
                chosen = target;
                break;
            }
        }

        if (chosen == null) { return; }


        if (ChangeCharacterStatus(CharacterStatus.attacking))
        {
            TF.LookAt(chosen.TF);
            ChangeAnim(animThrow);
            StartCoroutine(ThrowWeapon(chosen.TF));
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
                Throwable throwable = (Throwable)ObjectPool.SpawnObject(attackPos.position, Quaternion.identity, weapon.poolType, null);
                throwable.StartMoving(target, this);
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
}
