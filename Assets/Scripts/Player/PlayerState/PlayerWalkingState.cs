using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerWalkingState : IPlayerState
{
    [Header("Moving")]
    public float RotationSpeed;
    public float MaxWalkingSpeed;
    public float MaxRunningSpeed;
    private enum MovementState {
        Standing, Walking, Running
    }
    private MovementState currentMovementState;
    private MovementState previousMovementState;
    [Header("Head bob")]
    public Transform bobPivot;
    [Range(0, 1)]
    public float headBobRotationForce = 0.5f;
    [Range(0, 1)]
    public float headBobMaxX = 0.5f;
    [Range(0, 1)]
    public float headBobMaxY = 0.5f;
    private float headBobT = 0;
    private Vector3 originalBobPivotPosition;
    [Header("Feet")]
    public ColliderWraper GroundedCollider;
    public ColliderWraper FallingCollider;
    [Header("Sounds")]
    SoundHandle currentWalkSound = null;

    // Other components
    private CharacterController body;
    private Animator animator;
    public override void EnterState() {
        base.EnterState();
        input.Player.Enable();
        GetComponent<PlayerInteract>().enabled = true;
    }
    public override void ExitState() {
        base.ExitState();
        input.Player.Disable();
        GetComponent<PlayerInteract>().enabled = false;
    }
    void Start() {
        body = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        originalBobPivotPosition = bobPivot.localPosition;
    }
    void Update() {
        movePlayer();
        rotatePlayer();
        setCursor();
        headBob();
        handleSound();

        previousMovementState = currentMovementState;
    }
    
    private void handleSound() {
        if (previousMovementState == currentMovementState) return;
        Debug.Log($"Current movement state: {currentMovementState}");

        if (currentWalkSound != null) {
            Debug.Log("Reset");
            currentWalkSound.StopAndDestroy();
            currentWalkSound = null;
        }
        
        if (currentMovementState == MovementState.Running) {
            if (SoundManager.instance.PlayAndLoop(SoundAssetID.PlayerRunSnow, transform.position, out SoundHandle handle)) {
                Debug.Log("Run");
                currentWalkSound = handle;
            }
        }

        if (currentMovementState == MovementState.Walking) {
            if (SoundManager.instance.PlayAndLoop(SoundAssetID.PlayerWalkSnow, transform.position, out SoundHandle handle)) {
                Debug.Log("Walk");
                currentWalkSound = handle;
            }
        }
    }

    private void headBob() {
        if (currentMovementState == MovementState.Standing) {
            return;
        }

        headBobT += Time.deltaTime;
        headBobT %= 2;

        Vector3 newRotation = bobPivot.rotation.eulerAngles;
        float rotation = Mathf.Sin(headBobT * Mathf.PI) * animator.GetFloat("Speed") * headBobRotationForce;

        newRotation.z = rotation;
        bobPivot.rotation = Quaternion.Euler(newRotation);

        float x = Mathf.Sin(headBobT * Mathf.PI) * headBobMaxX;
        float y = Mathf.Cos(headBobT * Mathf.PI * 2) * headBobMaxY;
        bobPivot.localPosition = new Vector3 (x, y, 0) + originalBobPivotPosition;
    }

    // TODO: Rework movement for acceleration and deacceleration
    private void movePlayer() {
        Vector2 inputDirection = input.Player.Move.ReadValue<Vector2>();
        bool sprinting = input.Player.Sprint.IsPressed();
        float targetSpeed = sprinting ? MaxRunningSpeed : MaxWalkingSpeed;

        if (sprinting && inputDirection.magnitude > 0) {
            currentMovementState = MovementState.Running;
        } else if (inputDirection.magnitude > 0) {
            currentMovementState = MovementState.Walking;
        } else {
            currentMovementState = MovementState.Standing;
        }

        Vector3 direction = new Vector3(inputDirection.x, 0, inputDirection.y);
        Vector3 move = transform.rotation * direction;
        move.Normalize();
        move *= targetSpeed;

        move = adjustDirectionToSlope(move);

        animator.SetFloat("Speed", move.magnitude);
        
        body.Move(move * Time.deltaTime + Vector3.down * 2f * Time.deltaTime);
    }

    // Fuck the slopes
    private Vector3 adjustDirectionToSlope(Vector3 move) {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, body.height / 2f + 0.5f)) {
            move = Vector3.ProjectOnPlane(move, hit.normal);
        }
        return move;
    }

    private void rotatePlayer() {
        Vector2 value = input.Player.Look.ReadValue<Vector2>();

        float x = value.x * Time.deltaTime * RotationSpeed;

        transform.Rotate(Vector3.up * x);
    }

    private void setCursor() {
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.Locked;
    }
}
