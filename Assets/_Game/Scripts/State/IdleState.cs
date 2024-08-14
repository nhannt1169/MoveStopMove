using UnityEngine;

public class IdleState : ICharacterState
{
    private float timeLimit = 1f;
    private float timer;
    public void IStart(Bot bot)
    {
        bot.Stopping();
    }

    public void IStop(Bot bot)
    {

    }

    public void IUpdate(Bot bot)
    {
        timer += Time.deltaTime;
        if (timer > timeLimit)
        {
            if (!bot.CheckTargetsInRange())
            {
                if (bot.GetPursuitTarget() != null)
                {
                    bot.ChangeState(new PursuitState());
                }
                else
                {
                    bot.ChangeState(new PatrolState());
                }
            }
            timer = 0;
        }
    }
}
