using Unity.Cinemachine;
using UnityEngine;

public class RotateWithCamera : MonoBehaviour
{
    // [Range(0.5f, 1.5f)]
    // public float RotateRatio;
    [SerializeField] private CinemachineCamera targetCamera;

    private void OnEnable() {
        CinemachineCore.CameraUpdatedEvent.AddListener(onCameraUpdated);
    }

    private void OnDisable() {
        CinemachineCore.CameraUpdatedEvent.RemoveListener(onCameraUpdated);
    }

    private void onCameraUpdated(CinemachineBrain brain) {
        transform.rotation = targetCamera.transform.rotation;
    }
}
