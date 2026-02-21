using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerZoom : IPlayerFeature {
    private InputSystem_Actions input;
    void Awake() {
        input = new InputSystem_Actions();
        input.Player.Zoom.started += zoomIn;
        input.Player.Zoom.canceled += zoomOut;
    }

    private void zoomIn(InputAction.CallbackContext context) {
        PlayerEventBus.Zoom.Invoke(true);
    }
    private void zoomOut(InputAction.CallbackContext context) {
        PlayerEventBus.Zoom.Invoke(false);
    }
    void OnEnable() {
        input.Player.Enable();
    }
    void OnDisable(){
        input.Player.Disable();
        PlayerEventBus.Zoom.Invoke(false);
    }
    public override void Disable() {
        enabled = false;
    }
    public override void Enable() {
        enabled = true;
    }
}