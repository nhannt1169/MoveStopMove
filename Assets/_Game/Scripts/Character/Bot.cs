using UnityEngine;
using UnityEngine.AI;
using static Utils;

public class Bot : Character
{
    [SerializeField] private NavMeshAgent agent;
    private ICharacterState currentState;
    [SerializeField] private float movementRange;
    private Vector3 destination;
    public bool IsAtDestination => Vector3.Distance(TF.position, destination + (TF.position.y - destination.y) * Vector3.up) < 0.1f;
    //[SerializeField] private Indicator

    protected override void Update()
    {
        base.Update();
        currentState?.IUpdate(this);
    }

    public void Moving(Vector3 position)
    {
        if (ChangeCharacterStatus(CharacterStatus.walking))
        {
            ChangeAnim(animMove);
            this.destination = position;
            agent.SetDestination(position);
            agent.isStopped = false;
        }
    }

    public void Stopping()
    {
        if (ChangeCharacterStatus(CharacterStatus.idle))
        {
            agent.SetDestination(TF.position);
            agent.isStopped = true;
            ChangeAnim(animIdle);
        }
    }

    public void ChangeState(ICharacterState newState)
    {
        currentState?.IStop(this);

        currentState = newState;

        currentState?.IStart(this);
    }

    public override void ResetStatus()
    {
        base.ResetStatus();
        //agent.SetDestination(Vector3.zero);
    }

    public override void OnInit(Vector3 position)
    {
        base.OnInit(position);
        RandomizeAppearance();
        ChangeState(new IdleState());
    }

    public void FindTargetMovePoint(Vector3? targetMovePoint = null)
    {
        if (targetMovePoint != null)
        {
            Moving((Vector3)targetMovePoint);
        }
        else
        if (RandomPoint(this.TF.position, movementRange, out Vector3 target))
        {
            Moving(target);
        }
    }

    private bool RandomPoint(Vector3 center, float movementRange, out Vector3 res)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * movementRange;
        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 10.0f, NavMesh.AllAreas))
        {
            res = hit.position;
            return true;
        }

        res = Vector3.zero;
        return false;
    }

    public override void OnDeath()
    {
        base.OnDeath();
        BotManager.instance.RemoveBot(this);
    }
}
