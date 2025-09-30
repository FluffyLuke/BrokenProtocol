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
            if (!hit.transform.CompareTag(Tags.InteractableTag)) return;

            IInteractable interactable = hit.transform.GetComponent<IInteractable>();
            if (interactable == null) {
                Debug.LogWarning("Interactable object has no component for interaction");
            }
            interactable.Interact();
        }
    }
}