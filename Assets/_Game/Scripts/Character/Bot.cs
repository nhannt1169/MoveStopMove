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


    // Start is called before the first frame update
    void Start()
    {
    }

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

    public void FindTargetMovePoint()
    {
        Vector3 target;
        if (RandomPoint(this.TF.position, movementRange, out target))
        {
            Moving(target);
        }
    }

    private bool RandomPoint(Vector3 center, float movementRange, out Vector3 res)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * movementRange;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 10.0f, NavMesh.AllAreas))
        {
            res = hit.position;
            return true;
        }

        res = Vector3.zero;
        return false;
    }

    public bool CheckTargetsInRange()
    {
        foreach (var target in targets)
        {
            if (Vector3.Distance(TF.position, target.TF.position) <= range)
            {
                return true;
            }
        }

        targets.Clear();
        return false;
    }
}
