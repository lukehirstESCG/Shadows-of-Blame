using UnityEngine;

public class PlayerMovementSM : PlayerStateMachine
{
    public CharacterController har;
    public Transform player;
    public float rotationSpeed;
    public float speed;
    public Animator anim;
    public Transform cam;
    public Vector3 rotation;

    [HideInInspector]
    public Idle idleState;
    [HideInInspector]
    public Walk walkingState;
    [HideInInspector]
    public Sprint runningState;
    [HideInInspector]
    public Crouch crouchingState;
    [HideInInspector]
    public Shoot firingState;
    [HideInInspector]
    public Prone proningState;
    [HideInInspector]
    public Jump jumpingState;
    [HideInInspector]
    public Grounded groundedState;

    private void Awake()
    {
        idleState = new Idle(this);
        walkingState = new Walk(this);
        runningState = new Sprint(this);
        crouchingState = new Crouch(this);
        firingState = new Shoot(this);
        proningState = new Prone(this);
        jumpingState = new Jump(this);
        groundedState = new Grounded(this);
    }

    protected override PlayerBaseState GetInitialState()
    {
        return idleState;
    }
}
