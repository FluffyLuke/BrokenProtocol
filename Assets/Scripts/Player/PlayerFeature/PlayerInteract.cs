using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : IPlayerFeature {
    public float range = 3.0f;
    private InputSystem_Actions input;
    void Awake() {
        input = new InputSystem_Actions();
        input.Player.Interact.performed += interact;
        input.Player.Enable();
    }

    private void interact(InputAction.CallbackContext context) {
        Debug.Log("Player is trying to interact...");

        int layerMask = 1 << 6;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, range, layerMask)) {
            Debug.Log($"Ray hit: {hit.transform.gameObject.name}");

            IInteractable interactable = hit.transform.GetComponent<IInteractable>();
            if (interactable == null || !interactable.canInteract) return;

            foreach(var r in interactable.playerRequirements) {
                switch(r) {
                    case InteractionRequirement.HolsterWeapon:
                        Debug.Log("Interaction requirement: Holster weapon");
                        requirementHolsterWeapon();
                        break;
                }
            }

            Debug.Log($"Object hit is an interactable. Interacting...");

            interactable.Interact();
        }
    }
    private void requirementHolsterWeapon() {
        PlayerEventBus.HideItem.Invoke();
    }

    void OnEnable() {
        input.Player.Enable();
    }

    void OnDisable(){
        input.Player.Disable();
    }

    public override void Disable() {
        input.Player.Disable();
        //enabled = false;
        PlayerEventBus.PauseGame.RemoveListener(gamePaused);
    }

    public override void Enable() {
        input.Player.Enable();
        //enabled = true;
        PlayerEventBus.PauseGame.AddListener(gamePaused);
    }

    private void gamePaused(bool v) {
        if (v) input.Player.Disable();
        else input.Player.Enable();
    }
}