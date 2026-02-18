using UnityEngine;
using UnityEngine.InputSystem;

public class MoreSpeed: MonoBehaviour {
    private InputSystem_Actions input;
    void Awake() {
        input = new InputSystem_Actions();
        input.Debug.MoreSpeed.performed += addSpeed;
        input.Debug.Enable();
    }

    private void addSpeed(InputAction.CallbackContext context) {
        PlayerWalkingState w = GameObject.FindWithTag(Tags.PlayerTag).GetComponent<PlayerWalkingState>();
        w.MaxRunningSpeed *= 2;
    }

}