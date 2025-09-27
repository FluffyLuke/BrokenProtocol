using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineStateDrivenCamera))]
public class CameraLook : MonoBehaviour
{
    public float Sensivity;
    private InputSystem_Actions input;
    private CinemachinePanTilt[] cameras;
    void Awake() {
        input = new InputSystem_Actions();
        input.Player.Enable();
    }
    void Start() {
        cameras = GetComponentsInChildren<CinemachinePanTilt>();
    }
    void Update() {
        RotateCamera();
    }
    void RotateCamera() {
        Vector2 direction = input.Player.Look.ReadValue<Vector2>() * Sensivity;

        Debug.Log(direction);

        foreach(var c in cameras) {
            c.PanAxis.Value += direction.x * Time.deltaTime;
            c.TiltAxis.Value += direction.x * Time.deltaTime;
        }
    }
}
