// using UnityEngine;
// using UnityEngine.InputSystem;

// public class RotatePlayerAndCamera : IPlayerFeature {
//     private InputSystem_Actions input;
//     public float RotationSpeed;
//     void Awake() {
//         input = new InputSystem_Actions();
//         input.Player.Interact.performed += interact;
//         input.Player.Enable();
//     }

//     void Update() {
//         rotatePlayer();
//     }

//     private void rotatePlayer() {
//         Vector2 value = input.Player.Look.ReadValue<Vector2>();

//         float x = value.x * Time.deltaTime * RotationSpeed;

//         transform.Rotate(Vector3.up * x);
//     }

//     void OnEnable() {
//         input.Player.Enable();
//     }
//     void OnDisable(){
//         input.Player.Disable();
//     }
//     public override void Disable() {
//         enabled = false;
//     }
//     public override void Enable() {
//         enabled = true;
//     }
// }