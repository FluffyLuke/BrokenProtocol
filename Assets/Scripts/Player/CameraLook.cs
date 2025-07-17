using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineStateDrivenCamera))]
public class CameraLook : MonoBehaviour
{
    public float Sensivity;
    private InputSystem_Actions _input;
    private CinemachinePanTilt[] _cameras;
    void Awake() {
        _input = new InputSystem_Actions();
        _input.Player.Enable();
    }
    void Start() {
        _cameras = GetComponentsInChildren<CinemachinePanTilt>();
    }
    void Update() {
        RotateCamera();
    }
    void RotateCamera() {
        Vector2 direction = _input.Player.Look.ReadValue<Vector2>() * Sensivity;

        Debug.Log(direction);

        foreach(var c in _cameras) {
            c.PanAxis.Value += direction.x * Time.deltaTime;
            c.TiltAxis.Value += direction.x * Time.deltaTime;
        }
    }
}
