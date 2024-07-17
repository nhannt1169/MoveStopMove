using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] Character character;

    private void Start()
    {
        Physics.IgnoreCollision(character.GetComponent<Collider>(), this.GetComponent<Collider>());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Utils.botTag) || other.CompareTag(Utils.playerTag))
        {
            if (other.TryGetComponent<Character>(out var target))
            {
                character.AddTarget(target);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Utils.botTag) || other.CompareTag(Utils.playerTag))
        {
            if (other.TryGetComponent<Character>(out var target))
            {
                character.RemoveTarget(target);
            }
        }
    }
}
