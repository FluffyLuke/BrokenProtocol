using System;
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
    private float _currentAcceleration;
    private InputSystem_Actions _input;
    private CharacterController _body;
    private Animator _animator;
    void Awake() {
        _input = new InputSystem_Actions();
        _input.Player.Enable();
    }
    void Start() {
        _body = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }
    void Update() {
        MovePlayer();
        RotatePlayer();
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.Locked;
    }

    void MovePlayer() {
        Vector2 direction = _input.Player.Move.ReadValue<Vector2>();
        float actionSpeed = _input.Player.Sprint.IsPressed() ? RunningSpeed : WalkSpeed;

        if(direction != Vector2.zero) _currentAcceleration += Time.deltaTime / AccelerationTime;
        else _currentAcceleration = 0;

        float currentSpeed = Mathf.Lerp(actionSpeed / 2.0f, actionSpeed, _currentAcceleration);
        
        if(direction != Vector2.zero) {
            _animator.SetFloat("Speed", currentSpeed);
        } else {
            _animator.SetFloat("Speed", 0);
        }

        Vector3 moveBy = new Vector3(direction.x, 0, direction.y) * Time.deltaTime * currentSpeed;

        Quaternion currentRotation = transform.rotation;
        _body.SimpleMove(currentRotation * moveBy);
    }

    void RotatePlayer() {
        Vector2 value = _input.Player.Look.ReadValue<Vector2>();

        float x = value.x * Time.deltaTime * RotationSpeed;

        transform.Rotate(Vector3.up * x);
    }
}
