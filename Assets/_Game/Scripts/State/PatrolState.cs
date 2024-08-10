public class PatrolState : ICharacterState
{
    public void IStart(Bot bot)
    {
        bot.FindTargetMovePoint();
    }

    public void IStop(Bot bot)
    {

    }

    public void IUpdate(Bot bot)
    {
        if (bot.GetPursuitTarget() != null)
        {
            bot.ChangeState(new PursuitState());
        }
        else
        if (bot.IsAtDestination)
        {
            bot.ChangeState(new IdleState());
        }
    }
}