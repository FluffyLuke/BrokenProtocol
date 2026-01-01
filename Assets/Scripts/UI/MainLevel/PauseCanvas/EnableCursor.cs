using UnityEngine;

public class EnableCursor : MonoBehaviour {
    [SerializeField] private bool enableOnStart = false;
    void Start() {
        enableCursor(enableOnStart);
        PlayerEventBus.EnableCursor.AddListener(enableCursor);
    }

    private void enableCursor(bool enable) {
        UnityEngine.Cursor.visible = enable;
        UnityEngine.Cursor.lockState = enable ? UnityEngine.CursorLockMode.Confined : UnityEngine.CursorLockMode.Locked;
    }
}