using UnityEngine;
using UnityEngine.AI;
using static Utils;

public class Bot : Character
{
    [SerializeField] private NavMeshAgent agent;
    private ICharacterState currentState;
    [SerializeField] private float movementRange;
    private Vector3 destination;
    [SerializeField] private float comeBackRange;
    [SerializeField] private Transform playerTF;
    public bool IsAtDestination => Vector3.Distance(TF.position, destination + (TF.position.y - destination.y) * Vector3.up) < 0.1f;

    //public bool IsCloseToPlayer => Vector3.Distance(TF.position, playerTF.position) < 10f;

    // Update is called once per frame
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

    public override void OnInit(Vector3 position, Weapon weapon = null)
    {
        base.OnInit(position, weapon);
        ChangeState(new IdleState());
    }

    public void FindTargetMovePoint(bool moveTowardsPlayer = false)
    {
        //if (moveTowardsPlayer)
        //{
        //    Moving(playerTF.position);
        //}
        //else 
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
}
