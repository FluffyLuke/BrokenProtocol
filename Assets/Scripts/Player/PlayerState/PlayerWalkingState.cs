using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(ColliderWraper))]
[RequireComponent(typeof(Animator))]
public class PlayerWalkingState : PlayerState
{
    [Header("Moving")]
    public float RotationSpeed;
    public float MaxWalkingSpeed;
    public float MaxRunningSpeed;
    [Header("Feet")]
    public ColliderWraper GroundedCollider;
    public ColliderWraper FallingCollider;

    // Other components
    private InputSystem_Actions input;
    private CharacterController body;
    private Animator animator;
    void Awake() {
        input = new InputSystem_Actions();
        input.Player.Enable();
    }
    void Start() {
        body = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    void Update() {
        movePlayer();
        rotatePlayer();
        setCursor();
    }

    // TODO: Rework movement for acceleration and deacceleration
    private void movePlayer() {
        Vector2 inputDirection = input.Player.Move.ReadValue<Vector2>();
        bool sprinting = input.Player.Sprint.IsPressed();
        float targetSpeed = sprinting ? MaxRunningSpeed : MaxWalkingSpeed;

        Vector3 direction = new Vector3(inputDirection.x, 0, inputDirection.y);
        Vector3 move = transform.rotation * direction;
        move.Normalize();
        move *= targetSpeed;

        move = adjustDirectionToSlope(move);

        Debug.Log($"Magnitude: {move.magnitude}");
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
