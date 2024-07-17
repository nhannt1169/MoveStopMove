using UnityEngine;

public class Player : Character
{
    private Joystick joystick;
    [SerializeField] private float speed;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (rb.velocity != Vector3.zero)
        {
            characterStatus = Utils.CharacterStatus.walking;
            //ChangeAnim(Utils.animMove);
        }
        else
        {
            if (characterStatus != Utils.CharacterStatus.attacking && characterStatus != Utils.CharacterStatus.waiting)
            {
                characterStatus = Utils.CharacterStatus.idle;
                ChangeAnim(Utils.animIdle);
            }

        }
    }

    private void FixedUpdate()
    {
        if (joystick == null)
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

    public override void OnInit(Vector3 position, Weapon weapon = null)
    {
        base.OnInit(position, weapon);
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
}
