using Unity.VisualScripting;
using UnityEngine;

public class EnemyChase : EnemyBaseState
{
    private EnemyMovementSM esm;

    public EnemyChase(EnemyMovementSM enemyStateMachine) : base("Chase", enemyStateMachine)
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

        RaycastHit chaseHit;
        float rayLength = 10f;
        Ray chaseRay = new Ray(esm.FOV.transform.position, Vector3.forward);

        // Is the player more than or equal to 20 metres away from the enemy?
        if (!Physics.Raycast(chaseRay, out chaseHit, rayLength))
        {
            // Enemy is patrolling
            enemyStateMachine.ChangeState(esm.patrolState);
            esm.eAnim.SetBool("patrolling", true);
            esm.isPatrol = true;
            esm.attacking = false;
            esm.agent.isStopped = false;
        }

        // Is the enemy's health below or equal to 50 HP?
        if (esm.eHealth.health <= 50)
        {
            // Enemy is injured
            esm.eAnim.SetBool("injuredRun", true);
            esm.ecMaster.HandleGainSight(esm.enemy);
            Debug.Log("HIDING!");
        }

        /*
        // Is the player less than five metres from the enemy?
        if (Physics.Raycast(chaseRay, out chaseHit, rayLength))
        {
            enemyStateMachine.ChangeState(esm.meleeState);
            esm.eAnim.SetBool("punching", true);
            esm.isPatrol = false;
            esm.dealDamage = true;
            esm.agent.isStopped = true;
        }
        */
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        // Agent moves to the player
        esm.agent.SetDestination(esm.target.position);

        // Finds the distance between the enemy and the player
        Vector3 direction = esm.target.position - esm.enemy.transform.position;

        // Turns the enemy to face towards the player.
        esm.enemy.transform.rotation = Quaternion.Slerp(esm.enemy.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    }
}