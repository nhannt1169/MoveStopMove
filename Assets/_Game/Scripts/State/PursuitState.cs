using UnityEngine;

public class PursuitState : ICharacterState
{
    private float timeLimit = 1f;
    private float timer;
    public void IStart(Bot bot)
    {
        bot.FindTargetMovePoint(bot.GetPursuitTarget().TF.position);
    }

    public void IStop(Bot bot)
    {

    }

    public void IUpdate(Bot bot)
    {
        timer += Time.deltaTime;
        if (bot.CheckTargetsInRange() || timer > timeLimit)
        {
            bot.ChangeState(new IdleState());
        }
    }
}