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

    public void SetWeapon(int weaponIdx)
    {
        if (this.weapon != null)
        {
            Destroy(this.weapon.gameObject);
        }

        if (weaponIdx != -1 && weaponIdx < ItemManager.instance.weapons.Length)
        {
            this.weapon = Instantiate(ItemManager.instance.weapons[weaponIdx], weaponPos);
            this.weapon.gameObject.SetActive(true);
        }
    }

    public void SetHair(int hairIdx)
    {
        if (this.hair != null)
        {
            Destroy(this.hair.gameObject);
        }
        if (hairIdx != -1 && hairIdx < ItemManager.instance.hairs.Length)
        {
            this.hair = Instantiate(ItemManager.instance.hairs[hairIdx], headPos);
            this.hair.gameObject.SetActive(true);
        }
    }

    public void SetPants(int pantsIdx)
    {
        if (pantsIdx != -1 && pantsIdx < ItemManager.instance.pants.Length)
        {
            pantsMesh.material.mainTexture = ItemManager.instance.pants[pantsIdx].GetTexture();
            pantsMesh.gameObject.SetActive(true);
        }
        else
        {
            pantsMesh.gameObject.SetActive(false);
            pantsMesh.material = null;
        }
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
