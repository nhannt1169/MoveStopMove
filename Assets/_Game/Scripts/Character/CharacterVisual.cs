using UnityEngine;

public class CharacterVisual : GameUnit
{
    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody rb;
    protected Weapon weapon;
    [SerializeField] private Transform weaponPos;
    protected string currentAnimName;
    [SerializeField] private Renderer pantsMesh;
    [SerializeField] private Transform headPos;
    protected Hair hair;

    //TODO: fix
    public void SetWeapon(Weapon weapon)
    {
        if (this.weapon != null)
        {
            Destroy(this.weapon.gameObject);
        }
        this.weapon = Instantiate(weapon, weaponPos);
        this.weapon.gameObject.SetActive(true);
    }

    public void SetHair(Hair hair)
    {
        if (this.hair != null)
        {
            Destroy(this.hair.gameObject);
        }
        this.hair = Instantiate(hair, headPos);
        this.hair.gameObject.SetActive(true);
    }

    public void SetPants(Texture pants)
    {
        pantsMesh.material.mainTexture = pants;
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
