using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Moving")]
    public float RotationSpeed = 10;
    public float RunningSpeed = 500;
    public float WalkSpeed = 200;
    public float AccelerationTime = 0.5f; // How much it will take to reach full speed
    private InputSystem_Actions _input;
    private CharacterController _body;
    private Animator _animator;
    // Saved move values
    private float _currentAcceleration; // From 0 to 1
    void Awake() {
        _input = new InputSystem_Actions();
        _input.Player.Enable();
    }
    void Start() {
        _body = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }
    void Update() {
        movePlayer();
        rotatePlayer();
        setCursor();
    }

    private void movePlayer() {
        Vector2 direction = _input.Player.Move.ReadValue<Vector2>();
        float actionSpeed = _input.Player.Sprint.IsPressed() ? RunningSpeed : WalkSpeed;

        // Get current acceleration
        if (direction != Vector2.zero) _currentAcceleration += Time.deltaTime / AccelerationTime;
        else _currentAcceleration -= Time.deltaTime * 2;
        _currentAcceleration = Math.Min(_currentAcceleration, 1);
        _currentAcceleration = Math.Max(_currentAcceleration, 0);

        if(_currentAcceleration == 0) {
            _animator.SetFloat("Speed", 0);
            return;
        }

        float currentSpeed = (float)((1 /(-_currentAcceleration - 1)) + 1.5) * actionSpeed;
        _animator.SetFloat("Speed", currentSpeed);

        Vector3 moveBy = new Vector3(direction.x, 0, direction.y) * Time.deltaTime * currentSpeed;

        Quaternion currentRotation = transform.rotation;
        _body.SimpleMove(currentRotation * moveBy);
    }

    private void rotatePlayer() {
        Vector2 value = _input.Player.Look.ReadValue<Vector2>();

        float x = value.x * Time.deltaTime * RotationSpeed;

        transform.Rotate(Vector3.up * x);
    }

    private void setCursor() {
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.Locked;
    }
}
