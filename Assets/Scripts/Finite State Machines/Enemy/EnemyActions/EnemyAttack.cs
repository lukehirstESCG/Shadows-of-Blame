using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : EnemyBaseState
{
    private EnemyMovementSM esm;

    public EnemyShoot(EnemyMovementSM enemyStateMachine) : base("Shoot", enemyStateMachine)
    {
        esm = enemyStateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (esm.eHealth.health <= 65)
        {
            enemyStateMachine.ChangeState(esm.coverState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
