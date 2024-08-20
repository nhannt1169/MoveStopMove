using UnityEngine;

public class Player : Character
{
    private Joystick joystick;
    [SerializeField] private float speed;
    [SerializeField] private float maxHeight;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (rb.velocity != Vector3.zero)
        {
            if (ChangeCharacterStatus(Utils.CharacterStatus.walking))
            {
                StopAllCoroutines();
                ChangeAnim(Utils.animMove);
            }
        }
        else
        {
            if (characterStatus != Utils.CharacterStatus.attacking && characterStatus != Utils.CharacterStatus.waiting)
            {
                if (ChangeCharacterStatus(Utils.CharacterStatus.idle))
                {
                    ChangeAnim(Utils.animIdle);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (joystick == null || characterStatus == Utils.CharacterStatus.dead)
        {
            return;
        }

        if (speed < 1)
        {
            Debug.LogError("No speed!!!");
        }

        rb.velocity = new Vector3(joystick.Horizontal * speed * Time.fixedDeltaTime, rb.velocity.y, joystick.Vertical * speed * Time.fixedDeltaTime);

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    public override void OnInit(Vector3 position)
    {
        base.OnInit(position);
    }

    public void SetJoystick(Joystick joystick)
    {
        this.joystick = joystick;
    }

    public void Win(Vector3 position)
    {
        TF.position = position;
        rb.velocity = Vector3.zero;

        rb.AddForce(20f * Vector3.up);
        ChangeAnim(Utils.animJump);
    }

    public override void OnKill()
    {
        base.OnKill();
        DataManager.instance.GetCurrentData().userData.coins += 10;
        DataManager.instance.SaveToJson();
        float currHeight = TF.localScale.y;
        if (currHeight < maxHeight)
        {
            TF.localScale = new Vector3(TF.localScale.x, currHeight + 0.1f, TF.localScale.z);
        }
    }

    public override void OnDeath()
    {
        base.OnDeath();
        this.TF.position = new Vector3(0, 0, 0);
        LevelManager.instance.OnLose();
    }
}
