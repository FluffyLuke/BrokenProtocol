using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOpenPause : IPlayerFeature {
    private InputSystem_Actions input;
    void Awake() {
        input = new InputSystem_Actions();
        input.Player.OpenPause.performed += openPause;
    }

    private void openPause(InputAction.CallbackContext context) {
        PlayerEventBus.OpenPause.Invoke(!PlayerEventBus.isPauseOpened);
    }
    void OnEnable() {
        input.Player.Enable();
    }
    void OnDisable(){
        input.Player.Disable();
    }
    public override void Disable() {
        enabled = false;
    }
    public override void Enable() {
        enabled = true;
    }
}