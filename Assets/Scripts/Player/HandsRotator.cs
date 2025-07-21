using Unity.Cinemachine;
using UnityEngine;

public class HandsRotator : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _camera;

    private void OnEnable() {
        CinemachineCore.CameraUpdatedEvent.AddListener(onCameraUpdated);
    }

    private void OnDisable() {
        CinemachineCore.CameraUpdatedEvent.RemoveListener(onCameraUpdated);
    }

    private void onCameraUpdated(CinemachineBrain brain) {
        transform.rotation = _camera.transform.rotation;
    }
}
