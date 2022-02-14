using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EHMovementComponent : EHActorComponent
{
    protected const float GravityConstant = 9.8f;
    
    public enum EMovementState
    {
        Walk,
        Run,
        Sprint,
        Crouch,
        InAir,
        Custom,
    }
    public Vector3 Velocity;

    [Header("Movement Speeds")] 
    [SerializeField, Tooltip("The acceleration of our character while they are on the ground")]
    protected float GroundedAcceleration = 50f;
    [SerializeField, Tooltip("Walk speed of our character")]
    protected float WalkSpeed = 5;
    [SerializeField, Tooltip("Run Speed of our character")]
    protected float RunSpeed = 10;
    [SerializeField, Tooltip("The speed of our character when they are sprinting")]
    protected float SprintSpeed = 15;
    [SerializeField, Tooltip(("The speed of our character while they are crouching"))]
    protected float CrouchSpeed = 4f;
    

    [Header("Jumping Values")] 
    [SerializeField, Tooltip("The speed that our character will jump in the y Axis")]
    protected float JumpVelocity;
    [SerializeField, Tooltip("The scaled acceleration speed of our character due to gravity")]
    protected float GravityScale = 1f;
    [SerializeField, Range(0f, 1f), Tooltip("The amount of control that our character will have in the air. 0 means that our character will not change velocity even when input is set by the player")]
    protected float AirControlPercentage = 0f;
    [SerializeField, Tooltip("The max number of double jumps our character can perform before landing")]
    protected byte MaxDoubleJumps = 0;

    protected EMovementState MovementState = EMovementState.Walk;
    protected Vector2 MovementInput = Vector2.zero;
    private int TotalDoubleJumps = 0;
    
    


    #region monobehaviour methods

    protected virtual void Awake()
    {
        
    }

    protected virtual void Update()
    {
        UpdateFromMovementState();
        UpdateVelocityFromGravity();
        UpdatePosition();
    }
    
    #endregion monobehaviour methods
    public void SetHorizontalInput(float HInput)
    {
        MovementInput.x = HInput;
    }

    public void SetVerticalInput(float VInput)
    {
        MovementInput.y = VInput;
    }

    protected virtual void UpdateFromMovementState()
    {
        switch (MovementState)
        {
            case EMovementState.Walk:
                break;
            case EMovementState.Run:
                break;
            case EMovementState.Crouch:
                break;
            case EMovementState.InAir:
                break;
        }
    }

    private void UpdateVelocityFromGravity()
    {
        if (MovementState != EMovementState.InAir) return;
        Velocity.y -= GravityConstant * GravityScale * Time.deltaTime;
    }

    private void UpdatePosition()
    {
        SetActorLocation(GetActorLocation() + (Velocity * Time.deltaTime), true);
    }
    
    protected virtual float GetGoalVelocityFromState()
    {
        switch (MovementState)
        {
            case EMovementState.Crouch:
                return CrouchSpeed;
            case EMovementState.Walk:
                return WalkSpeed;
            case EMovementState.Run:
                return RunSpeed;
            case EMovementState.InAir:
                return 0;
            default:
                Debug.LogWarning("State is not setup");
                return 0;
        }
    }
    
    public virtual void AttemptJump()
    {
        if (MovementState != EMovementState.InAir)
        {
            Jump();
            return;
        }

        if (TotalDoubleJumps >= MaxDoubleJumps) return;
        TotalDoubleJumps++;
        Jump();
    }

    protected virtual void Jump()
    {
        Velocity.y = JumpVelocity;
    }

    protected virtual void StopJump()
    {
        Velocity.y = 0;
    }
}
