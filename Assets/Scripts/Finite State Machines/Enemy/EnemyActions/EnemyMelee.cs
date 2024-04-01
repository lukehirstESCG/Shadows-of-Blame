using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : EnemyBaseState
{
    EnemyMovementSM esm;
    float meleeDist = 8;

    public EnemyMelee(EnemyMovementSM enemyStateMachine) : base("Melee", enemyStateMachine)
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

        if (!esm.attackedPlayer)
        {
            esm.eMSystem.AttackPlayer();
        }

        if (Vector3.Distance(esm.enemy.transform.position, esm.target.transform.position) > meleeDist && !esm.playsm.weapon.gunEquipped)
        {
            enemyStateMachine.ChangeState(esm.chaseState);
            esm.isMeleeAttack = false;
            esm.isChasing = true;
            esm.eAnim.SetBool("chase", true);
        }

        if (esm.playsm.weapon.gunEquipped)
        {
            enemyStateMachine.ChangeState(esm.fireState);
            esm.isMeleeAttack = false;
            esm.isShooting = true;
            esm.eAnim.SetBool("shoot", true);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        esm.enemy.LookAt(esm.target);

        // Finds the distance between the enemy and the player
        Vector3 direction = esm.target.position - esm.enemy.transform.position;

        // Turns the enemy to face towards the player.
        esm.enemy.transform.rotation = Quaternion.Slerp(esm.enemy.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    }
}