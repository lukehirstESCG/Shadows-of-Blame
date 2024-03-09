using UnityEngine;

public class Idle : PlayerBaseState
{
    private PlayerMovementSM playsm;

    public Idle(PlayerMovementSM playerStateMachine) : base("Idle", playerStateMachine)
    {
        playsm = playerStateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        playerStateMachine.ChangeState(playsm.walkingState);

        playerStateMachine.ChangeState(playsm.crouchingState);
    }
}
