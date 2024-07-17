using UnityEngine;
using UnityEngine.AI;
using static Utils;

public class Bot : Character
{
    [SerializeField] private NavMeshAgent agent;
    public Transform Destination { private set; get; }
    private ICharacterState currentState;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //currentState?.IUpdate(this);
    }

    public void Moving(Vector3 position)
    {
        //ChangeAnim(animMove);
        agent.SetDestination(position);
        agent.isStopped = false;
    }

    public void Stopping()
    {
        ChangeAnim(animIdle);
        agent.SetDestination(TF.position);
    }

    public void ChangeState(ICharacterState newState)
    {
        currentState?.IStop(this);

        currentState = newState;

        currentState?.IStart(this);
    }



    public void SetDestination()
    {
    }

    public void UpdateAtNewStage()
    {
        Destination = null;
        SetDestination();
    }

    public override void ResetStatus()
    {
        base.ResetStatus();
        //agent.SetDestination(Vector3.zero);
        Destination = null;
    }
}
