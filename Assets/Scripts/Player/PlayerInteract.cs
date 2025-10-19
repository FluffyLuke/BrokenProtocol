using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour {
    public float range = 3.0f;
    private InputSystem_Actions input;
    void Awake() {
        input = new InputSystem_Actions();
        input.Player.Interact.performed += interact;
        input.Player.Enable();
    }

    void OnEnable() {
        input.Player.Enable();
    }

    void OnDisable(){
        input.Player.Disable();
    }

    private void interact(InputAction.CallbackContext context) {
        Debug.Log("Player is trying to interact...");

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, range)) {
            Debug.Log($"Ray hit: {hit.transform.gameObject.name}");

            IInteractable interactable = hit.transform.GetComponent<IInteractable>();
            if (interactable == null || !interactable.canInteract) return;

            foreach(var r in interactable.requirements) {
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
}