using UnityEngine;

public class SightArea : MonoBehaviour
{
    [SerializeField] Character character;
    private float timer;
    private float timeLimit = 5;

    private void Start()
    {
        Physics.IgnoreCollision(character.GetComponent<Collider>(), this.GetComponent<Collider>());
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer > timeLimit)
        {
            character.RemovePursuitTarget();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Utils.botTag) || other.CompareTag(Utils.playerTag))
        {
            if (other.TryGetComponent<Character>(out var target))
            {
                character.SetPursuitTarget(target);
            }
        }
    }
}
