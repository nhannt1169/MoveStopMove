using UnityEngine;

public class CharacterVisual : GameUnit
{
    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody rb;
    protected Weapon weapon;
    [SerializeField] private Transform weaponPos;
    protected string currentAnimName;


    public void SetWeapon(Weapon weapon)
    {
        if (this.weapon != null)
        {
            Destroy(this.weapon.gameObject);
        }
        this.weapon = Instantiate(weapon, weaponPos);
        this.weapon.gameObject.SetActive(true);


    }

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            if (currentAnimName != null)
            {
                anim.ResetTrigger(currentAnimName);
            }

            currentAnimName = animName;

            anim.SetTrigger(currentAnimName);
        }
    }
}
